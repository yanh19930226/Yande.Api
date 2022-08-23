using Logger.LocalFile;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YandeSignApi.Applications.Logs;

namespace YandeSignApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            try
            {
                #region д�����ݿ���Ҫ
                ////ȷ��NLog.config�������ַ�����appsettings.json��ͬ��
                //NLogUtil.EnsureNlogConfig("NLog.config"); 
                #endregion

                //������Ŀ����ʱ��Ҫ��������
                NLogUtil.WriteFileLog(NLog.LogLevel.Trace, LogType.Web, "��վ����", "��վ�����ɹ�");
                host.Run();
            }
            catch (Exception ex)
            {
                //ʹ��nlogд��������־�ļ�����һ���ݿ�û����/���ӳɹ���
                NLogUtil.WriteFileLog(NLog.LogLevel.Error, LogType.Web, "��վ����", "��ʼ�������쳣", ex);
                throw;
            }
        }
        /// <summary>
        /// CreateHostBuilder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                #region �Զ���Log����
                //������־�ļ���¼
                //logging.AddLocalFileLogger(options => { options.SaveDays = 7; });
                #endregion

                #region Nlog����
                //�Ƴ��Ѿ�ע���������־�������
                logging.ClearProviders();
                //������С����־����
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                #endregion
            })
            .UseNLog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
