using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    public class Iodm
    {
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string nbSubManageData(string appKey, string appSecret, string body)
        {
            string path = "/iodm/app/sub/v1.1.0/subscribe";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210721031158";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }

    }
}
