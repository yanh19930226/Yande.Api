using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    class Aep_mq_sub
    {
        public static string QueryServiceState(string appKey, string appSecret)
        {
            string path = "/aep_mq_sub/mqStat";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210121102821";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string OpenMqService(string appKey, string appSecret, string body)
        {
            string path = "/aep_mq_sub/mqStat";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20201214175517";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数topicId: 类型long, 参数不可以为空
        //  描述:
        public static string QueryTopicInfo(string appKey, string appSecret, string topicId)
        {
            string path = "/aep_mq_sub/topic";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("topicId", topicId);

            string version = "20210121102533";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数topicId: 类型long, 参数不可以为空
        //  描述:
        public static string QueryTopicCacheInfo(string appKey, string appSecret, string topicId)
        {
            string path = "/aep_mq_sub/topic/cache";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("topicId", topicId);

            string version = "20210121102132";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        public static string QueryTopics(string appKey, string appSecret)
        {
            string path = "/aep_mq_sub/topics";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210121101501";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string QuerySubRules(string appKey, string appSecret, string body)
        {
            string path = "/aep_mq_sub/rule";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210121105552";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        public static string ClosePushService(string appKey, string appSecret)
        {
            string path = "/aep_mq_sub/mqStat";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20210121113722";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "DELETE");
            if (response != null)
                return response;
            return null;
        }

    }
}
