using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeiShuSdk
{
    public class BaseResponse
    {
        /// <summary>
        /// 返回码
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        [JsonProperty("msg")]
        public string Msg { get; set; }
    }
}
