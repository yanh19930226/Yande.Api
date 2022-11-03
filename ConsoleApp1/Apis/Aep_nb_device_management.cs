using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    class Aep_nb_device_management
    {
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string BatchCreateNBDevice(string appKey, string appSecret, string body)
        {
            string path = "/aep_nb_device_management/batchNBDevice";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20200824104542";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数MasterKey: 类型String, 参数可以为空
        //  描述:
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string CreateDevice(string appKey, string appSecret, string body, string MasterKey = "")
        {
            string path = "/aep_nb_device_management/device";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = null;
            string version = "20200821151424";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:
        //参数deviceId: 类型String, 参数不可以为空
        //  描述:
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string UpdateDevice(string appKey, string appSecret, string MasterKey, string deviceId, string body)
        {
            string path = "/aep_nb_device_management/device";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("deviceId", deviceId);

            string version = "20200821160251";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "PUT");
            if (response != null)
                return response;
            return null;
        }
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:
        //参数productId: 类型long, 参数可以为空
        //  描述:
        //参数searchValue: 类型String, 参数可以为空
        //  描述:
        //参数pageNow: 类型long, 参数可以为空
        //  描述:
        //参数pageSize: 类型long, 参数可以为空
        //  描述:
        //参数test: 类型String, 参数可以为空
        //  描述:
        public static string QueryDeviceList(string appKey, string appSecret, string MasterKey, string productId = "", string searchValue = "", string pageNow = "", string pageSize = "", string test = "")
        {
            string path = "/aep_nb_device_management/devices";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("productId", productId);
            param.Add("searchValue", searchValue);
            param.Add("pageNow", pageNow);
            param.Add("pageSize", pageSize);
            param.Add("test", test);

            string version = "20200821154004";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:
        //参数deviceId: 类型String, 参数不可以为空
        //  描述:
        //参数productId: 类型long, 参数不可以为空
        //  描述:
        public static string QueryDevice(string appKey, string appSecret, string MasterKey, string deviceId, string productId)
        {
            string path = "/aep_nb_device_management/device";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("deviceId", deviceId);
            param.Add("productId", productId);

            string version = "20200821154431";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:
        //参数productId: 类型long, 参数不可以为空
        //  描述:
        //参数deviceIds: 类型String, 参数不可以为空
        //  描述:
        public static string DeleteDevice(string appKey, string appSecret, string MasterKey, string productId, string deviceIds)
        {
            string path = "/aep_nb_device_management/device";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("productId", productId);
            param.Add("deviceIds", deviceIds);

            string version = "20200821154327";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "DELETE");
            if (response != null)
                return response;
            return null;
        }
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string BatchCancelDevices(string appKey, string appSecret, string MasterKey, string body)
        {
            string path = "/aep_nb_device_management/cancelledDevices";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = null;
            string version = "20210918070025";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "PUT");
            if (response != null)
                return response;
            return null;
        }
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string DeleteDeviceByImei(string appKey, string appSecret, string MasterKey, string body)
        {
            string path = "/aep_nb_device_management/deleteDeviceByImei";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = null;
            string version = "20220214142629";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }

    }
}
