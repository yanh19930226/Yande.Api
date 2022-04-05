using Nacos.V2;
using Nacos.V2.Config.Abst;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Yande.Api.NacosParser
{
    //public class MyNacosConfigFilter : IConfigFilter
    //{

    //    private string ReplaceJsonNode(string src, string encryptedDataKey, bool isEnc = true)
    //    {
    //        // 示例配置用的是JSON，如果用的是 yaml，这里换成用 yaml 解析即可。
    //        var jObj = JObject.Parse(src);

    //        foreach (var item in _jsonPaths)
    //        {
    //            var t = jObj.SelectToken(item);

    //            if (t != null)
    //            {
    //                var r = t.ToString();

    //                // 加解密
    //                var newToken = isEnc
    //                    ? AESEncrypt(r, encryptedDataKey)
    //                    : AESDecrypt(r, encryptedDataKey);

    //                if (!string.IsNullOrWhiteSpace(newToken))
    //                {
    //                    // 替换旧值
    //                    t.Replace(newToken);
    //                }
    //            }
    //        }

    //        return jObj.ToString();
    //    }
    //    public void DoFilter(IConfigRequest request, IConfigResponse response, IConfigFilterChain filterChain)
    //    {
    //        if (request != null)
    //        {
    //            var encryptedDataKey = DefaultKey;
    //            var raw_content = request.GetParameter(Nacos.V2.Config.ConfigConstants.CONTENT);

    //            // 部分配置加密后的 content
    //            var content = ReplaceJsonNode((string)raw_content, encryptedDataKey, true);

    //            // 加密配置后，不要忘记更新 request !!!!
    //            request.PutParameter(Nacos.V2.Config.ConfigConstants.ENCRYPTED_DATA_KEY, encryptedDataKey);
    //            request.PutParameter(Nacos.V2.Config.ConfigConstants.CONTENT, content);
    //        }

    //        if (response != null)
    //        {
    //            var resp_content = response.GetParameter(Nacos.V2.Config.ConfigConstants.CONTENT);
    //            var resp_encryptedDataKey = response.GetParameter(Nacos.V2.Config.ConfigConstants.ENCRYPTED_DATA_KEY);

    //            // nacos 2.0.2 服务端目前还没有把 encryptedDataKey 记录并返回，所以 resp_encryptedDataKey 目前只会是 null
    //            // 如果服务端有记录并且能返回，我们可以做到每一个配置都用不一样的 encryptedDataKey 来加解密。
    //            // 目前的话，只能固定一个 encryptedDataKey 
    //            var encryptedDataKey = (resp_encryptedDataKey == null || string.IsNullOrWhiteSpace((string)resp_encryptedDataKey))
    //                    ? DefaultKey
    //                    : (string)resp_encryptedDataKey;

    //            var content = ReplaceJsonNode((string)resp_content, encryptedDataKey, false);
    //            response.PutParameter(Nacos.V2.Config.ConfigConstants.CONTENT, content);
    //        }
    //    }

    //    public string GetFilterName() => nameof(MyNacosConfigFilter);

    //    public int GetOrder() => 1;

    //    public void Init(NacosSdkOptions options)
    //    {
    //        // 从 Options 里面的拓展信息获取需要加密的 json path
    //        // 这里只是示例，根据具体情况调整成自己合适的！！！！
    //        var extInfo = JObject.Parse(options.ConfigFilterExtInfo);

    //        if (extInfo.ContainsKey("JsonPaths"))
    //        {
    //            // JsonPaths 在这里的含义是，那个path下面的内容要加密
    //            _jsonPaths = extInfo.GetValue("JsonPaths").ToObject<List<string>>();
    //        }
    //    }
    //}
}
