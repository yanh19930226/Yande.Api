using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OrderApi
{
    public class NacosDiscoveryDelegatingHandler : DelegatingHandler
    {
        private readonly INacosServerManager _serverManager;
        private readonly ILogger<NacosDiscoveryDelegatingHandler> _logger;

        public NacosDiscoveryDelegatingHandler(INacosServerManager serverManager, ILogger<NacosDiscoveryDelegatingHandler> logger)
        {
            _serverManager = serverManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var current = request.RequestUri;
            try
            {
                //通过nacos sdk获取注册中心服务地址，内置了随机负载均衡算法，所以只返回一条信息
                var baseUrl = await _serverManager.GetServerAsync(current.Host);
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
