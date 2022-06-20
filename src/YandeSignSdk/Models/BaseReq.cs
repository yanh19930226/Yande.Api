using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandeSignSdk.Models
{
    /// <summary>
    /// BaseRequset
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRequset<T, K>
    {
        public BaseRequset(T param)
        {
            Param = param;
        }
        /// <summary>
        /// 请求体
        /// </summary>
        public T Param { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public abstract string Uri { get; }

    }

    /// <summary>
    /// BaseResponseModel
    /// </summary>
    public class BaseResponse<T>
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public CallBackResultCode Code { get; set; }
        /// <summary>
        /// 响应信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 成功
        /// </summary>
        public bool IsSuccess => Code == CallBackResultCode.Succeed;
        /// <summary>
        /// 时间戳(毫秒)
        /// </summary>
        public long Timestamp { get; } = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        /// <summary>
        /// 返回结果
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// 状态值
    /// </summary>
    public enum CallBackResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Succeed = 0,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Failed = 1
    }
}
