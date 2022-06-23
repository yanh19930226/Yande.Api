using System;
using Xunit;
using YandeSignSdk;
using YandeSignSdk.Models.Tests;

namespace TestProject1
{
    public class UnitTest1
    {
        private readonly YandeSignClient _client;

        public UnitTest1()
        {
            var pk= @"MIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQDLGrLQk3StP6WV
Gn4wpY2nHUjfuKvIAq2szkRIb+vvjrOnkUTr+lPryavKkkKeH0Uu23++MfUZ7f+S
X8nusaXV3TDUr12st3cCcHG5piKFj8Yy2f9XNbPW4cTP8+HYXREwRMcy1t/H8SQA
PJcIoKtgdyImVl0lXe0kA8U8QqOr9xwaGjYHgdAMUZ3K+6BCpwt+eMqX0Fhmkxa0
3SQInbZYrXU30ZHTIHYsYTEQ0YLQMaGHvDMMEhh8sPcr2b7ldharaKuN5I9FbI5J
TmvIsXqUcoKEGFWbF6BXr9hcsTlq/9MsAv4r1YEMdNGTGUrx85Wa+HqXJuGqgBPd
XiSuyhRxAgMBAAECggEAFE+1Njqo3nKRAppFqGPGCMJa+VSkiToLWE7AcWYP+EMT
IFzcwaSlI91lOrrpwppp9seovMKOOmuctWyy6xYE+aBHM93dxloKosnP9brxlN3N
+mPmrzgFpiWp4woGufaEs4kNSr5GXt50tTlZ+VjoCpufZKoaYpREFOfzn5UifR3p
qpJkoZmIeWvnf6uO6jaQRxbOVDXdqBQwInLukhxGilHa89757rzcxiTOBMb2dyyc
BzDyZvl74Oe1/Avc0frTlb7zPiz/PvKjD9fyfn0CN15H7QGsSW4uL58hMPMv9xKz
oYkCEFvgdojTff6+IrzlIdTU8jayHLTeEEhaDQG4AQKBgQDnpyJXCzMSMJq9JS1F
5KyIHzLQ3giTbBsl52PCOE/NN/zkuPhhy8Z7u3O629NEZ/oYUTByiLKBaYGjSEym
dIjdYLbXCVfpYpnnfMxrlqtwrmrSuEEbD3spJJ5GdiMX2aN38dxO8AOkw/dr4lSZ
fO70XRRS4HP3bysuYyAECM2UMQKBgQDgc29FFsivNmWstt+EP5wz7/zgsAH5ZjPP
u57/B2yWJAiOZZ5K6cp9kNEtBlEory+tT/ma3L9VDFOKbSB5ePyhZ1gICHwv7Tep
H6qcLzsOBjI2MN2pgtfuGzDJSOTPEAu1cl9qWzn2b/fjgpcQ3LizulaSJpS/cfo/
MVcc7HW0QQKBgDoFm81PidAximB70lYiS7Cgl9rG8kDyn+4jgdIgxqQxNM+ZIOVl
4+YT8o3IB6enn3W9yDO313Yglg7uyfwbTjicw88ikPls7/2SPaVpDLHhjfIPoocD
nqaUfGxLpMiRg6dEVhkTduYrC2rjOqtjJnrgYXnMrF86YgdxXpCEH/HRAoGAHHeH
jUOugEpt6tHm12cZ8JxnfjfUiEUmHRq2t7HBW+mGDElnIik0vWg4n3VFpdtSOLED
/1gwDCFcFxpwG/f0UqRAzgQFfC6h+JlDkjuLSeQPSkA4XN9zc4ePUHgmTPzD/2da
IqsfVtosnnZZopHb+y+O+0pZY06ZZppjag+zfgECgYBm8qhcrecW42SJkCXDm+n1
E3/ojQOMYGbW6v2Qy5RJE3PJb5kgpDtb7o3SS10TS0noenw50ClFt+eaqnLZhrNy
XO6sXOvN+Z9aexXFqvzhZYodSKtYfAodSVn3ASxGghd7CjWnYQfPNrkaAO4Tx6iq
+bCSpBMKRP3BpOYoa82WUQ==";
            _client = new YandeSignClient(EnvEnum.Dev, "yande-rsa", "1", pk);
        }

        [Fact]
        public void Test1()
        {
            var para = new TestModel()
            {
                Id = "Test"
            };

            var res =  _client.PostRequestAsync(new TestReq(para));

            Assert.Equal("", "");
        }
    }
}
