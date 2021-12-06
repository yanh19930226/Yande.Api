using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yande.Core.Filter;
using Yande.Core.Service;

namespace Yande.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class YandeController : Controller
    {
        private readonly ITestAutofac _testAutofac;
        private readonly ILogger<YandeController> _logger;
        public YandeController(ITestAutofac testAutofac, ILogger<YandeController> logger)
        {
            _testAutofac = testAutofac;
            _logger = logger;
        }
        [HttpPost]
        [HelloFilter]
        public IActionResult Hellow(string str)
        {
            var returnVal = "Hellow World";

            _testAutofac.Test();

            _logger.LogInformation("Info Message……");
            _logger.LogWarning("Warning Message……");
            _logger.LogDebug("Debug Message……");
            _logger.LogError("Error Message……");

            return Ok(returnVal);
        }
    }
}
