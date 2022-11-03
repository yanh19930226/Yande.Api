using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    class Distribute_it
    {
        //参数tenantId: 类型String, 参数不可以为空
        //  描述:
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string CreatApp(string appKey, string appSecret, string tenantId, string body)
        {
            string path = "/distribute_it/distributed/app/saasApp";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("tenantId", tenantId);

            Dictionary<string, string> param = null;
            string version = "20210512032621";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数appId: 类型String, 参数不可以为空
        //  描述:
        //参数userId: 类型String, 参数不可以为空
        //  描述:
        //参数tenantId: 类型String, 参数不可以为空
        //  描述:
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string DeleteApp(string appKey, string appSecret, string appId, string userId, string tenantId, string body)
        {
            string path = "/distribute_it/deleteApp";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("tenantId", tenantId);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("appId", appId);
            param.Add("userId", userId);

            string version = "20210512060215";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数appId: 类型String, 参数不可以为空
        //  描述:
        //参数addrTypeId: 类型String, 参数不可以为空
        //  描述:
        //参数tenantId: 类型String, 参数不可以为空
        //  描述:
        public static string GetAddr(string appKey, string appSecret, string appId, string addrTypeId, string tenantId)
        {
            string path = "/distribute_it/getAddr";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("tenantId", tenantId);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("appId", appId);
            param.Add("addrTypeId", addrTypeId);

            string version = "20210512060145";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数appId: 类型String, 参数可以为空
        //  描述:
        //参数tenantId: 类型String, 参数可以为空
        //  描述:
        public static string getInfo(string appKey, string appSecret, string appId = "", string tenantId = "")
        {
            string path = "/distribute_it/getInfo";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("tenantId", tenantId);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("appId", appId);

            string version = "20210512092409";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }

    }
}
