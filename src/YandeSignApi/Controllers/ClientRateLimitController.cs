using AspNetCoreRateLimit;
using InitQ.Abstractions;
using InitQ.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using YandeSignApi.Applications.Redis;
using YandeSignApi.Applications.RedisMq;

namespace YandeSignApi.Controllers
{
	/// <summary>
	/// IpClientController
	/// </summary>
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ClientRateLimitController : Controller,IRedisSubscribe
	{
		private readonly ClientRateLimitOptions _options;
		private readonly IClientPolicyStore _clientPolicyStore;
		private readonly IRedisOperationRepository _redisOperationRepository;
		
		/// <summary>
		/// ClientRateLimitController
		/// </summary>
		/// <param name="optionsAccessor"></param>
		/// <param name="clientPolicyStore"></param>
		public ClientRateLimitController(IOptions<ClientRateLimitOptions> optionsAccessor, IClientPolicyStore clientPolicyStore, IRedisOperationRepository redisOperationRepository)
		{
			_redisOperationRepository = redisOperationRepository;
			_options = optionsAccessor.Value;
			_clientPolicyStore = clientPolicyStore;
		}
		/// <summary>
		/// Get
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ClientRateLimitPolicy> Get()
		{
			return await _clientPolicyStore.GetAsync($"{_options.ClientPolicyPrefix}_cl-key-1");
		}
		/// <summary>
		/// Post
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task Post()
		{
			var id = $"{_options.ClientPolicyPrefix}_cl-key-1";
			var clPolicy = await _clientPolicyStore.GetAsync(id);
			clPolicy.Rules.Add(new RateLimitRule
			{
				Endpoint = "*/api/testpolicyupdate",
				Period = "1h",
				Limit = 100
			});
			await _clientPolicyStore.SetAsync(id, clPolicy);
		}
		/// <summary>
		/// RedisMq发布订阅
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task RedisMq()
		{
			await _redisOperationRepository.ListRightPushAsync(RedisMessageQueueKey.TestSubscribeQueue, "test");
		}

		/// <summary>
		/// RedisMq延迟
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task RedisMqDelay()
		{
			var dtNow = DateTime.Now;

			await _redisOperationRepository.SortedSetAddAsync(RedisMessageQueueKey.TestSubscribeDelayQueue, $"{dtNow.ToString("yyyy-MM-dd HH:mm:ss")}", dtNow.AddSeconds(10));
		}


		[HttpGet]
		[Subscribe(RedisMessageQueueKey.TestSubscribeQueue)]
		public async Task SetBusinessSettlerEvent(string msg)
		{
			var response = Response;

			HttpContext.Response.Headers.Add("Content-Type", "text/event-stream");
			await HttpContext.Response.WriteAsync($"id: {Guid.NewGuid().ToString()}\n");
			await HttpContext.Response.WriteAsync($"event:SetBusinessSettlerEvent\n");
			await HttpContext.Response.WriteAsync($"retry: 3\n");
			await HttpContext.Response.WriteAsync($"data: {111}\r\r");

			await HttpContext.Response.Body.FlushAsync();

			Response.Body.Close();
		}

	}
}
