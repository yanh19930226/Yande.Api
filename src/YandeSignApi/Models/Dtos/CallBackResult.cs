﻿using System;
using System.ComponentModel;

namespace YandeSignApi.Models.Dtos
{
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
    /// <summary>
    /// 响应实体
    /// </summary>
    public class CallBackResult
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
        /// 响应成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void Success(string message = "")
        {
            Message = message;
            Code = CallBackResultCode.Succeed;
        }
        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void Failed(string message = "")
        {
            Message = message;
            Code = CallBackResultCode.Failed;
        }
        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="exexception></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void Failed(Exception exception)
        {
            Message = exception.InnerException?.StackTrace;
            Code = CallBackResultCode.Failed;
        }
    }
    /// <summary>
    /// 响应实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CallBackResult<T> : CallBackResult
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public void Success(T result = default(T), string message = "")
        {
            Message = message;
            Code = CallBackResultCode.Succeed;
            Data = result;
        }
    }
}
