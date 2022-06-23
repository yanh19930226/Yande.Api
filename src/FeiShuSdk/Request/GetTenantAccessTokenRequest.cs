using FeiShuSdk.Constant;
using FeiShuSdk.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeiShuSdk.Request
{
    public class GetTenantAccessTokenRequest : BaseRequest<GetTenantAccessTokenResponse>
    {

        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("app_secret")]
        public string AppSecret { get; set; }

        public override IDictionary<string, object> GetParameters()
        {
            IDictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("app_id", this.AppId);
            keyValues.Add("app_secret", this.AppSecret);
            return keyValues;
        }

        public override string GetUrl()
        {
            return $"{FeiShuConstant.BASE_URL}/open-apis/auth/v3/tenant_access_token/internal";
        }
    }
}
