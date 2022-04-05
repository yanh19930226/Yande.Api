using Nacos.V2;
using System.Collections.Generic;

namespace Yande.Api.NacosParser
{
    public class XmlConfigurationStringParser : INacosConfigurationParser
    {
        public static XmlConfigurationStringParser Instance = new XmlConfigurationStringParser();

        public IDictionary<string, string> Parse(string input)
        {
            // 具体的解析逻辑
            return new Dictionary<string, string>();
        }
    }
}
