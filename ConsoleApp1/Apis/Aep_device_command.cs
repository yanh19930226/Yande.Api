using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    class Aep_device_command
    {
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:MasterKey在该设备所属产品的概况中可以查看
        //参数productId: 类型long, 参数不可以为空
        //  描述:
        //参数deviceId: 类型String, 参数不可以为空
        //  描述:
        //参数startTime: 类型String, 参数可以为空
        //  描述:精确到毫秒的时间戳
        //参数endTime: 类型String, 参数可以为空
        //  描述:精确到毫秒的时间戳
        //参数pageNow: 类型long, 参数可以为空
        //  描述:当前页数
        //参数pageSize: 类型long, 参数可以为空
        //  描述:每页记录数
        //参数groupCommandId: 类型String, 参数可以为空
        //  描述:群组任务ID
        public static string QueryCommandList(string appKey, string appSecret, string MasterKey, string productId, string deviceId, string startTime = "", string endTime = "", string pageNow = "", string pageSize = "", string groupCommandId = "")
        {
            string path = "/aep_device_command/commands";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("productId", productId);
            param.Add("deviceId", deviceId);
            param.Add("startTime", startTime);
            param.Add("endTime", endTime);
            param.Add("pageNow", pageNow);
            param.Add("pageSize", pageSize);
            param.Add("groupCommandId", groupCommandId);

            string version = "20200817195717";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:MasterKey在该设备所属产品的概况中可以查看
        //参数commandId: 类型String, 参数不可以为空
        //  描述:创建指令成功响应中返回的id，
        //参数productId: 类型long, 参数不可以为空
        //  描述:
        //参数deviceId: 类型String, 参数不可以为空
        //  描述:设备ID，Lwm2m协议和NB网关协议必填
        public static string QueryCommand(string appKey, string appSecret, string MasterKey, string commandId, string productId, string deviceId)
        {
            string path = "/aep_device_command/command";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("commandId", commandId);
            param.Add("productId", productId);
            param.Add("deviceId", deviceId);

            string version = "20190619200322";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:MasterKey在该设备所属产品的概况中可以查看
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string CreateCommand(string appKey, string appSecret, string MasterKey, string body)
        {
            string path = "/aep_device_command/command";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = null;
            string version = "20190619200235";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string CancelCommand(string appKey, string appSecret, string MasterKey, string body)
        {
            string path = "/aep_device_command/cancelCommand";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = null;
            string version = "20190522185339";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "PUT");
            if (response != null)
                return response;
            return null;
        }

    }
}
