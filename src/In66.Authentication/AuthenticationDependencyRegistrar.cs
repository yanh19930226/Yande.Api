using In66.Authentication.Basic;
using In66.Authentication.Hybrid;
using In66.Authentication.JwtBearer;

namespace In66.Authentication
{
    public  static class AuthenticationDependencyRegistrar
    {
        /// <summary>
        /// <summary>
        /// 注册身份认证组件
        /// </summary>
        public static void AddAuthentication<TAuthenticationHandler>(this IServiceCollection services,IConfiguration Configuration)
            where TAuthenticationHandler : AbstractAuthenticationProcessor
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services
                .AddScoped<AbstractAuthenticationProcessor, TAuthenticationHandler>();

            services
                .AddAuthentication(HybridDefaults.AuthenticationScheme)
                //hybrid
                .AddHybrid()
                //basic
                .AddBasic(options => options.Events.OnTokenValidated = (context) =>//options.Events.OnTokenValidated:登入成功后记录的信息
                {
                    var AuthenticationInfo = context.HttpContext.RequestServices.GetService<AuthenticationInfo>();
                    var claims = context.Principal?.Claims;
                    AuthenticationInfo.Id = long.Parse(claims.First(x => x.Type == BasicDefaults.NameId).Value);
                    AuthenticationInfo.Account = claims.First(x => x.Type == BasicDefaults.UniqueName).Value;
                    AuthenticationInfo.Name = claims.First(x => x.Type == BasicDefaults.Name).Value;
                    AuthenticationInfo.RemoteIpAddress = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                    return Task.CompletedTask;
                })
                //Token(Jwt+Redis)
                .AddBearer(options => {
                    options.Events.OnTokenValidated = (context) =>
                    {
                        var AuthenticationInfo = context.HttpContext.RequestServices.GetService<AuthenticationInfo>();
                        var claims = context.Principal.Claims;
                        //AuthenticationInfo赋值
                        AuthenticationInfo.Id = long.Parse(claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value);
                        AuthenticationInfo.Account = claims.First(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value;
                        AuthenticationInfo.Name = claims.First(x => x.Type == JwtRegisteredClaimNames.Name).Value;
                        AuthenticationInfo.RoleIds = claims.First(x => x.Type == "roleids").Value;
                        AuthenticationInfo.RemoteIpAddress = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                        return Task.CompletedTask;
                    };
                    options.Events.OnTokenFailed = (context) =>
                    {
                        context.Response.Headers.Add("Token-Error", "Token-Error");
                        return Task.CompletedTask;
                    };
                });
                ////Jwt(消息不存储redis)
                //.AddJwtBearer(options =>
                //{
                //    services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
                //    var jwtConfig = services.BuildServiceProvider().GetService<IOptions<JwtConfig>>().Value;
                //    options.TokenValidationParameters = JwtTokenHelper.GenarateTokenValidationParameters(jwtConfig);
                //    options.Events = JwtTokenHelper.GenarateJwtBearerEvents(jwtConfig);
                //});
        }
    }
}
