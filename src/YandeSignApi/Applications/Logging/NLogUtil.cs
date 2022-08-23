using NLog;
using NLog.Config;
using SqlSugar;
using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace YandeSignApi.Applications.Logs
{
    /// <summary>
    /// LogType
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// Web
        /// </summary>
        [Description("网站")]
        Web,
        /// <summary>
        /// DataBase
        /// </summary>
        [Description("数据库")]
        DataBase,
        /// <summary>
        /// ApiRequest
        /// </summary>
        [Description("Api接口")]
        ApiRequest,
        /// <summary>
        /// Middleware
        /// </summary>
        [Description("中间件")]
        Middleware,
        /// <summary>
        /// Other
        /// </summary>
        [Description("其他")]
        Other,
        /// <summary>
        /// Swagger
        /// </summary>
        [Description("Swagger")]
        Swagger,
        /// <summary>
        /// Task
        /// </summary>
        [Description("定时任务")]
        Task
    }
    /// <summary>
    /// NLogUtil
    /// </summary>
    public static class NLogUtil
    {
        //private static readonly Logger DbLogger = LogManager.GetLogger("logdb");
        private static readonly ILogger FileLogger = LogManager.GetLogger("logfile");
        //private static readonly string ConnectionString = AppSettingsConstVars.DbSqlConnection;
        //private static readonly string DbTypeString = AppSettingsConstVars.DbDbType;

        /// <summary>
        /// 同时写入到日志到数据库和文件
        /// </summary>
        /// <param name="logLevel">日志等级</param>
        /// <param name="logType">日志类型</param>
        /// <param name="logTitle">标题（255字符）</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public static void WriteAll(LogLevel logLevel, LogType logType, string logTitle, string message, Exception exception = null)
        {
            //先存文件
            WriteFileLog(logLevel, logType, logTitle, message, exception);
            ////后存数据库
            //WriteDbLog(logLevel, logType, logTitle, message, exception);
        }

        /// <summary>
        /// 写日志到文件
        /// </summary>
        /// <param name="logLevel">日志等级</param>
        /// <param name="logType">日志类型</param>
        /// <param name="logTitle">标题（255字符）</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public static void WriteFileLog(LogLevel logLevel, LogType logType, string logTitle, string message, Exception exception = null)
        {
            LogEventInfo theEvent = new LogEventInfo(logLevel, FileLogger.Name, message);
            theEvent.Properties["LogType"] = logType.ToString();
            theEvent.Properties["LogTitle"] = logTitle;
            theEvent.Exception = exception;
            FileLogger.Log(theEvent);
        }

        #region 日志写入数据库
        ///// <summary>
        ///// 写日志到数据库
        ///// </summary>
        ///// <param name="logLevel">日志等级</param>
        ///// <param name="logType">日志类型</param>
        ///// <param name="logTitle">标题（255字符）</param>
        ///// <param name="message">信息</param>
        ///// <param name="exception">异常</param>
        //public static void WriteDbLog(LogLevel logLevel, LogType logType, string logTitle, string message, Exception exception = null)
        //{
        //    LogEventInfo theEvent = new LogEventInfo(logLevel, DbLogger.Name, message);
        //    theEvent.Properties["LogType"] = logType.ToString();
        //    theEvent.Properties["LogTitle"] = logTitle;
        //    theEvent.Exception = exception;
        //    DbLogger.Log(theEvent);
        //}

        ///// <summary>
        ///// 确保NLog配置文件sql连接字符串正确
        ///// </summary>
        ///// <param name="nlogPath"></param>
        //public static void EnsureNlogConfig(string nlogPath)
        //{
        //    var dbProvider = DbTypeString == DbType.MySql.ToString() ? "MySql.Data.MySqlClient.MySqlConnection,Mysql.Data" : "Microsoft.Data.SqlClient.SqlConnection, Microsoft.Data.SqlClient";

        //    XDocument xd = XDocument.Load(nlogPath);
        //    if (xd.Root.Elements().FirstOrDefault(a => a.Name.LocalName == "targets")
        //        is XElement targetsNode && targetsNode != null &&
        //        targetsNode.Elements().FirstOrDefault(a => a.Name.LocalName == "target" && a.Attribute("name").Value == "log_database")
        //        is XElement targetNode && targetNode != null)
        //    {
        //        if (!targetNode.Attribute("connectionString").Value.Equals(ConnectionString) || !targetNode.Attribute("dbProvider").Value.Equals(dbProvider))
        //        {
        //            if (!targetNode.Attribute("connectionString").Value.Equals(ConnectionString))
        //            {
        //                targetNode.Attribute("connectionString").Value = ConnectionString;
        //            }
        //            if (!targetNode.Attribute("dbProvider").Value.Equals(dbProvider))
        //            {
        //                targetNode.Attribute("dbProvider").Value = dbProvider;
        //            }

        //            xd.Save(nlogPath);
        //            //编辑后重新载入配置文件（不依靠NLog自己的autoReload，有延迟）
        //            LogManager.Configuration = new XmlLoggingConfiguration(nlogPath);
        //        }
        //    }
        //}
        #endregion
    }
}
