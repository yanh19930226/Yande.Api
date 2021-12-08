using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        private readonly ILogger<YandeController> _logger;
        //private readonly IRedisManager _redisManager;
        public YandeController(ITestAutofac testAutofac, ILogger<YandeController> logger/*, IRedisManager redisManager*/)
        {
            _testAutofac = testAutofac;
            _logger = logger;
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
    }
}
