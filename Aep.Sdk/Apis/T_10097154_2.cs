using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    public class T_10097154_2
    {
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string t_saasQueryRule(string appKey, string appSecret, string body)
        {
            string path = "/t_10097154_2/api/v2/rule/sass/queryRule";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20191217174022";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }

    }
}
