using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    class Tr069_device_management
    {
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string CreateDevice(string appKey, string appSecret, string body)
        {
            string path = "/tr069_device_management/device";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20220822175025";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string UpdateDevice(string appKey, string appSecret, string body)
        {
            string path = "/tr069_device_management/device";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20220822174625";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "PUT");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string DeleteDevice(string appKey, string appSecret, string body)
        {
            string path = "/tr069_device_management/deleteDevice";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20220823091949";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string QueryDeviceList(string appKey, string appSecret, string body)
        {
            string path = "/tr069_device_management/devices";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20220822174815";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数productId: 类型long, 参数可以为空
        //  描述:
        //参数deviceId: 类型String, 参数可以为空
        //  描述:
        public static string QueryDevice(string appKey, string appSecret, string productId = "", string deviceId = "")
        {
            string path = "/tr069_device_management/device";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("productId", productId);
            param.Add("deviceId", deviceId);

            string version = "20220822174930";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数deviceId: 类型String, 参数可以为空
        //  描述:
        //参数deviceName: 类型String, 参数可以为空
        //  描述:
        //参数pageNow: 类型long, 参数可以为空
        //  描述:
        //参数pageSize: 类型long, 参数可以为空
        //  描述:
        public static string ListDeviceInfo(string appKey, string appSecret, string deviceId = "", string deviceName = "", string pageNow = "", string pageSize = "")
        {
            string path = "/tr069_device_management/listByDeviceIds";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("deviceId", deviceId);
            param.Add("deviceName", deviceName);
            param.Add("pageNow", pageNow);
            param.Add("pageSize", pageSize);

            string version = "20220822175115";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }

    }
}
