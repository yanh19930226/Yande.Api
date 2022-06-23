using System;
using System.Collections.Generic;
using System.Text;

namespace FeiShuSdk
{
    public abstract class BaseRequest<T> : IFeishuRequest<T> where T : BaseResponse
    {

        private String httpMethod = "POST";
        private IDictionary<string, string> headerParams;
        private IDictionary<string, string> pathParams;
        public string GetHttpMethod()
        {
            return httpMethod;
        }

        public abstract string GetUrl();

        public IDictionary<string, string> GetHeaderParameters()
        {
            if (this.headerParams == null)
            {
                this.headerParams = new Dictionary<string, string>();
            }
            return this.headerParams;
        }
        public void AddHeaderParameter(string key, string value)
        {
            GetHeaderParameters().Add(key, value);
        }
        /// <summary>
        /// 设置请求
        /// </summary>
        /// <param name="httpMethod">默认POST</param>
        public void SetHttpMethod(String httpMethod)
        {
            this.httpMethod = httpMethod;
        }

        public IDictionary<string, string> GetPathParameters()
        {
            if (this.pathParams == null)
            {
                this.pathParams = new Dictionary<string, string>();
            }
            return this.pathParams;
        }
        public void AddPathParameter(string key, string value)
        {
            GetPathParameters().Add($":{key}", value);
        }


        public abstract IDictionary<string, object> GetParameters();


    }
}
