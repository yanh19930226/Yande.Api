using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Linq;

namespace YandeSignApi.Applications.Commons
{
    /// <summary>
    /// 获取Appsettings配置信息
    /// </summary>
    public class AppSettingsHelper
    {
        static IConfiguration Configuration { get; set; }

        public AppSettingsHelper(string contentPath)
        {
            //如果你把配置文件 是 根据环境变量来分开了，可以这样写
            //Path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";
            string Path = "appsettings.json";
            Configuration = new ConfigurationBuilder().SetBasePath(contentPath).Add(new JsonConfigurationSource { Path = Path, Optional = false, ReloadOnChange = true }).Build();
        }

        /// <summary>
        /// 封装要操作的字符
        /// AppSettingsHelper.GetContent(new string[] { "JwtConfig", "SecretKey" });
        /// </summary>
        /// <param name="sections">节点配置</param>
        /// <returns></returns>
        public static string GetContent(params string[] sections)
        {
            try
            {

                if (sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (Exception) { }

            return "";
        }

    }
}
