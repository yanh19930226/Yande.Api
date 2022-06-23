using System;
using System.Collections.Generic;
using System.Text;

namespace FeiShuSdk
{
    public interface IFeishuRequest<out T> where T : BaseResponse
    {
        public string GetUrl();
        public string GetHttpMethod();

        /// <summary>
        /// 获取所有的Key-Value形式的文本请求参数字典。
        /// </summary>
        IDictionary<string, object> GetParameters();

        /// <summary>
        /// 获取自定义HTTP请求头参数。
        /// </summary>
        IDictionary<string, string> GetHeaderParameters();
        /// <summary>
        /// 获取路径参数 已：路径参数名称  在url中
        /// </summary>
        /// <returns></returns>
        IDictionary<string, string> GetPathParameters();
    }
}
