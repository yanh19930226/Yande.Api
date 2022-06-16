using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using YandeSignApi.Models.Dtos.Reqs;

namespace YandeSignApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : RsaBaseController
    {
        public IActionResult Test1()
        {
            var appid = Request.HttpContext.User.Claims.FirstOrDefault(o => o.Type == "appid").Value;
            var appname = Request.HttpContext.User.Claims.FirstOrDefault(o => o.Type == "appname").Value;

            return Ok($"appid:{appid},appname:{appname}");
        }

        [HttpPost]
        [Route("/test/test2")]
        public IActionResult Test2([FromBody]TestReq request)
        {
            return Ok(JsonConvert.SerializeObject(request));
        }
    }
}
