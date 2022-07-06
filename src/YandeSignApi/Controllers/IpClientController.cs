using Microsoft.AspNetCore.Mvc;

namespace YandeSignApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IpClientController : Controller
    {
        /// <summary>
        /// Str
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/ipclient/str")]
        public string Str()
        {
            return "test";
        }
        /// <summary>
        /// Str2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/ipclient/str2")]
        public string Str2()
        {
            return "test2";
        }
        /// <summary>
        /// Str3
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/ipclient/str3")]
        public string Str3()
        {
            return "test3";
        }
    }
}
