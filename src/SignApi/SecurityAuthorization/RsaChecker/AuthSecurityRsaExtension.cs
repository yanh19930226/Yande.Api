using Microsoft.AspNetCore.Authentication;
using System;

namespace SignApi.SecurityAuthorization.RsaChecker
{
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
