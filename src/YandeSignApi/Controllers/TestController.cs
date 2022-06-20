using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using YandeSignApi.Models.Dtos;
using YandeSignApi.Models.Dtos.Reps;
using YandeSignApi.Models.Dtos.Reqs;

namespace YandeSignApi.Controllers
{
    /// <summary>
    /// TestController
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : RsaBaseController
    {
        /// <summary>
        /// Test1
        /// </summary>
        /// <returns></returns>
        public IActionResult Test1()
        {
            var appid = Request.HttpContext.User.Claims.FirstOrDefault(o => o.Type == "appid").Value;
            var appname = Request.HttpContext.User.Claims.FirstOrDefault(o => o.Type == "appname").Value;

            return Ok($"appid:{appid},appname:{appname}");
        }

        /// <summary>
        /// Test2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/test/test2")]
        public CallBackResult<TesRep> Test2([FromBody]TestReq request)
        {
            var result=new CallBackResult<TesRep>();

            TesRep tesRep=new TesRep();
            tesRep.Id=request.Id;
            tesRep.Name = "返回数据";
            result.Success(tesRep);
            return result;
        }
    }
}
