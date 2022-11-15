using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In66.Authentication.Rsa
{
    //注册使用自定义AuthenticationSchemeSecurityRsaAuth
    public static class RsaSchemeExtension
    {
        public static AuthenticationBuilder AddAuthSecurityRsa(this AuthenticationBuilder builder)
            => builder.AddAuthSecurityRsa(RsaDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddAuthSecurityRsa(this AuthenticationBuilder builder, Action<RsaSchemeOptions> configureOptions)
            => builder.AddAuthSecurityRsa(RsaDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddAuthSecurityRsa(this AuthenticationBuilder builder, string authenticationScheme, Action<RsaSchemeOptions> configureOptions)
            => builder.AddAuthSecurityRsa(authenticationScheme, displayName: null, configureOptions: configureOptions);

        public static AuthenticationBuilder AddAuthSecurityRsa(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<RsaSchemeOptions> configureOptions)
        {
            return builder.AddScheme<RsaSchemeOptions, RsaAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
