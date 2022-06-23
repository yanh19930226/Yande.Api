using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeiShuSdk.Response
{
    public class GetTenantAccessTokenResponse : BaseResponse
    {
        [JsonProperty("tenant_access_token")]
        public string TenantAccessToken { get; set; }

        [JsonProperty("expire")]
        public int Expire { get; set; }
    }
}
