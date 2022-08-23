using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using YandeSignApi.Data;
using YandeSignApi.Models.ShardingCore;

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
        private readonly DefaultDbContext _defaultDbContext;
        /// <summary>
        /// EnviromentController
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="defaultDbContext"></param>
        public EnviromentController(
            IConfiguration configuration,
            DefaultDbContext defaultDbContext
            )
        {
            Configuration = configuration;
            _defaultDbContext=defaultDbContext;
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

        /// <summary>
        /// ShardingCoreQuery
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Query()
        {
            var list = await _defaultDbContext.Set<OrderByHour>().ToListAsync();
            return Ok(list);
        }

        /// <summary>
        /// ShardingCoreInsert
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            var orderByHour = new OrderByHour();
            orderByHour.Id = Guid.NewGuid().ToString("n");
            orderByHour.Name = $"Name:" + Guid.NewGuid().ToString("n");
            var dateTime = DateTime.Now;
            orderByHour.CreateTime = dateTime;
            await _defaultDbContext.AddAsync(orderByHour);
            await _defaultDbContext.SaveChangesAsync();
            return Ok();
        }


    }
}
