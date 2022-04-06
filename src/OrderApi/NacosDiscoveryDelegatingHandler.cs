using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OrderApi
{
    public class NacosDiscoveryDelegatingHandler : DelegatingHandler
    {
        private readonly Nacos.V2.INacosNamingService _serverManager;
        private readonly ILogger<NacosDiscoveryDelegatingHandler> _logger;

        private readonly string GroupName;

        private readonly string ServiceName;

        public NacosDiscoveryDelegatingHandler(Nacos.V2.INacosNamingService serverManager, ILogger<NacosDiscoveryDelegatingHandler> logger)
        {
            _serverManager = serverManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var current = request.RequestUri;
            try
            {
                var instance = await _serverManager.SelectOneHealthyInstance("App2", "DEFAULT_GROUP");
                var host = $"{instance.Ip}:{instance.Port}";
                var baseUrl = instance.Metadata.TryGetValue("secure", out _)
                    ? $"https://{host}"
                    : $"http://{host}";

                request.RequestUri = new Uri($"{baseUrl}{current.PathAndQuery}");
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger?.LogDebug(e, "Exception during SendAsync()");
                throw;
            }
            finally
            {
                request.RequestUri = current;
            }
        }
    }
}
