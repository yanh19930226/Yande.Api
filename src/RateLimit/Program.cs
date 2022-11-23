using AspNetCoreRateLimit;
using Autofac.Extensions.DependencyInjection;
using RateLimit;
using RateLimit.RedisConfiguration;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
var conf = builder.Configuration;
// Add services to the container.

//添加Redis配置源
conf.AddRedisConfiguration("114.55.177.197,connectTimeout=1000,connectRetry=1,syncTimeout=10000,DefaultDatabase=8", "IpRateLimitOptions", 15);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRedisSetup();
builder.Services.AddMemoryCache();

//AddInMemoryRateLimiting
builder.Services.AddInMemoryRateLimiting();
// 从appsettings.json中加载ip限流配置通用规则
//builder.Services.Configure<IpRateLimitOptions>(opt => {

//    //opt.IpPolicyPrefix = "";
//    //opt.IpWhitelist = "";
//    //opt.RealIpHeader = "";
//    //opt.EndpointWhitelist = "";

//    opt.EnableEndpointRateLimiting = true;
//    opt.StackBlockedRequests = true;
//    opt.QuotaExceededResponse = new QuotaExceededResponse()
//    {
//        StatusCode = 429,
//        Content = "{{\"code\":429,\"msg\":\"Visit too frequently, please try again later\",\"data\":null}}",
//        ContentType= "application/json;utf-8"
//    };
//    opt.HttpStatusCode = 429;
//});


//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


builder.Services.Configure<IpRateLimitOptions>(conf.GetSection("IpRateLimiting"));
// 从appsettings.json中加载客户端限流配置通用规则
builder.Services.Configure<ClientRateLimitOptions>(conf.GetSection("IpRateLimiting"));
// 配置（解析器、计数器密钥生成器）
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
//解析clientid和ip的使用有用，如果默认没有启用，则此处启用
//services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

/// <summary>
/// 特殊ip和客户端规则加载
/// </summary>
//builder.Services.AddSingleton<IRateLimitCounterStore, RedisRateLimitCounterStore>();
builder.Services.AddSingleton<IIpPolicyStore, RedisIpPolicyStore>();
builder.Services.AddSingleton<IClientPolicyStore, RedisClientPolicyStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//使用Ip限制
app.UseIpRateLimiting();
//app.UseClientRateLimiting();

app.UseAuthorization();

app.MapControllers();

app.Run();
