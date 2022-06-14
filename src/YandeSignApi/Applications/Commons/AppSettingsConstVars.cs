using SqlSugar.Extensions;

namespace YandeSignApi.Applications.Commons
{
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

    }
}
