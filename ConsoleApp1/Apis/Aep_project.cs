using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    class Aep_project
    {
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string products(string appKey, string appSecret, string body)
        {
            string path = "/aep_project/products";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210623072425";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string devices(string appKey, string appSecret, string body)
        {
            string path = "/aep_project/devices";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210623072440";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }

    }
}
