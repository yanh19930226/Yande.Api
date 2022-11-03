using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    public class Aep_order
    {
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string refundCall(string appKey, string appSecret, string body)
        {
            string path = "/aep_order/paycall/v1/refundCall";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20180814185018";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string payCall(string appKey, string appSecret, string body)
        {
            string path = "/aep_order/paycall/v1/payCall";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20180814183934";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string creditControl(string appKey, string appSecret, string body)
        {
            string path = "/aep_order/v1/credit/creditControl";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20181010152049";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数msg: 类型String, 参数不可以为空
        //  描述:
        public static string Testxx(string appKey, string appSecret, string msg)
        {
            string path = "/aep_order/testxx";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("msg", msg);

            string version = "20181016105634";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }

    }
}
