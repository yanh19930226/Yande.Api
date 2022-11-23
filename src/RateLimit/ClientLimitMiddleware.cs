using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;

namespace RateLimit
{
    public class ClientLimitMiddleware : ClientRateLimitMiddleware
    {
        public ClientLimitMiddleware(RequestDelegate next, IProcessingStrategy processingStrategy, IOptions<ClientRateLimitOptions> options, IClientPolicyStore policyStore, IRateLimitConfiguration config, ILogger<ClientRateLimitMiddleware> logger) : base(next, processingStrategy, options, policyStore, config, logger)
        {
        }

    }
}
