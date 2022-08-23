using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YandeSignApi.Applications.Logs;

namespace YandeSignApi.Controllers
{
    /// <summary>
    /// IpClientController
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IpClientController : Controller
    {

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
            //_logger.Log(LogLevel.Warning, "测试");
            //_logger.LogInformation("日志组件测试");
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
