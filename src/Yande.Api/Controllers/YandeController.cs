using BeetleX.BNR;
using BeetleX.Redis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Spire.Barcode;
using StackExchange.Profiling;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Yande.Api.SlideCaptcha;
using Yande.Core.AppSettings;
using Yande.Core.Filter;
using Yande.Core.Redis;
using Yande.Core.Service;
using YandeSignApi.Applications.Redis;

namespace Yande.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class YandeController : Controller
    {
        private readonly IRedisDatabase _redisDatabase;
        private readonly ITestAutofac _testAutofac;
        public IWebHostEnvironment _env;
        private readonly ILogger<YandeController> _logger;
        //private readonly IRedisOperationRepository _redisOperationRepository;
        public YandeController(
            ITestAutofac testAutofac,
            IRedisDatabase redisDatabase,
            ILogger<YandeController> logger,
            //IRedisOperationRepository redisOperationRepository,
            IWebHostEnvironment env
            )
        {
            _redisDatabase= redisDatabase;
            _testAutofac = testAutofac;
            _logger = logger;
            _env = env;
            //_redisOperationRepository = redisOperationRepository;
        }

        /// <summary>
        /// MiniProfilerTest
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult MiniProfilerTest()
        {
            using (MiniProfiler.Current.Step("整体测试"))
            {
                using (MiniProfiler.Current.Step("第一个方法"))
                {
                    using (MiniProfiler.Current.CustomTiming("SQL", "SELECT * FROM Table"))
                    {

                        Thread.Sleep(500);
                    }
                }
                using (MiniProfiler.Current.Step("第二个方法"))
                {
                    using (MiniProfiler.Current.CustomTiming("子方法1", "GET "))
                    {
                        Thread.Sleep(700);
                    }


                    using (MiniProfiler.Current.CustomTiming("子方法2", "GET "))
                    {
                        Thread.Sleep(800);
                    }
                }
            }

            return Ok();
        }

        [HttpGet]
        public IActionResult Setcalss()
        {

            TestClass testClass= new TestClass();
            testClass.ClassNo = 1;
            testClass.ClassDesc = "ClassDesc";
            testClass.Students = new List<Student>()
            {
                new Student(){ name="yyy",age=11},
                new Student(){ name="yyy",age=11},
                new Student(){ name="55",age=55}
            };

            _redisDatabase.AddAsync("myCacheKey", testClass)
            .GetAwaiter()
            .GetResult();

            var res = _redisDatabase.GetAsync<TestClass>("myCacheKey")
            .GetAwaiter()
            .GetResult();

            return Ok();
        }

        public class TestClass 
        { 
        
           public int ClassNo { get; set; }

            public string ClassDesc { get; set; }

            public List<Student> Students { get; set; }

        }
        public class Student
        {
            public string name { get; set; }

            public int age { get; set; }

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


            path = "D:\\MyPlayground\\Yande.Api\\wps.json";
            swaggerHelper.Run(path);

            return Ok();
        }

        ///// <summary>
        ///// 获取活动验证码
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public IActionResult GetCaptcha()
        //{
        //    Captcha64Model model = Captcha.GenerateBase64();
        //    _redisOperationRepository.Set("sliderX", model.X,TimeSpan.FromMinutes(1));
        //    Hashtable ht = new Hashtable();
        //    ht.Add("background", model.Background);
        //    ht.Add("slider", model.Slide);
        //    ht.Add("sliderXXXXX", model.X);
        //    return Json(ht);
        //}

        ///// <summary>
        ///// 检查验证
        ///// </summary>
        ///// <param name="x"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public IActionResult CheckCaptcha(int x = 0)
        //{
        //    Hashtable hs = new Hashtable();
        //    string Mess = "";
        //    int Code = 0;
        //    var session = _redisOperationRepository.Get("sliderX");
        //    if (session == null)
        //    {
        //        Mess = "请刷新验证码";
        //        Code = 500;
        //        goto block;
        //    }
        //    string sliderXStr = session.Result;
        //    int sliderX = Convert.ToInt32(sliderXStr);
        //    int difX = sliderX - x;
        //    if (difX >= 0 - Config.blod && difX <= Config.blod)
        //    {
        //        Mess = "success";
        //        Code = 0;
        //    }
        //    else
        //    {
        //        Mess = "错误";
        //        Code = 500;
        //    }
        //block:
        //    hs.Add("Mess", Mess);
        //    hs.Add("Code", Code);
        //    return Json(hs);
        //}
        public class User
        {
            public int id { get; set; }
            public string name { get; set; }
            public int age { get; set; }
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
            for (int i = 0; i < 10; i++)
            {
                User user = new User();
                user.id = i;
                user.name = "yanh";
                user.age = i;
                // event:ping event是事件字段名，冒号后面是事件名称，不要忘了\n换行符。
                await HttpContext.Response.WriteAsync($"event:ping\n");

                // data: 是数据字段名称，冒号后面是数据字段内容。注意数据内容仅仅支持UTF-8，不支持二进制格式。
                // data后面的数据当然可以传递JSON String的。
                await HttpContext.Response.WriteAsync($"data:{JsonConvert.SerializeObject(user)}\r\r");

                // 写入数据到响应后不要忘记 FlushAsync()，因为该api方法是异步的，所以要全程异步，调用同步方法会报错。
                await HttpContext.Response.Body.FlushAsync();

                //模拟一个1秒的延迟。
                await Task.Delay(1000);
            }

            //数据发送完毕后关闭连接。
            //Response.Body.Close();
        }
        #endregion

        /// <summary>
        /// 生成条形码二维码
        /// </summary>
        /// https://mp.weixin.qq.com/s/rO2-a7GdMKxfrq4mqvYrxg
        /// <returns></returns>
        public IActionResult BarCode()
        {
            //创建 BarcodeSettings对象
            BarcodeSettings settings = new BarcodeSettings();
            //设置条形类型为EAN-13
            settings.Type = BarCodeType.EAN13;
            //设置条形码数据
            settings.Data = "58465157484";
            //使用校检
            settings.UseChecksum = CheckSumMode.ForceEnable;
            //在底部显示条形码数据
            settings.ShowTextOnBottom = true;
            //设置宽度
            settings.X = 1f;
            //初始化 BarcodeSetting对象,传入以上设置
            BarCodeGenerator generator = new BarCodeGenerator(settings);
            //创建条形码图片并保存为png格式
            Image image = generator.GenerateImage();
            image.Save("111.png", System.Drawing.Imaging.ImageFormat.Png);
            return Ok();
        }

        /// <summary>
        /// 根据规则生成订单号
        /// </summary>
        /// https://mp.weixin.qq.com/s/v1WVywxT8b2-JMBilarEOA
        /// <returns></returns>
        public async Task<IActionResult> RandomCode()
        {
            DefaultRedis.Instance.Host.AddWriteHost("116.62.214.239");
            BNRFactory.Default.Register("redis", new RedisSequenceParameter(DefaultRedis.Instance));
            string[] citys = new string[] { "广州", "深圳", "上海", "北京" };
            foreach (var item in citys)
            {
                Console.WriteLine(await BNRFactory.Default.Create($"[CN:{item}][N:[CN:{item}]/0000000]"));
            }

            foreach (var item in citys)
            {
                Console.WriteLine(await BNRFactory.Default.Create($"[CN:{item}][redis:city/0000000]"));
            }
            Console.Read();

            return Ok();
        }
    }
}
