namespace In66.Authentication.Bearer
{
    public class BearerTokenValidFailedContext : ResultContext<BearerSchemeOptions>
    {
        public BearerTokenValidFailedContext(HttpContext context, AuthenticationScheme scheme, BearerSchemeOptions options)
        : base(context, scheme, options)
        {
        }
    }
}
