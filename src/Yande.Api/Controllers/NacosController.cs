using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Yande.Api.Models;

namespace Yande.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NacosController : Controller
    {
        private readonly ILogger<NacosController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOptionsSnapshot<UserInfo> _options;
        private readonly IOptionsSnapshot<AppSettings> _soptions;

        public NacosController(ILogger<NacosController> logger, IConfiguration configuration, IOptionsSnapshot<UserInfo> options, IOptionsSnapshot<AppSettings> sOptions)
        {
            _logger = logger;
            _configuration = configuration;
            _options = options;
            _soptions=sOptions;
        }
        [HttpGet("getconfig")]
        public UserInfo GetConfig()
        {
            var userInfo1 = _configuration.GetSection("UserInfo").Get<UserInfo>();
            var commonvalue = _configuration["commonkey"];
            var demovalue = _configuration["demokey"];
            _logger.LogInformation("commonkey:" + commonvalue);
            _logger.LogInformation("demokey:" + demovalue);
            return userInfo1;
        }

        /// <summary>
        /// GetConfigBinding
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetConfigBinding")]
        public UserInfo GetConfigBinding()
        {
            var userInfo1 = _options.Value;
            return userInfo1;
        }

        /// <summary>
        /// GetYaml
        /// </summary>
        [HttpGet("GetYaml")]
        public void GetYaml()
        {
            var conn = _configuration.GetConnectionString("Default");

            var version = _configuration["version"];

            var str1 = Newtonsoft.Json.JsonConvert.SerializeObject(_soptions.Value);

        }
    }
}
