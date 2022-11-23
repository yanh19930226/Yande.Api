using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace RateLimit.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : Controller
    {
        public IOptionsSnapshot<IpRateLimitOptions> _options;
        public HomeController(
            IOptionsSnapshot<IpRateLimitOptions> options
            )
        {
            _options = options;
        }

        
        [HttpGet]
        public ActionResult<IpRateLimitOptions> Get()
        {
            var val = _options.Value;

            return Ok(val);
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("Delete");
        }
    }
}
