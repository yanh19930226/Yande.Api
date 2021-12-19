using Microsoft.AspNetCore.Hosting;
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
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<OutPut> FileUp()
        {
            var ret = new OutPut();
            try
            {
                //不能用FromBody
                var dto = JsonConvert.DeserializeObject<ImagesDto>(Request.Form["ImageModelInfo"]);//文件类实体参数
                var files = Request.Form.Files;//接收上传的文件，可能多个 看前台
                if (files.Count > 0)
                {
                    var path = _env.ContentRootPath + @"/Uploads/Images/";//绝对路径
                    string dirPath = Path.Combine(path, dto.Type + "/");//绝对径路 储存文件路径的文件夹
                    if (!Directory.Exists(dirPath))//查看文件夹是否存在
                        Directory.CreateDirectory(dirPath);
                    var file = files.Where(x => true).FirstOrDefault();//只取多文件的一个
                    var fileNam = $"{Guid.NewGuid():N}_{file.FileName}";//新文件名
                    string snPath = $"{dirPath + fileNam}";//储存文件路径
                    using var stream = new FileStream(snPath, FileMode.Create);
                    await file.CopyToAsync(stream);
                    //次出还可以进行数据库操作 保存到数据库
                    ret = new OutPut { Code = 200, Msg = "上传成功", Success = true };
                }
                else//没有图片
                {
                    ret = new OutPut { Code = 400, Msg = "请上传图片", Success = false };
                }
            }
            catch (Exception ex)
            {
                ret = new OutPut { Code = 500, Msg = $"异常：{ex.Message}", Success = false };
            }
            return ret;
        }


        #region 上传视频
        public IActionResult Upload()
        {
            ViewBag.Token = HttpContext.Request.Headers["Authorization"];//获取认证信息，传递给前台，方便Ajax请求时提供
            return View();
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> FileSave()
        {
            var date = Request;
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string fileExt = formFile.FileName.Substring(formFile.FileName.IndexOf('.')); //文件扩展名，不含“.”
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位
                                                     //string newFileName = Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名

                    var path = _env.ContentRootPath + @"/Uploads/Images/";//绝对路径

                    string DirPath = Path.Combine(path , Request.Form["guid"]);
                    if (!Directory.Exists(DirPath))
                    {
                        Directory.CreateDirectory(DirPath);
                    }
                    var filePath = DirPath + "/" + Request.Form["chunk"] + fileExt;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);

                    }
                }
            }
            return Ok(new { count = files.Count, size });
        }

        /// <summary>
        /// 合并请求
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> FileMerge()
        {
            bool ok = false;
            string errmsg = "";
            try
            {
                var path = _env.ContentRootPath + @"/Uploads/Images/";//绝对路径

                var temporary = Path.Combine(path, Request.Form["guid"]);//临时文件夹
                string fileName = Request.Form["fileName"];//文件名
                string fileExt = Path.GetExtension(fileName);//获取文件后缀
                var files = Directory.GetFiles(temporary);//获得下面的所有文件

                var finalFilePath = Path.Combine(path + fileName);//最终的文件名
                //var fs = new FileStream(finalFilePath, FileMode.Create);
                using (var fs = new FileStream(finalFilePath, FileMode.Create))
                {
                    foreach (var part in files.OrderBy(x => x.Length).ThenBy(x => x))
                    {
                        var bytes = System.IO.File.ReadAllBytes(part);
                        await fs.WriteAsync(bytes, 0, bytes.Length);
                        bytes = null;
                        System.IO.File.Delete(part);//删除分块
                    }
                    Directory.Delete(temporary);//删除文件夹
                    ok = true;
                }
            }
            catch (Exception ex)
            {
                ok = false;
                errmsg = ex.Message;
            }
            if (ok)
            {
                return Ok(new { success = true, msg = "" });
            }
            else
            {
                return Ok(new { success = false, msg = errmsg }); ;
            }
        }

        #endregion
    }



    /// <summary>
    /// 返回输出类
    /// </summary>
    public class OutPut
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
    }
    /// <summary>
    /// 接收参数Dto
    /// </summary>
    public class ImagesDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RelationId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
    }
}
