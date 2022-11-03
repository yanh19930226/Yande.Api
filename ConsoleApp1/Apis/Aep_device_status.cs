using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    class Aep_device_status
    {
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string QueryDeviceStatus(string appKey, string appSecret, string body)
        {
            string path = "/aep_device_status/deviceStatus";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20190924151922";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数tenantId: 类型String, 参数可以为空
        //  描述:
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string getDeviceStatusHisInPage_test(string appKey, string appSecret, string body, string tenantId = "")
        {
            string path = "/aep_device_status/api/v1/getDeviceStatusHisInPage";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("tenantId", tenantId);

            Dictionary<string, string> param = null;
            string version = "20181206100328";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string getDeviceStatusHisInTotal_test(string appKey, string appSecret, string body)
        {
            string path = "/aep_device_status/v1/getDeviceStatusHisInTotal";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20220304162645";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string getDeviceStatusHisInTotal(string appKey, string appSecret, string body)
        {
            string path = "/aep_device_status/getDeviceStatusHisInTotal";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20200623153917";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string getDeviceStatusHisInPage(string appKey, string appSecret, string body)
        {
            string path = "/aep_device_status/getDeviceStatusHisInPage";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211109160549";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string QueryDeviceStatusList(string appKey, string appSecret, string body)
        {
            string path = "/aep_device_status/deviceStatusList";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20220304160727";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }

    }
}
