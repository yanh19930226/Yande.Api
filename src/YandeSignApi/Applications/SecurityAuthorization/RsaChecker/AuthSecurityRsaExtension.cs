using Microsoft.AspNetCore.Authentication;
using System;

namespace YandeSignApi.Applications.SecurityAuthorization.RsaChecker
{
    //注册使用自定义AuthenticationSchemeSecurityRsaAuth
    public static class AuthSecurityRsaExtension
    {
        public static AuthenticationBuilder AddAuthSecurityRsa(this AuthenticationBuilder builder)
            => builder.AddAuthSecurityRsa(AuthSecurityScheme.AuthenticationSchemeSecurityRsaAuth, _ => { });

        public static AuthenticationBuilder AddAuthSecurityRsa(this AuthenticationBuilder builder, Action<AuthSecurityRsaOptions> configureOptions)
            => builder.AddAuthSecurityRsa(AuthSecurityScheme.AuthenticationSchemeSecurityRsaAuth, configureOptions);

        public static AuthenticationBuilder AddAuthSecurityRsa(this AuthenticationBuilder builder, string authenticationScheme, Action<AuthSecurityRsaOptions> configureOptions)
            => builder.AddAuthSecurityRsa(authenticationScheme, displayName: null, configureOptions: configureOptions);

        public static AuthenticationBuilder AddAuthSecurityRsa(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<AuthSecurityRsaOptions> configureOptions)
        {
            return builder.AddScheme<AuthSecurityRsaOptions, AuthSecurityRsaAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
