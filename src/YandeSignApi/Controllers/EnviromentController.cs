using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace YandeSignApi.Controllers
{
    /// <summary>
    /// 环境控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EnviromentController : Controller
    {
        private readonly IConfiguration Configuration;
        /// <summary>
        /// EnviromentController
        /// </summary>
        /// <param name="configuration"></param>
        public EnviromentController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 读取不同文件的配置文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCustomConfig()
        {
            return Content($"读取CustomConfig配置为:{Configuration["CustomConfig"]};读取公共配置为:{Configuration["CommonConfig"]}");
        }
    }
}
