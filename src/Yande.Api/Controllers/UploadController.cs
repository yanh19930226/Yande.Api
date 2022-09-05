using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using OfficeToPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileInfo = OfficeToPdf.FileInfo;

namespace Yande.Api.Controllers
{
    /// <summary>
    /// UploadController
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadController : Controller
    {

        private IMongoDatabase _db;
        private int _chunkSize;
        private GridFSBucket _gridFs;


        public string GetMD5Hash(Stream stream)
        {
            string result = "";
            string hashData = "";
            byte[] arrbytHashValue;
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher =
                       new System.Security.Cryptography.MD5CryptoServiceProvider();

            try
            {
                arrbytHashValue = md5Hasher.ComputeHash(stream);//计算指定Stream 对象的哈希值
                //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
                hashData = System.BitConverter.ToString(arrbytHashValue);
                //替换-
                hashData = hashData.Replace("-", "");
                result = hashData;
            }
            catch (System.Exception ex)
            {
                //记录日志
                throw ex;
            }

            return result;
        }


        private FileInfo AddFile(Stream fileStream, string fileName)
        {
            GridFSUploadOptions options = new GridFSUploadOptions();
            options.ChunkSizeBytes = _chunkSize;
            ObjectId id = _gridFs.UploadFromStream(fileName, fileStream, options);
            var briefInfo = GetFileInfo(id.ToString());

            return briefInfo;
        }

        private static byte[] StreamToBytes(Stream stream)
        {
            if (stream == null)
            {
                return null;
            }
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public FileInfo AddFile(Stream fileStream, string fileName, bool isCheckExist = true)
        {
            if (!isCheckExist)
            {
                return AddFile(fileStream, fileName);
            }

            var result = new FileInfo();
            byte[] bytes = StreamToBytes(fileStream);
            //如果md5相同 着不需要再上传
            string md5 = GetMD5Hash(fileStream);
            string id = IsExist(md5.ToLower());
            if (string.IsNullOrEmpty(id))
            {
                var oldStream = BytesToStream(bytes);
                result = AddFile(oldStream, fileName);
            }
            else
            {
                result = GetFileInfo(id);
            }
            return result;
        }

        public bool DeleteFile(string fileId)
        {
            _gridFs.Delete(ObjectId.Parse(fileId));
            return true;
        }

        public string IsExist(string tag)
        {
            string id = "";
            GridFSFindOptions options = new GridFSFindOptions();
            options.Limit = 1;
            var filter = MongoDB.Driver.Builders<GridFSFileInfo>.Filter.Eq("md5", tag);
            var filterDefintion = FilterDefinition<GridFSFileInfo>.Empty;

            var objFile = _gridFs.Find(filter, options);
            if (objFile == null)
                return id;
            var files = objFile.ToList();

            if (files.Count > 0)
            {
                id = files.FirstOrDefault().Id.ToString();
            }
            return id;
        }

        public Stream GetFile(string fileId)
        {
            //ObjectId id = ObjectId.Parse(fileId);
            //byte[] fileByte =  _gridFs.DownloadAsBytes(id);
            //return BytesToStream(fileByte);
            return GetStream(fileId);
        }
        public Stream GetStream(string fileId)
        {
            try
            {
                ObjectId id = ObjectId.Parse(fileId);
                return _gridFs.OpenDownloadStream(id, new GridFSDownloadOptions()
                {
                    Seekable = true
                });
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public string GetFileName(string fileId)
        {
            ObjectId id = ObjectId.Parse(fileId);
            var fileInfo = GetGridFSFileInfo(id);
            if (fileInfo != null)
            {
                return fileInfo.Filename;
            }
            return string.Empty;
        }

        private GridFSFileInfo GetGridFSFileInfo(ObjectId id)
        {
            var filter = MongoDB.Driver.Builders<GridFSFileInfo>.Filter.Eq("_id", id);
            var fileInfo = _gridFs.Find(filter).ToList();
            return fileInfo.FirstOrDefault();
        }

        public string GetMD5(string fileId)
        {
            ObjectId id = ObjectId.Parse(fileId);
            var fileInfo = GetGridFSFileInfo(id);
            if (fileInfo != null)
            {
                return fileInfo.MD5;
            }
            return string.Empty;
        }


        public FileInfo GetFileInfo(string fileId)
        {
            var result = new FileInfo();
            var fileInfo = GetGridFSFileInfo(ObjectId.Parse(fileId));
            if (fileInfo != null)
            {
                result.Md5 = fileInfo.MD5;
                result.FileId = fileInfo.Id.ToString();
                result.Length = fileInfo.Length;
                result.UploadDateTime = fileInfo.UploadDateTime;
                result.FileName = fileInfo.Filename;
            }
            return result;
        }

        public FileByteInfo DownloadFile(string fileId)
        {
            FileByteInfo retFile = new FileByteInfo();
            retFile.Bytes = StreamToBytes(this.GetFile(fileId));

            if (retFile == null || retFile.Bytes == null)
                throw new GridFSFileNotFoundException(fileId);

            retFile.FileName = GetFileName(fileId);
            retFile.ContentType = Path.GetExtension(retFile.FileName).ToLower();
            return retFile;
        }

        [Serializable]
        public class Test
        {
            public string Id { get; set; }
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Upload()
        {
            //var file = Request.Form.Files["file"];

            FileInfo fileInfo = new FileInfo(); 

            try
            {
                string hostName = EnvironmentHelper.GetEnvValue("MongoHostName");
                string dbName = EnvironmentHelper.GetEnvValue("MongoDBName");
                string mongoPort = EnvironmentHelper.GetEnvValue("MongoPort");
                string mongoUser = EnvironmentHelper.GetEnvValue("MongoUserName");
                string mongoPwd = EnvironmentHelper.GetEnvValue("MongoPassword");
                _chunkSize = 261120;
                //int.TryParse((ConfigurationManager.AppSettings["MongoChunkSize"] + string.Empty), out _chunkSize);//261120
                List<MongoServerAddress> mongoServerAddress = new List<MongoServerAddress>();

                MongoClient client = null;
                if (string.IsNullOrWhiteSpace(mongoUser))
                {
                    //多host用,分隔
                    for (int i = 0; i < hostName.Split(',').Length; i++)
                    {
                        string host = hostName.Split(',')[i];
                        string port = mongoPort.Split(',')[i];
                        mongoServerAddress.Add(new MongoServerAddress(host, int.Parse(port)));
                    }

                    MongoClientSettings mcs = new MongoClientSettings();
                    mcs.Servers = mongoServerAddress;
                    client = new MongoClient(mcs);
                }
                else
                {
                    List<string> hosts = new List<string>();
                    for (int i = 0; i < hostName.Split(',').Length; i++)
                    {
                        string host = hostName.Split(',')[i];
                        if (!string.IsNullOrWhiteSpace(host))
                        {
                            string port = mongoPort.Split(',')[i];
                            hosts.Add($"{host}:{port}");     //127.0.0.1:27017
                        }

                    }
                    string strHost = string.Join(",", hosts);   //127.0.0.1:27017,127.0.0.1:28017
                    string connectionString1 = $"mongodb://{mongoUser}:{mongoPwd}@{strHost}/{dbName}?authSource=admin";
                    client = new MongoClient(connectionString1);
                }
                _db = client.GetDatabase(dbName);
                _gridFs = new GridFSBucket(_db);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't find MongoHostName!!!", ex);
            }

            FileStream fileStream = new FileStream(@"D:\big.docx", FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            Stream stream = new MemoryStream(bytes);

            fileInfo = AddFile(stream, "big.docx", true);

            #region MyRegion
            try
            {
                var mqManager = new MQManager(new MQConfig
                {
                    AutomaticRecoveryEnabled = true,
                    HeartBeat = 60,
                    NetworkRecoveryInterval = new TimeSpan(60),
                    Host = EnvironmentHelper.GetEnvValue("MQHostName"),
                    UserName = EnvironmentHelper.GetEnvValue("MQUserName"),
                    Password = EnvironmentHelper.GetEnvValue("MQPassword"),
                    Port = EnvironmentHelper.GetEnvValue("MQPort")
                });

                if (mqManager.Connected)
                {

                    Console.WriteLine("RabbitMQ连接成功。");

                    OfficeToPdf.FileInfo fileInforabbit = new OfficeToPdf.FileInfo();

                    fileInforabbit.FileId = fileInfo.FileId;

                    fileInforabbit.FileName = fileInfo.FileName;

                    fileInforabbit.Length = fileInfo.Length;

                    fileInforabbit.Md5 = fileInfo.Md5;

                    fileInforabbit.UploadDateTime = fileInfo.UploadDateTime;

                    ExcelConvertMessage excelConvertMessage = new ExcelConvertMessage(fileInforabbit);

                    mqManager.Publish(excelConvertMessage);

                    Console.WriteLine("发送成功");

                }
                else
                {
                    Console.WriteLine("RabbitMQ连接初始化失败,请检查连接。");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
            }
            #endregion

            return Ok();

        }

        /// <summary>
        /// 文件上传分片上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UploadFile()
        {
            var data = Request.Form.Files["data"];
            string lastModified = Request.Form["lastModified"].ToString();
            var total = Request.Form["total"];
            var fileName = Request.Form["fileName"];
            var index = Request.Form["index"];

            string temporary = Path.Combine(@"E:\浏览器", lastModified);//临时保存分块的目录
            try
            {
                if (!Directory.Exists(temporary))
                    Directory.CreateDirectory(temporary);
                string filePath = Path.Combine(temporary, index.ToString());
                if (!Convert.IsDBNull(data))
                {
                    await Task.Run(() => {
                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        data.CopyTo(fs);
                        fs.Close();
                    });
                }
                bool mergeOk = false;
                if (total == index)
                {
                    mergeOk = await FileMerge(lastModified, fileName);
                }

                Dictionary<string, object> result = new Dictionary<string, object>();
                result.Add("number", index);
                result.Add("mergeOk", mergeOk);
                return Json(result);

            }
            catch (Exception ex)
            {
                Directory.Delete(temporary);//删除文件夹
                throw ex;
            }
        }

        private async Task<bool> FileMerge(string lastModified, string fileName)
        {
            bool ok = false;
            try
            {
                var temporary = Path.Combine(@"E:\浏览器", lastModified);//临时文件夹
                fileName = Request.Form["fileName"];//文件名
                string fileExt = Path.GetExtension(fileName);//获取文件后缀
                var files = Directory.GetFiles(temporary);//获得下面的所有文件
                var finalPath = Path.Combine(@"E:\浏览器", DateTime.Now.ToString("yyMMddHHmmss") + fileExt);//最终的文件名（demo中保存的是它上传时候的文件名，实际操作肯定不能这样）
                var fs = new FileStream(finalPath, FileMode.Create);
                foreach (var part in files.OrderBy(x => x.Length).ThenBy(x => x))//排一下序，保证从0-N Write
                {
                    var bytes = System.IO.File.ReadAllBytes(part);
                    await fs.WriteAsync(bytes, 0, bytes.Length);
                    bytes = null;
                    System.IO.File.Delete(part);//删除分块
                }
                fs.Close();
                Directory.Delete(temporary);//删除文件夹
                ok = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ok;
        }
    }
}
