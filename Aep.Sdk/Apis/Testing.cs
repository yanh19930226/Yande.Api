using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    public class Testing
    {
        public static string apitest(string appKey, string appSecret)
        {
            string path = "/testing/app/host/api/test";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20190514164617";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }

    }
}
