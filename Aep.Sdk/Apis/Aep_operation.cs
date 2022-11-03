using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    public                                        class Aep_operation
    {
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string QueryProductCount(string appKey, string appSecret, string body)
        {
            string path = "/aep_operation/operationData/dm/v1/productCount";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20190826161828";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数tenantIds: 类型String, 参数不可以为空
        //  描述:
        public static string QueryProductInfos(string appKey, string appSecret, string tenantIds)
        {
            string path = "/aep_operation/operationData/dm/v1/productInfos";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("tenantIds", tenantIds);

            string version = "20190116151708";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string QueryActivatedDeviceCount(string appKey, string appSecret, string body)
        {
            string path = "/aep_operation/operationData/dm/v1/activatedDeviceCount";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20190826163530";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string QueryRegisteredDeviceCount(string appKey, string appSecret, string body)
        {
            string path = "/aep_operation/operationData/dm/v1/registeredDeviceCount";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20190826163415";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数productIds: 类型String, 参数可以为空
        //  描述:
        //参数notInTenantIds: 类型String, 参数可以为空
        //  描述:
        public static string QueryDeviceCountById(string appKey, string appSecret, string productIds = "", string notInTenantIds = "")
        {
            string path = "/aep_operation/operationData/dm/v1/deviceCountById";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("productIds", productIds);
            param.Add("notInTenantIds", notInTenantIds);

            string version = "20190418104504";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数startTime: 类型String, 参数不可以为空
        //  描述:
        //参数endTime: 类型String, 参数不可以为空
        //  描述:
        //参数pageNow: 类型long, 参数可以为空
        //  描述:
        //参数pageSize: 类型long, 参数可以为空
        //  描述:
        //参数notInTenantIds: 类型String, 参数可以为空
        //  描述:
        public static string QueryProductInfoByTime(string appKey, string appSecret, string startTime, string endTime, string pageNow = "", string pageSize = "", string notInTenantIds = "")
        {
            string path = "/aep_operation/operationData/dm/v1/productInfoByTime";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("startTime", startTime);
            param.Add("endTime", endTime);
            param.Add("pageNow", pageNow);
            param.Add("pageSize", pageSize);
            param.Add("notInTenantIds", notInTenantIds);

            string version = "20190418104949";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数level: 类型long, 参数不可以为空
        //  描述:分类级别
        //参数parentType: 类型long, 参数不可以为空
        //  描述:父类Id
        public static string QueryCategoryBylevel(string appKey, string appSecret, string level, string parentType)
        {
            string path = "/aep_operation/operationData/dm/productcategory/v1/getCategoryBylevel";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("level", level);
            param.Add("parentType", parentType);

            string version = "20190418135917";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }

    }
}
