using AspNetCoreRateLimit;
using Extensions.Configuration.Redis;
using Microsoft.Extensions.Options;
using RateLimit;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRedisSetup();
builder.Services.AddMemoryCache();

#region 添加自定义配置源Redis

IConfigurationBuilder configurationBuilder;

var cancellationTokenSource = new CancellationTokenSource();
        configurationBuilder = builder.Configuration.AddRedis("RedisConfig",
        cancellationTokenSource.Token, options =>
        {
            options.Server = "114.55.177.197,connectTimeout=1000,connectRetry=1,syncTimeout=10000,abortConnect=false,DefaultDatabase=8";
            options.OnReload = () =>
            {
                Console.WriteLine("============== Updated ============");
            };
        });

/// <summary>
/// 创建配置
/// </summary>
IConfiguration Configuration = configurationBuilder.Build();
#endregion

#region Redis中添加配置
var connf = new RedisConfiguration
{
    AbortOnConnectFail = true,
    KeyPrefix = "",
    Hosts = new[] { new RedisHost { Host = "114.55.177.197", Port = 6379 } },
    AllowAdmin = true,
    ConnectTimeout = 5000,
    Database = 8,
    PoolSize = 2,
    Name = "Secndary Instance"
};
builder.Services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(connf);
#endregion

#region 限流配置通用配置

builder.Services.AddInMemoryRateLimiting();

builder.Services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

// 配置（解析器、计数器密钥生成器）
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
//解析clientid和ip的使用有用，如果默认没有启用，则此处启用
//services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
#endregion

#region 特殊ip和客户端规则加载
/// <summary>
/// 特殊ip和客户端规则加载
/// </summary>
//builder.Services.AddSingleton<IRateLimitCounterStore, RedisRateLimitCounterStore>();
builder.Services.AddSingleton<IIpPolicyStore, RedisIpPolicyStore>();
//builder.Services.AddSingleton<IClientPolicyStore, RedisClientPolicyStore>(); 
#endregion

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
