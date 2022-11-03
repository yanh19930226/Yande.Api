using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    class Aep_edge_gateway_
    {
        //参数gatewayDeviceId: 类型String, 参数不可以为空
        //  描述:
        //参数pageNow: 类型long, 参数可以为空
        //  描述:
        //参数pageSize: 类型long, 参数可以为空
        //  描述:
        public static string QueryEdgeInstanceDevice(string appKey, string appSecret, string gatewayDeviceId, string pageNow = "", string pageSize = "")
        {
            string path = "/aep_edge_gateway_/instance/devices";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("gatewayDeviceId", gatewayDeviceId);
            param.Add("pageNow", pageNow);
            param.Add("pageSize", pageSize);

            string version = "20201223152232";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string DeleteEdgeInstanceDevice(string appKey, string appSecret, string body)
        {
            string path = "/aep_edge_gateway_/instance/devices";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20201217161509";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数id: 类型long, 参数不可以为空
        //  描述:
        public static string DeleteEdgeInstance(string appKey, string appSecret, string id)
        {
            string path = "/aep_edge_gateway_/instance";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("id", id);

            string version = "20201217161503";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string AddEdgeInstanceDevice(string appKey, string appSecret, string body)
        {
            string path = "/aep_edge_gateway_/instance/device";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20201217161500";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string AddEdgeInstanceDrive(string appKey, string appSecret, string body)
        {
            string path = "/aep_edge_gateway_/instance/drive";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20201217161456";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string CreateEdgeInstance(string appKey, string appSecret, string body)
        {
            string path = "/aep_edge_gateway_/instance";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20201217165327";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string EdgeInstanceDeploy(string appKey, string appSecret, string body)
        {
            string path = "/aep_edge_gateway_/instance/settings";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20201217161447";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }

    }
}
