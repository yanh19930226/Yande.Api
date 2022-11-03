using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    public class Aep_rule
    {
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string saasCreateRule(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule/api/v2/rule/sass/createRule";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20190723160312";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string saasUpdateRule(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule/api/v2/rule/sass/updateRule";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20190723160157";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string saasDeleteRuleEngine(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule/api/v2/rule/sass/deleteRule";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20190723150343";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }

    }
}
