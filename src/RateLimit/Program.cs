using AspNetCoreRateLimit;
using RateLimit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRedisSetup();

builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitCounterStore, RedisRateLimitCounterStore>();
builder.Services.AddSingleton<IIpPolicyStore, RedisIpPolicyStore>();
builder.Services.AddSingleton<IClientPolicyStore, RedisClientPolicyStore>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIpRateLimiting();
app.UseClientRateLimiting();

app.UseAuthorization();

app.MapControllers();

app.Run();
