using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace YandeSignApi.Controllers
{
	/// <summary>
	/// IpClientController
	/// </summary>
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ClientRateLimitController : Controller
	{
		private readonly ClientRateLimitOptions _options;
		private readonly IClientPolicyStore _clientPolicyStore;
		/// <summary>
		/// ClientRateLimitController
		/// </summary>
		/// <param name="optionsAccessor"></param>
		/// <param name="clientPolicyStore"></param>
		public ClientRateLimitController(IOptions<ClientRateLimitOptions> optionsAccessor, IClientPolicyStore clientPolicyStore)
		{
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
