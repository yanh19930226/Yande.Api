using Microsoft.AspNetCore.Mvc;

namespace YandeSignApi.Controllers
{
    /// <summary>
    /// IpClientController
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IpClientController : Controller
    {
        /// <summary>
        /// Str
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Str()
        {
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
