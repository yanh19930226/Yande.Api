using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace RateLimit.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientRateLimitController : ControllerBase
    {
        private readonly ClientRateLimitOptions _options;
        private readonly IClientPolicyStore _clientPolicyStore;

        public ClientRateLimitController(
            IOptions<ClientRateLimitOptions> optionsAccessor, 
            IClientPolicyStore clientPolicyStore
            )
        {
            _options = optionsAccessor.Value;
            _clientPolicyStore = clientPolicyStore;
        }

        [HttpGet]
        public async Task ExistsAsync()
        {
            var id = $"{_options.ClientPolicyPrefix}_cl-key-1";

            await _clientPolicyStore.ExistsAsync(id);
        }

        [HttpGet]
        public async Task<ClientRateLimitPolicy> GetAsync()
        {
            return await _clientPolicyStore.GetAsync($"{_options.ClientPolicyPrefix}_cl-key-1");
        }

        [HttpPost]
        public async Task SetAsync()
        {
            var id = $"{_options.ClientPolicyPrefix}_cl-key-1";
            var clPolicy = await _clientPolicyStore.GetAsync(id);
            clPolicy.Rules.Add(new RateLimitRule
            {
                Endpoint = "*/api/testpolicyupdate",
                Period = "1h",
                Limit = 100
            });

            await _clientPolicyStore.SetAsync(id, clPolicy);
        }

        [HttpDelete]
        public async Task DeleteAsync()
        {
            var id = $"{_options.ClientPolicyPrefix}_cl-key-1";

            await _clientPolicyStore.RemoveAsync(id);
        }
    }
}
