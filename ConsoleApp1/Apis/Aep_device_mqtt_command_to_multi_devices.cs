using AepSdk.Apis.Core;
using System.Collections.Generic;


namespace AepSdk.Apis
{
    class Aep_device_mqtt_command_to_multi_devices
    {
        //参数MasterKey: 类型String, 参数可以为空
        //  描述:
        //参数body: 类型json, 参数不可以为空
        //  描述:body,具体参考平台api说明
        public static string commandToDevices(string appKey, string appSecret, string body, string MasterKey = "")
        {
            string path = "/aep_device_mqtt_command_to_multi_devices/commandToDevices";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("MasterKey", MasterKey);

            Dictionary<string, string> param = null;
            string version = "20200420094552";

            string application = appKey;
            string key = appSecret;


            string response = AepHttpRequest.SendAepHttpRequest(path, headers, param, body, version, application, key, "POST");
            if (response != null)
                return response;
            return null;
        }

    }
}
