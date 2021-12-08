using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yande.Core.WebHelper
{
    public interface IHttpClientHelper
    {
        /// <summary>
        /// 通用访问webapi方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        T Post<T>(string url, string data);

        /// <summary>
        /// 带用户名和密码的通用访问webapi方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="account">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        T Post<T>(string url, string data, string account, string pwd);

    }
}
