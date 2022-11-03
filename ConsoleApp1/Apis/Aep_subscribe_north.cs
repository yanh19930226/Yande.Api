using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    class Aep_subscribe_north
    {
        //参数subId: 类型long, 参数不可以为空
        //  描述:id
        //参数productId: 类型long, 参数不可以为空
        //  描述:产品Id
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:MasterKey
        public static string GetSubscription(string appKey, string appSecret, string subId, string productId, string MasterKey)
        {
            string path = "/aep_subscribe_north/subscription";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("subId", subId);
            param.Add("productId", productId);

            string version = "20181018100038";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数subId: 类型String, 参数不可以为空
        //  描述:订阅Id
        //参数deviceId: 类型String, 参数可以为空
        //  描述:设备Id
        //参数productId: 类型long, 参数不可以为空
        //  描述:产品Id
        //参数subLevel: 类型long, 参数不可以为空
        //  描述:订阅级别(1:产品级 2：设备级)
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:MasterKey
        public static string DeleteSubscription(string appKey, string appSecret, string subId, string productId, string subLevel, string MasterKey, string deviceId = "")
        {
            string path = "/aep_subscribe_north/subscription";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("subId", subId);
            param.Add("deviceId", deviceId);
            param.Add("productId", productId);
            param.Add("subLevel", subLevel);

            string version = "20181018163447";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "DELETE");
            if (response != null)
                return response;
            return null;
        }
        //参数productId: 类型long, 参数不可以为空
        //  描述:产品ID
        //参数pageNow: 类型long, 参数不可以为空
        //  描述:当前页数
        //参数pageSize: 类型long, 参数不可以为空
        //  描述:每页记录数
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:MasterKey
        //参数subType: 类型long, 参数不可以为空
        //  描述:订阅类型(1表示，LWM2M的设备数据变化通知、2表示，LWM2M的设备响应命令通知、3表示，TUP的设备数据变化通知、4表示，TUP的设备响应命令通知)
        //参数searchValue: 类型String, 参数可以为空
        //  描述:搜索值（产品id模糊查询）
        //参数deviceGroupId: 类型long, 参数可以为空
        //  描述:
        public static string GetSubscriptionsList(string appKey, string appSecret, string productId, string pageNow, string pageSize, string MasterKey, string subType, string searchValue = "", string deviceGroupId = "")
        {
            string path = "/aep_subscribe_north/subscribes";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("productId", productId);
            param.Add("pageNow", pageNow);
            param.Add("pageSize", pageSize);
            param.Add("subType", subType);
            param.Add("searchValue", searchValue);
            param.Add("deviceGroupId", deviceGroupId);

            string version = "20220929090647";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:MasterKey
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string CreateSubscription(string appKey, string appSecret, string MasterKey, string body)
        {
            string path = "/aep_subscribe_north/subscription";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = null;
            string version = "20181018095708";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }

    }
}
