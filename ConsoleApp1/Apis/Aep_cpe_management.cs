using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    class Aep_cpe_management
    {
        public static string CpeAcsSelect(string appKey, string appSecret)
        {
            string path = "/aep_cpe_management/cpeManager/cpeAcsSelect";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073511";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数pageNum: 类型long, 参数可以为空
        //  描述:
        //参数pageSize: 类型long, 参数可以为空
        //  描述:
        //参数productId: 类型long, 参数可以为空
        //  描述:
        //参数acsId: 类型long, 参数可以为空
        //  描述:
        //参数deviceId: 类型String, 参数可以为空
        //  描述:
        //参数deviceName: 类型String, 参数可以为空
        //  描述:
        public static string QueryDeviceList(string appKey, string appSecret, string pageNum = "", string pageSize = "", string productId = "", string acsId = "", string deviceId = "", string deviceName = "")
        {
            string path = "/aep_cpe_management/cpeManager/queryDeviceList";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("pageNum", pageNum);
            param.Add("pageSize", pageSize);
            param.Add("productId", productId);
            param.Add("acsId", acsId);
            param.Add("deviceId", deviceId);
            param.Add("deviceName", deviceName);

            string version = "20211015073508";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数pageNum: 类型long, 参数可以为空
        //  描述:
        //参数pageSize: 类型long, 参数可以为空
        //  描述:
        //参数deviceId: 类型String, 参数可以为空
        //  描述:
        //参数terminalName: 类型String, 参数可以为空
        //  描述:
        public static string GetAllDeviceInfo(string appKey, string appSecret, string pageNum = "", string pageSize = "", string deviceId = "", string terminalName = "")
        {
            string path = "/aep_cpe_management/cpeManager/getAllDeviceInfo";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("pageNum", pageNum);
            param.Add("pageSize", pageSize);
            param.Add("deviceId", deviceId);
            param.Add("terminalName", terminalName);

            string version = "20211015073506";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string deleteDevice(string appKey, string appSecret, string body)
        {
            string path = "/aep_cpe_management/cpeManager/deleteDevice";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073501";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string CreateDevice(string appKey, string appSecret, string body)
        {
            string path = "/aep_cpe_management/cpeManager/createDevice";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073457";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string OrderByParam(string appKey, string appSecret, string body)
        {
            string path = "/aep_cpe_management/cpeServer/orderByParam";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073453";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string CreateProduct(string appKey, string appSecret, string body)
        {
            string path = "/aep_cpe_management/cpeManager/createProduct";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073449";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string UpdateCpeServer(string appKey, string appSecret, string body)
        {
            string path = "/aep_cpe_management/cpeServer/updateCpeServer";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073446";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string InsertCpeServer(string appKey, string appSecret, string body)
        {
            string path = "/aep_cpe_management/cpeServer/insertCpeServer";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073443";

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
            string path = "/aep_cpe_management/cpeManager/updateDevice";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073440";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string UpdateAcs(string appKey, string appSecret, string body)
        {
            string path = "/aep_cpe_management/acsManager/updateAcs";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073437";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string GetDeviceStatusHis(string appKey, string appSecret, string body)
        {
            string path = "/aep_cpe_management/cpeManager/getDeviceStatusHis";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073434";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string CreateAcs(string appKey, string appSecret, string body)
        {
            string path = "/aep_cpe_management/acsManager/insertAcs";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073431";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数pageNum: 类型long, 参数可以为空
        //  描述:
        //参数pageSize: 类型long, 参数可以为空
        //  描述:
        //参数acsName: 类型String, 参数可以为空
        //  描述:
        //参数acsAddress: 类型String, 参数可以为空
        //  描述:
        public static string FindAcs(string appKey, string appSecret, string pageNum = "", string pageSize = "", string acsName = "", string acsAddress = "")
        {
            string path = "/aep_cpe_management/acsManager/findAcs";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("pageNum", pageNum);
            param.Add("pageSize", pageSize);
            param.Add("acsName", acsName);
            param.Add("acsAddress", acsAddress);

            string version = "20211015073427";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数cpeServerId: 类型long, 参数不可以为空
        //  描述:
        //参数deviceId: 类型String, 参数不可以为空
        //  描述:
        public static string FindDeviceCpeParam(string appKey, string appSecret, string cpeServerId, string deviceId)
        {
            string path = "/aep_cpe_management/cpeServer/findDeviceCpeParam/[cpeServerId]/[deviceId]";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("cpeServerId", cpeServerId);
            param.Add("deviceId", deviceId);

            string version = "20211015073342";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数cpeServerId: 类型long, 参数可以为空
        //  描述:
        public static string FindServerParamById(string appKey, string appSecret, string cpeServerId = "")
        {
            string path = "/aep_cpe_management/findServerParamById/[cpeServerId]";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("cpeServerId", cpeServerId);

            string version = "20211015073244";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数acsId: 类型long, 参数不可以为空
        //  描述:
        public static string FindAcsById(string appKey, string appSecret, string acsId)
        {
            string path = "/aep_cpe_management/acsManager/findAcsById/[acsId]";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("acsId", acsId);

            string version = "20211015073201";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        public static string CpeServerSelect(string appKey, string appSecret)
        {
            string path = "/aep_cpe_management/cpeManager/cpeServerSelect";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073424";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数pageNum: 类型long, 参数可以为空
        //  描述:
        //参数pageSize: 类型long, 参数可以为空
        //  描述:
        public static string CpeProductSelect(string appKey, string appSecret, string pageNum = "", string pageSize = "")
        {
            string path = "/aep_cpe_management/cpeManager/cpeProductSelect";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("pageNum", pageNum);
            param.Add("pageSize", pageSize);

            string version = "20211015073421";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        public static string ParamSelect(string appKey, string appSecret)
        {
            string path = "/aep_cpe_management/cpeServer/paramSelect";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073417";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        public static string DataTypeSelect(string appKey, string appSecret)
        {
            string path = "/aep_cpe_management/cpeServer/dataTypeSelect";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073414";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string DeleteAcs(string appKey, string appSecret, string body)
        {
            string path = "/aep_cpe_management/acsManager/deleteAcs";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20211015073408";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数deviceId: 类型String, 参数可以为空
        //  描述:
        public static string RebootDevice(string appKey, string appSecret, string deviceId = "")
        {
            string path = "/aep_cpe_management/cpeManager/rebootDevice";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("deviceId", deviceId);

            string version = "20211015073404";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数productId: 类型long, 参数可以为空
        //  描述:
        //参数deviceId: 类型String, 参数可以为空
        //  描述:
        public static string FindDeviceById(string appKey, string appSecret, string productId = "", string deviceId = "")
        {
            string path = "/aep_cpe_management/cpeManager/findDeviceById";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("productId", productId);
            param.Add("deviceId", deviceId);

            string version = "20211015073102";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数pageNum: 类型long, 参数可以为空
        //  描述:
        //参数pageSize: 类型long, 参数可以为空
        //  描述:
        //参数cpeServerName: 类型String, 参数可以为空
        //  描述:
        public static string FindCpeServer(string appKey, string appSecret, string pageNum = "", string pageSize = "", string cpeServerName = "")
        {
            string path = "/aep_cpe_management/cpeServer/findCpeServer";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("pageNum", pageNum);
            param.Add("pageSize", pageSize);
            param.Add("cpeServerName", cpeServerName);

            string version = "20211015073057";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数cpeServerId: 类型long, 参数可以为空
        //  描述:
        public static string DeleteCpeServer(string appKey, string appSecret, string cpeServerId = "")
        {
            string path = "/aep_cpe_management/cpeServer/deleteCpeServer";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("cpeServerId", cpeServerId);

            string version = "20211015073054";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }

    }
}
