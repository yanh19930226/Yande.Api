using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using YandeSignApi.Applications.Commons;
using YandeSignApi.Models.Dtos.Reqs;

namespace YandeSignConsole
{

    #region 参考微信文档设计
    //签名生成：微信支付API v3 要求商户对请求进行签名。微信支付会在收到请求后进行签名的验证。如果签名验证不通过，微信支付API v3将会拒绝处理请求，并返回401 Unauthorized

    //构造签名串
    //我们希望商户的技术开发人员按照当前文档约定的规则构造签名串。微信支付会使用同样的方式构造签名串。
    //如果商户构造签名串的方式错误，将导致签名验证不通过。下面先说明签名串的具体格式。
    //签名串一共有五行，每一行为一个参数。行尾以 \n（换行符，ASCII编码值为0x0A）结束，包括最后一行。如果参数本身以\n结束，也需要附加一个\n

    //HTTP请求方法\n
    //URL\n
    //请求时间戳\n
    //请求随机串\n
    //请求报文主体\n

    //1.获取HTTP请求的方法（GET,POST,PUT）等
    //GET
    //2.获取请求的绝对URL，并去除域名部分得到参与签名的URL。如果请求中有查询参数，URL末尾应附加有'?'和对应的查询字符串
    ///v3/certificates
    //3.获取发起请求时的系统当前时间戳，即格林威治时间1970年01月01日00时00分00秒(北京时间1970年01月01日08时00分00秒)起至现在的总秒数，作为请求时间戳。微信支付会拒绝处理很久之前发起的请求，请商户保持自身系统的时间准确
    //1554208460
    //4.生成一个请求随机串，可参见生成随机数算法。这里，我们使用命令行直接生成一个
    //593BEC0C930BF1AFEB40B4A08C8FB242
    //5.获取请求中的请求报文主体
    //   请求方法为GET时，报文主体为空
    //   当请求方法为POST或PUT时，请使用真实发送的JSON报文
    //6.按照前述规则，构造的请求签名串为:
    //    GET\n 
    //    /v3/certificates\n 
    //    1554208460\n 
    //    593BEC0C930BF1AFEB40B4A08C8FB242\n 
    //    \n
    //计算签名值:Sign()
    //设置HTTP头:微信支付商户API v3要求请求通过HTTP Authorization头来传递签名。 Authorization由认证类型和签名信息两个部分组成
    //具体组成为：
    //1.认证类型，目前为WECHATPAY2-SHA256-RSA2048
    //2.签名信息
    //发起请求的商户（包括直连商户、服务商或渠道商）的商户号 mchid
    //商户API证书序列号serial_no，用于声明所使用的证书
    //请求随机串nonce_str
    //时间戳timestamp
    //签名值signature
    //注：以上五项签名信息，无顺序要求 
    #endregion

    internal class Program
    {
        private const string AppPrivateKey = @"MIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQDLGrLQk3StP6WV
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
        private const string AppId = "1";

        static void Main(string[] args)
        {
            using (var httpClient = new HttpClient())
            {
                Test2(httpClient);
            }
            Console.WriteLine("Hello World!");
        }
        

        static void Test2(HttpClient httpClient)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/test/test2");
            var method = requestMessage.Method;
            var uri = requestMessage.RequestUri.PathAndQuery;
            var timestamp = UtcTime.CurrentTimeMillis();
            var nonce = Guid.NewGuid().ToString("n");
          

            TestReq testReq = new TestReq();
            testReq.Id= "123";
            var requestBodyString = JsonConvert.SerializeObject(testReq);

            string signBodyString = $"{method}\n{uri}\n{timestamp}\n{nonce}\n{requestBodyString}\n";

            //使用app的秘钥进行加签
            String prikey = RsaKeyConvert.RSAPrivateKeyJava2DotNet(AppPrivateKey);
            var signature = SecurityFunc.RSASignCSharp(signBodyString.ToString(), prikey);

            //设置请求头
            requestMessage.Headers.Add("signtype", "yande-rsa");
            requestMessage.Headers.Add("appid", AppId);
            requestMessage.Headers.Add("nonce", nonce);
            requestMessage.Headers.Add("timestamp", timestamp.ToString());
            requestMessage.Headers.Add("signature", signature);

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(testReq), Encoding.UTF8, "application/json");
            var response = httpClient.SendAsync(requestMessage).GetAwaiter().GetResult();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
            {
                Console.WriteLine("错了");
            }
        }
    }
}
