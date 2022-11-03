using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    class Nieyijie
    {
        //参数searchValue: 类型String, 参数可以为空
        //  描述:产品id或者产品名称
        //参数pageNow: 类型long, 参数可以为空
        //  描述:当前页数
        //参数pageSize: 类型long, 参数可以为空
        //  描述:每页记录数
        public static string TestQueryProductList(string appKey, string appSecret, string searchValue = "", string pageNow = "", string pageSize = "")
        {
            string path = "/nieyijie/products";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("searchValue", searchValue);
            param.Add("pageNow", pageNow);
            param.Add("pageSize", pageSize);

            string version = "20220921143811";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }

    }
}
