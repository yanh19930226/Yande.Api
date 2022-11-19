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
        public async Task ExistsAsync()
        {
            var id = $"{_options.IpPolicyPrefix}_cl-key-1";

            await _ipPolicyStore.ExistsAsync(id);
        }


        [HttpGet]
        public async Task<IpRateLimitPolicies> GetAsync()
        {
            return await _ipPolicyStore.GetAsync($"{_options.IpPolicyPrefix}_cl-key-1");
        }

        [HttpPost]
        public async Task SetAsync()
        {
            var id = $"{_options.IpPolicyPrefix}_cl-key-1";
            var ipPolicy = await _ipPolicyStore.GetAsync(id);
            ipPolicy.IpRules.Add(new IpRateLimitPolicy
            {
               Ip="111",
                //Endpoint = "*/api/testpolicyupdate",
                //Period = "1h",
                //Limit = 100
            });

            await _ipPolicyStore.SetAsync(id, ipPolicy);
        }

        [HttpDelete]
        public async Task DeleteAsync()
        {
            var id = $"{_options.IpPolicyPrefix}_cl-key-1";

            await _ipPolicyStore.RemoveAsync(id);
        }
    }
}
