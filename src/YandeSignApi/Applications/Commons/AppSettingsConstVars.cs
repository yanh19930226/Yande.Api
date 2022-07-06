using SqlSugar.Extensions;

namespace YandeSignApi.Applications.Commons
{
    /// <summary>
    /// AppSettingsConstVars
    /// </summary>
    public class AppSettingsConstVars
    {
        #region 数据库================================================================================
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        public static readonly string DbSqlConnection = AppSettingsHelper.GetContent("ConnectionStrings", "SqlConnection");
        /// <summary>
        /// 获取数据库类型
        /// </summary>
        public static readonly string DbDbType = AppSettingsHelper.GetContent("ConnectionStrings", "DbType");
        #endregion

        #region redis================================================================================
        /// <summary>
        /// 获取redis是否开启
        /// </summary>
        public static readonly bool RedisConfigEnabled = AppSettingsHelper.GetContent("RedisConfig", "Enabled").ObjToBool();
        /// <summary>
        /// 获取redis连接字符串
        /// </summary>
        public static readonly string RedisConfigConnectionString = AppSettingsHelper.GetContent("RedisConfig", "ConnectionString");
        #endregion

        #region Middleware中间件================================================================================
        /// <summary>
        /// Ip限流
        /// </summary>
        public static readonly bool MiddlewareIpLogEnabled = AppSettingsHelper.GetContent("Middleware", "IPLog", "Enabled").ObjToBool();
        /// <summary>
        /// 记录请求与返回数据
        /// </summary>
        public static readonly bool MiddlewareRequestResponseLogEnabled = AppSettingsHelper.GetContent("Middleware", "RequestResponseLog", "Enabled").ObjToBool();
        /// <summary>
        /// 用户访问记录-过滤ip
        /// </summary>
        public static readonly string MiddlewareRequestResponseLogIgnoreApis = AppSettingsHelper.GetContent("Middleware", "RequestResponseLog", "IgnoreApis");

        #endregion

    }
}
