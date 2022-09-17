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
	}
}
