using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yande.Core.AppSettings;
using Yande.Core.Filter;
using Yande.Core.Redis;
using Yande.Core.Service;

namespace Yande.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class YandeController : Controller
    {
        private readonly ITestAutofac _testAutofac;
        public IWebHostEnvironment _env;
        private readonly ILogger<YandeController> _logger;
        //private readonly IRedisManager _redisManager;
        public YandeController(ITestAutofac testAutofac, ILogger<YandeController> logger, IWebHostEnvironment env/*, IRedisManager redisManager*/)
        {
            _testAutofac = testAutofac;
            _logger = logger;
            _env = env;
            //_redisManager = redisManager;
        }
        [HttpPost]
        [HelloFilter]
        public IActionResult Hellow(string str)
        {
            var returnVal = "Hellow World";

            _testAutofac.Test();

            //_redisManager.Set("aaa", "aaa",TimeSpan.FromMinutes(1));

            //_redisManager.Get("aaa");

            _logger.LogInformation("Info Message……");
            _logger.LogWarning("Warning Message……");
            _logger.LogDebug("Debug Message……");
            _logger.LogError("Error Message……");

            string a = AppHelper.ReadAppSettings("Test", "A");
            string b = AppHelper.ReadAppSettings("Test", "B", "C");
            return Ok($"{a} ***** {b}");
        }

        /// <summary>
        /// SwaggerHelper
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IActionResult SwaggerHelper(string path)
        {
            SwaggerHelper swaggerHelper = new SwaggerHelper();

            swaggerHelper.Run(path);

            return Ok();
        }

        #region 基于ASP.NET Core api 的服务器事件发送
        /// <summary>
        /// 基于ASP.NET Core api 的服务器事件发送
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task GetValue()
        {
            //测试，debug到这里的时候你会发现，协议使用的是HTTP/2. APS.NET Core 2.1以上就默认支持HTTP/2，无需额外的配置。再Windows Server2016/Windows10+会自动提供支持。
            string requestProtocol = HttpContext.Request.Protocol;
            var response = Response;
            //响应头部添加text/event-stream，这是HTTP/2协议的一部分。
            response.Headers.Add("Content-Type", "text/event-stream");
            for (int i = 0; i < 100; i++)
            {
                // event:ping event是事件字段名，冒号后面是事件名称，不要忘了\n换行符。
                await HttpContext.Response.WriteAsync($"event:ping\n");

                // data: 是数据字段名称，冒号后面是数据字段内容。注意数据内容仅仅支持UTF-8，不支持二进制格式。
                // data后面的数据当然可以传递JSON String的。
                await HttpContext.Response.WriteAsync($"data:Controller {i} at {DateTime.Now}\r\r");

                // 写入数据到响应后不要忘记 FlushAsync()，因为该api方法是异步的，所以要全程异步，调用同步方法会报错。
                await HttpContext.Response.Body.FlushAsync();

                //模拟一个1秒的延迟。
                await Task.Delay(1000);
            }

            //数据发送完毕后关闭连接。
            Response.Body.Close();
        } 
        #endregion
    }
}
