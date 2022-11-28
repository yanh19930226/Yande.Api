using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RateLimit.Models;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace RateLimit.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : Controller
    {
        private IOptionsSnapshot<IpRateLimitOptions> _options;
        private  IRedisDatabase _redisDatabase;
        private IConfiguration _configuration;
        public HomeController(
            IOptionsSnapshot<IpRateLimitOptions> options,
            IRedisDatabase redisDatabase,
            IConfiguration configuration
            )
        {
            _options = options;
            _redisDatabase=redisDatabase;
            _configuration=configuration;
        }

        
        [HttpPost]
        public ActionResult<IpRateLimitOptions> Set()
        {
            IpRateLimitOptions opt = new IpRateLimitOptions();

            opt.IpPolicyPrefix = "test";
            opt.RealIpHeader = "X-Real-IP";
            opt.IpWhitelist = new List<string>() { "117.29.133.53" };
            opt.ClientWhitelist = new List<string> { "dev-id-1", "dev-id-2" };
            opt.EndpointWhitelist = new List<string>() { "get:/api/home", "*:/api/status" };
            opt.GeneralRules = new List<AspNetCoreRateLimit.RateLimitRule>() {
               new AspNetCoreRateLimit.RateLimitRule(){
                   Period="1s",
                   Limit=1,
                   MonitorMode=false,
                   Endpoint="*"
               } 
            };
            opt.EnableEndpointRateLimiting = true;
            opt.StackBlockedRequests = true;
            opt.QuotaExceededResponse = new AspNetCoreRateLimit.QuotaExceededResponse()
            {
                StatusCode = 429,
                Content = "{{\"code\":429,\"msg\":\"Visit too frequently, please try again later\",\"data\":null}}",
                ContentType = "application/json;utf-8"
            };
            opt.HttpStatusCode = 429;

            RedisConfig redisConfig = new RedisConfig();

            redisConfig.IpRateLimiting = opt;

            _redisDatabase.AddAsync("RedisConfig", redisConfig)
           .GetAwaiter()
           .GetResult();

            var res = _redisDatabase.GetAsync<IpRateLimitOptions>("IpRateLimiting")
            .GetAwaiter()
            .GetResult();

            return Ok();
        }

        [HttpGet]
        public ActionResult<IpRateLimitOptions> Get()
        {
            var val = _options.Value;

            var res = _redisDatabase.GetAsync<IpRateLimitOptions>("IpRateLimiting")
            .GetAwaiter()
            .GetResult();


            return Ok(val);
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("Delete");
        }
    }
}
