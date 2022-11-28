namespace RateLimit.Models
{
    public class RateLimitOption
    {
        public string IpPolicyPrefix { get; set; } = "ippp";

        public List<RateLimitRule> GeneralRules { get; set; }

        public List<string> EndpointWhitelist { get; set; }

        public string ClientIdHeader { get; set; } = "X-ClientId";

        public List<string> ClientWhitelist { get; set; }

        public string RealIpHeader { get; set; } = "X-Real-IP";

        public List<string> IpWhitelist { get; set; }

        public int HttpStatusCode { get; set; } = 429;

        public string QuotaExceededMessage { get; set; }

        public QuotaExceededResponse QuotaExceededResponse { get; set; }

        public string RateLimitCounterPrefix { get; set; } = "crlc";


        public bool StackBlockedRequests { get; set; }

        public bool EnableEndpointRateLimiting { get; set; }

        public bool DisableRateLimitHeaders { get; set; }

        public bool EnableRegexRuleMatching { get; set; }
    }

    public class RateLimitRule
    {
        public string Endpoint { get; set; }

        public string Period { get; set; }

        public TimeSpan? PeriodTimespan { get; set; }

        public double Limit { get; set; }

        public QuotaExceededResponse QuotaExceededResponse { get; set; }

        public bool MonitorMode { get; set; }
    }

    public class QuotaExceededResponse
    {
        public string ContentType { get; set; }
       

        public string Content { get; set; }

        public int? StatusCode { get; set; } = 429;
    }
}
