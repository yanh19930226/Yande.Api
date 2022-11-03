using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    class Aep_statistic
    {
        //参数notInTenantIds: 类型String, 参数可以为空
        //  描述:
        public static string apiReport(string appKey, string appSecret, string notInTenantIds = "")
        {
            string path = "/aep_statistic/apiReport";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("notInTenantIds", notInTenantIds);

            string version = "20190307184500";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }

    }
}
