using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace RateLimit.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IpRateLimitController : ControllerBase
    {
        private readonly IpRateLimitOptions _options;
        private readonly IIpPolicyStore _ipPolicyStore;

        public IpRateLimitController(
            IOptions<IpRateLimitOptions> optionsAccessor,
            IIpPolicyStore ipPolicyStore
            )
        {
            _options = optionsAccessor.Value;
            _ipPolicyStore = ipPolicyStore;
        }

        [HttpGet]
        public async Task ExistsAsync(string key)
        {
            var id = $"{_options.IpPolicyPrefix}_{key}";

            await _ipPolicyStore.ExistsAsync(id);
        }

        [HttpGet]
        public async Task<IpRateLimitPolicies> GetAsync(string key)
        {
            return await _ipPolicyStore.GetAsync($"{_options.IpPolicyPrefix}_{key}");
        }

        /// <summary>
        /// 添加特殊Ip规则
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SetAsync(string key)
        {
            var id = $"{_options.IpPolicyPrefix}_{key}";
            var ipPolicy = await _ipPolicyStore.GetAsync(id);
            ipPolicy.IpRules.Add(new IpRateLimitPolicy
            {
                Ip = "111",
                Rules = new List<RateLimitRule>() {

                    new RateLimitRule(){
                       Endpoint = "*/api/testpolicyupdate",
                       Period = "1h",
                       Limit = 100
                    },
                    new RateLimitRule(){
                        Endpoint = "*/api/testpolicyupdate",
                        Period = "1h",
                        Limit = 100
                    }
                }
            });

            await _ipPolicyStore.SetAsync(id, ipPolicy);
        }

        /// <summary>
        /// 删除所有
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task DeleteAsync(string key)
        {
            var id = $"{_options.IpPolicyPrefix}_{key}";

            await _ipPolicyStore.RemoveAsync(id);
        }
    }
}
