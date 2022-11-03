using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    class Aeptest
    {
        //参数tttt: 类型String, 参数可以为空
        //  描述:
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string testapi(string appKey, string appSecret, string body, string tttt = "")
        {
            string path = "/aeptest/testapi";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("tttt", tttt);

            string version = "20210324113402";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        public static string testapi_local(string appKey, string appSecret)
        {
            string path = "/aeptest/testapi_local";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20200814112329";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }

    }
}
