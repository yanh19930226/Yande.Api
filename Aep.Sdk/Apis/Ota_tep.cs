using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    public class Ota_tep
    {
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string createTask(string appKey, string appSecret, string body)
        {
            string path = "/ota_tep/tep/dm/api/task/createTask";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211124160655";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string updateTask(string appKey, string appSecret, string body)
        {
            string path = "/ota_tep/tep/dm/api/task/updateTask";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211124160731";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数jobId: 类型long, 参数不可以为空
        //  描述:
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string deleteTask(string appKey, string appSecret, string jobId, string body)
        {
            string path = "/ota_tep/tep/dm/api/task/deleteTask";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("jobId", jobId);

            string version = "20211124161958";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }

    }
}
