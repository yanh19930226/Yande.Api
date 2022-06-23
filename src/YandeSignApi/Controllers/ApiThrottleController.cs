using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace YandeSignApi.Controllers
{
    //public class ApiThrottleController : Controller
    //{
    //    /// <summary>
    //    /// Api限流管理服务
    //    /// </summary>
    //    private readonly IApiThrottleService _service;

    //    public ApiThrottleController(IApiThrottleService service)
    //    {
    //        _service = service;
    //    }

    //    /// <summary>
    //    /// 取得客户端IP地址
    //    /// </summary>
    //    private static string GetIpAddress(HttpContext context)
    //    {
    //        var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
    //        if (string.IsNullOrEmpty(ip))
    //        {
    //            ip = context.Connection.RemoteIpAddress.ToString();
    //        }
    //        return ip;
    //    }


    //    /// <summary>
    //    /// 添加黑名单
    //    /// </summary>
    //    /// <returns></returns>
    //    [HttpPost]
    //    [BlackListValve(Policy = Policy.Ip)]
    //    public async Task AddBlackList()
    //    {
    //        var ip = GetIpAddress(HttpContext);
    //        //添加IP黑名单
    //        await _service.AddRosterAsync(RosterType.BlackList,
    //            "WebApiTest.Controllers.ValuesController.AddBlackList",
    //            Policy.Ip, null, TimeSpan.FromSeconds(60), ip);
    //    }

        
    //}
}
