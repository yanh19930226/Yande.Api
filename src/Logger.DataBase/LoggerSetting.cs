using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Logger.DataBase
{
    public class LoggerSetting
    {

        /// <summary>
        /// 项目
        /// </summary>
        public string Project { get; set; } = Assembly.GetEntryAssembly()?.GetName().Name!;

    }
}
