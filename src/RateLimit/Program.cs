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

#region ����Զ�������ԴRedis

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
/// ��������
/// </summary>
IConfiguration Configuration = configurationBuilder.Build();
#endregion

#region Redis���������
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

#region ��������ͨ������

builder.Services.AddInMemoryRateLimiting();

builder.Services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

// ���ã�����������������Կ��������
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
//����clientid��ip��ʹ�����ã����Ĭ��û�����ã���˴�����
//services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
#endregion

#region ����ip�Ϳͻ��˹������
/// <summary>
/// ����ip�Ϳͻ��˹������
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

//ʹ��Ip����
app.UseIpRateLimiting();
//app.UseClientRateLimiting();

app.UseAuthorization();

app.MapControllers();

app.Run();
