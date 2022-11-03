using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    public  class Aep_rule_engine
    {
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string saasCreateRule(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/api/v2/rule/sass/createRule";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20200107172057";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string saasUpdateRule(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/api/v2/rule/sass/updateRule";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20200107172020";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string saasDeleteRuleEngine(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/api/v2/rule/sass/deleteRule";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20200107172343";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数ruleId: 类型String, 参数可以为空
        //  描述:
        //参数productId: 类型String, 参数可以为空
        //  描述:
        //参数pageNow: 类型long, 参数可以为空
        //  描述:
        //参数pageSize: 类型long, 参数可以为空
        //  描述:
        public static string saasQueryRule(string appKey, string appSecret, string ruleId = "", string productId = "", string pageNow = "", string pageSize = "")
        {
            string path = "/aep_rule_engine/api/v2/rule/sass/queryRule";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("ruleId", ruleId);
            param.Add("productId", productId);
            param.Add("pageNow", pageNow);
            param.Add("pageSize", pageSize);

            string version = "20200107172119";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string DeleteForward(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/v3/rule/deleteForward";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210322084225";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数ruleId: 类型String, 参数不可以为空
        //  描述:
        //参数productId: 类型String, 参数可以为空
        //  描述:
        //参数pageNow: 类型String, 参数可以为空
        //  描述:
        //参数pageSize: 类型String, 参数可以为空
        //  描述:
        public static string GetForwards(string appKey, string appSecret, string ruleId, string productId = "", string pageNow = "", string pageSize = "")
        {
            string path = "/aep_rule_engine/v3/rule/getForwards";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("ruleId", ruleId);
            param.Add("productId", productId);
            param.Add("pageNow", pageNow);
            param.Add("pageSize", pageSize);

            string version = "20210322084235";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string UpdateForward(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/v3/rule/updateForward";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210322084206";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string CreateForward(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/v3/rule/addForward";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210322084219";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string UpdateRule(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/v3/rule/updateRule";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210322084201";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string UpdateRuleRunStatus(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/v3/rule/modifyRuleRunningStatus";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210322084155";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string GetRuleRunStatus(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/v3/rule/getRuleRunningStatus";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210322084150";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数ruleId: 类型String, 参数不可以为空
        //  描述:
        //参数productId: 类型String, 参数可以为空
        //  描述:
        //参数pageNow: 类型String, 参数可以为空
        //  描述:
        //参数pageSize: 类型String, 参数可以为空
        //  描述:
        public static string GetRuleInfo(string appKey, string appSecret, string ruleId, string productId = "", string pageNow = "", string pageSize = "")
        {
            string path = "/aep_rule_engine/v3/rule/getRules";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("ruleId", ruleId);
            param.Add("productId", productId);
            param.Add("pageNow", pageNow);
            param.Add("pageSize", pageSize);

            string version = "20210322084144";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string DeleteRule(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/v3/rule/deleteRule";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210322084126";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string CreateRule(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/v3/rule/createRule";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210322084119";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string CreateAction(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/v3/rule/addAction";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210412092449";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string UpdateAction(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/v3/rule/updateAction";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210412092439";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string DeleteAction(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/v3/rule/deleteAct";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210412092444";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数ruleId: 类型String, 参数不可以为空
        //  描述:
        //参数pageNow: 类型String, 参数可以为空
        //  描述:
        //参数pageSize: 类型String, 参数可以为空
        //  描述:
        public static string GetActions(string appKey, string appSecret, string ruleId, string pageNow = "", string pageSize = "")
        {
            string path = "/aep_rule_engine/v3/rule/getActions";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("ruleId", ruleId);
            param.Add("pageNow", pageNow);
            param.Add("pageSize", pageSize);

            string version = "20210412092402";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string CreateWarn(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/v3/rule/addWarn";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210412092434";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string DeleteWarn(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/v3/rule/deleteWarn";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210412092429";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string UpdateWarn(string appKey, string appSecret, string body)
        {
            string path = "/aep_rule_engine/v3/rule/updateWarn";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210412092418";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数ruleId: 类型String, 参数不可以为空
        //  描述:
        //参数pageNow: 类型String, 参数可以为空
        //  描述:
        //参数pageSize: 类型String, 参数可以为空
        //  描述:
        public static string GetWarns(string appKey, string appSecret, string ruleId, string pageNow = "", string pageSize = "")
        {
            string path = "/aep_rule_engine/v3/rule/getWarns";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("ruleId", ruleId);
            param.Add("pageNow", pageNow);
            param.Add("pageSize", pageSize);

            string version = "20210412092411";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数pageNow: 类型String, 参数可以为空
        //  描述:
        //参数pageSize: 类型String, 参数可以为空
        //  描述:
        public static string GetUserWarns(string appKey, string appSecret, string pageNow = "", string pageSize = "")
        {
            string path = "/aep_rule_engine/v3/rule/getUserWarns";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("pageNow", pageNow);
            param.Add("pageSize", pageSize);

            string version = "20210412092425";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }

    }
}
