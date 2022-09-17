using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YandeSignApi.Applications.Logs;
using YandeSignApi.Applications.Redis;
using YandeSignApi.Applications.RedisMq;

namespace YandeSignApi.Controllers
{
    /// <summary>
    /// IpClientController
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IpClientController : Controller
    {

        private readonly IRedisOperationRepository _redisOperationRepository;
        public IpClientController(IRedisOperationRepository redisOperationRepository)
        {
            _redisOperationRepository=redisOperationRepository;
        }


        private readonly ILogger<IpClientController> _logger;
        public IpClientController(ILogger<IpClientController> logger)
        {
            _logger=logger;
        }
        /// <summary>
        /// Str
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Str()
        {

            NLogUtil.WriteFileLog(NLog.LogLevel.Error, LogType.Web, "网站启动", "网站启动成功");

            NLogUtil.WriteFileLog(NLog.LogLevel.Info, LogType.Web, "网站启动", "网站启动成功");

            return "test";
        }
        /// <summary>
        /// Str2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Str2()
        {
            return "test2";
        }
        /// <summary>
        /// Str3
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Str3()
        {
            return "test3";
        }
        

    }
}
