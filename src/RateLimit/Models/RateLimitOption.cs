namespace RateLimit.Models
{
    public class RateLimitOption
    {
        /// <summary>
        /// Api的前缀
        /// </summary>
        public string IpPolicyPrefix { get; set; }
        /// <summary>
        /// 请求Ip头
        /// </summary>
        public string RealIpHeader { get; set; } = "X-Real-IP";
        /// <summary>
        /// ip白名单
        /// </summary>
        public List<string> IpWhitelist { get; set; }
        /// <summary>
        /// api可忽略限制的接口列表
        /// </summary>
        public List<string> EndpointWhitelist { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool StackBlockedRequests { get; set; }
        /// <summary>
        /// 是否应用于全局
        /// </summary>
        public bool EnableEndpointRateLimiting { get; set; }
    }
}
