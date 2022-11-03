using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    public class Iocm
    {
        //参数notifyType: 类型String, 参数可以为空
        //  描述:
        //参数callbackUrl: 类型String, 参数可以为空
        //  描述:
        public static string delNbSubs(string appKey, string appSecret, string notifyType = "", string callbackUrl = "")
        {
            string path = "/iocm/app/sub/v1.2.0/subscriptions";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("notifyType", notifyType);
            param.Add("callbackUrl", callbackUrl);

            string version = "20210723092313";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "DELETE");
            if (response != null)
                return response;
            return null;
        }
        //参数subscriptionId: 类型long, 参数不可以为空
        //  描述:
        public static string delNbSub(string appKey, string appSecret, string subscriptionId)
        {
            string path = "/iocm/v1.2.0/subscriptions/[subscriptionId]";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("subscriptionId", subscriptionId);

            string version = "20210629023058";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "DELETE");
            if (response != null)
                return response;
            return null;
        }
        //参数notifyType: 类型String, 参数可以为空
        //  描述:
        //参数pageNo: 类型long, 参数可以为空
        //  描述:
        //参数pageSize: 类型long, 参数可以为空
        //  描述:
        public static string getNbSubList(string appKey, string appSecret, string notifyType = "", string pageNo = "", string pageSize = "")
        {
            string path = "/iocm/app/sub/v1.2.0/subscriptions";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("notifyType", notifyType);
            param.Add("pageNo", pageNo);
            param.Add("pageSize", pageSize);

            string version = "20210723092145";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数subscriptionId: 类型long, 参数不可以为空
        //  描述:
        public static string getNbSub(string appKey, string appSecret, string subscriptionId)
        {
            string path = "/iocm/app/sub/v1.2.0/subscriptions/[subscriptionId]";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("subscriptionId", subscriptionId);

            string version = "20210723091041";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string nbSubProfData(string appKey, string appSecret, string body)
        {
            string path = "/iocm/app/sub/v1.2.0/subscriptions";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210820092548";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数deviceId: 类型String, 参数不可以为空
        //  描述:
        //参数gatewayId: 类型String, 参数不可以为空
        //  描述:
        //参数pageNo: 类型long, 参数可以为空
        //  描述:
        //参数pageSize: 类型long, 参数可以为空
        //  描述:
        //参数startTime: 类型String, 参数可以为空
        //  描述:
        //参数endTime: 类型String, 参数可以为空
        //  描述:
        public static string getDeviceMsgOnePage(string appKey, string appSecret, string deviceId, string gatewayId, string pageNo = "", string pageSize = "", string startTime = "", string endTime = "")
        {
            string path = "/iocm/app/data/v1.2.0/deviceDataHistory";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("deviceId", deviceId);
            param.Add("gatewayId", gatewayId);
            param.Add("pageNo", pageNo);
            param.Add("pageSize", pageSize);
            param.Add("startTime", startTime);
            param.Add("endTime", endTime);

            string version = "20210723085102";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string nbSubProfDataV110(string appKey, string appSecret, string body)
        {
            string path = "/iocm/app/sub/v1.1.0/subscribe";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210820092733";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }

    }
}
