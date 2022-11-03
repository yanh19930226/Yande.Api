using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    public class Webctdfs
    {
        //参数tenantId: 类型String, 参数可以为空
        //  描述:
        //参数productId: 类型String, 参数可以为空
        //  描述:
        //参数deviceId: 类型String, 参数可以为空
        //  描述:
        //参数fileName: 类型String, 参数可以为空
        //  描述:
        //参数startTime: 类型String, 参数可以为空
        //  描述:
        //参数endTime: 类型String, 参数可以为空
        //  描述:
        //参数fileType: 类型String, 参数可以为空
        //  描述:
        public static string webctdfs_fileInfos_get(string appKey, string appSecret, string tenantId = "", string productId = "", string deviceId = "", string fileName = "", string startTime = "", string endTime = "", string fileType = "")
        {
            string path = "/webctdfs/fileInfos";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("tenantId", tenantId);
            param.Add("productId", productId);
            param.Add("deviceId", deviceId);
            param.Add("fileName", fileName);
            param.Add("startTime", startTime);
            param.Add("endTime", endTime);
            param.Add("fileType", fileType);

            string version = "20181029162043";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string webctdfs_file_post(string appKey, string appSecret, string body)
        {
            string path = "/webctdfs/file";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = null;
            string version = "20181015162119";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }
        //参数tenantId: 类型String, 参数可以为空
        //  描述:
        //参数productId: 类型String, 参数可以为空
        //  描述:
        //参数deviceId: 类型String, 参数可以为空
        //  描述:
        //参数fileName: 类型String, 参数可以为空
        //  描述:
        //参数fileType: 类型String, 参数可以为空
        //  描述:
        //参数accessToken: 类型String, 参数可以为空
        //  描述:
        public static string webctdfs_file_get(string appKey, string appSecret, string tenantId = "", string productId = "", string deviceId = "", string fileName = "", string fileType = "", string accessToken = "")
        {
            string path = "/webctdfs/file";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("tenantId", tenantId);
            param.Add("productId", productId);
            param.Add("deviceId", deviceId);
            param.Add("fileName", fileName);
            param.Add("fileType", fileType);
            param.Add("accessToken", accessToken);

            string version = "20181015161725";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "GET");
            if (response != null)
                return response;
            return null;
        }
        //参数tenantId: 类型String, 参数可以为空
        //  描述:
        //参数productId: 类型String, 参数可以为空
        //  描述:
        //参数deviceId: 类型String, 参数可以为空
        //  描述:
        //参数fileName: 类型String, 参数可以为空
        //  描述:
        //参数fileType: 类型String, 参数可以为空
        //  描述:
        public static string webctdfs_file_delete(string appKey, string appSecret, string tenantId = "", string productId = "", string deviceId = "", string fileName = "", string fileType = "")
        {
            string path = "/webctdfs/file";
            Dictionary<string, string> headers = null;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("tenantId", tenantId);
            param.Add("productId", productId);
            param.Add("deviceId", deviceId);
            param.Add("fileName", fileName);
            param.Add("fileType", fileType);

            string version = "20181015170759";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, null, version, application, key, "DELETE");
            if (response != null)
                return response;
            return null;
        }

    }
}
