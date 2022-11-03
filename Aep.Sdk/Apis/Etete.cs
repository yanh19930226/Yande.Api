using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    public class Etete
    {
        public static string sere(string appKey, string appSecret)
        {
            string path = "/etete/d";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20221014142912";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数MasterKey: 类型String, 参数不可以为空
        //  描述:MasterKey在该设备所属产品的概况中可以查看
        //参数productId: 类型long, 参数不可以为空
        //  描述:
        public static string QueryDeviceList(string appKey, string appSecret, string MasterKey, string productId)
        {
            string path = "/etete/devices";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("productId", productId);

            string version = "20200512162427";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }

    }
}
