using FeiShuSdk.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public class FeiShuTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal("", "");
        }

        /// <summary>
        /// 获取token
        /// </summary>
        [Fact]
        public async Task<string> TestGetTenantAccessToken()
        {

            //if (!_memoryCache.TryGetValue("token", out string cacheValue))
            //{
            //    //测试获取token
            //    var req = new GetTenantAccessTokenRequest();
            //    req.AppId = AppSetting.FeiShuAppId;
            //    req.AppSecret = AppSetting.FeiShuAppSecret;

            //    var getTenantAccessTokenResponse = await _feishuClient.ExcueAsync(req);

            //    cacheValue = getTenantAccessTokenResponse.TenantAccessToken;

            //    var cacheEntryOptions = new MemoryCacheEntryOptions()
            //        .SetSlidingExpiration(TimeSpan.FromSeconds(getTenantAccessTokenResponse.Expire));
            //    _memoryCache.Set("token", cacheValue, cacheEntryOptions);

            //    Assert.True(getTenantAccessTokenResponse.Code == 0);

            //}

            return cacheValue;

        }
    }
}
