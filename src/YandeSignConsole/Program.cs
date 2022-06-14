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
    //   1554208460\n 
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
        private const string AppPrivateKey = "<RSAKeyValue><Modulus>vKQbdjZ4b4foZOq4RkUfQ4COkJ6htxW+S1fTZvTC8HbaaicTYWi9WICzeTd5PuAaDBwztk0HsS3r6ds1HSy//Wb7JsBE8ynrALaVPApNn54yQsTqEPmuGiTMYEBFIdyNwKzgdxFz6MWO7An2yWsenC0IEpSdntL918eVixt4aKOl39mtftSK2vVBhL+tljLzTkk6KZvjFGmGxf4dZFeSlU7H8BQ+zQKfkyViDeKgewFqrRsc2JGCCKChr1paVampBE/lpb6hzUPLUUQbpFHKBlIwtmut7m+Ly0fylbO0lSO06Q3CoEPLYfc81dTnPvMKRO6SEgFPpQBepqhLzedRTw==</Modulus><Exponent>AQAB</Exponent><P>7zl5GNpKU89rcew0fnQUp/uMdPsb3+MAYkQgzXYxqV6FSLKuxIoU1E7UMXswGESHaVO10KO2LqubgjIJvmosFvFSeB2IvUnUax6/K6fMR+mddzAiOFPhH3DHGVHyJ9NVOLSSZxnXGEqZMLPxWxO0cfmW5yJPJxIUKnZpgy+IaKk=</P><Q>yd6RC+nJYq+0fKET12eaYQZNb/8uxywDIL5RFGS09coJfCsN+U4iM/RHFxghmVeAAdFxvF0uERdXfxnD8MyHHvQAEnjyXqKRJIKRVHHQ8VZ5Rq9nr3xGhZeIYk780J6fRS2VyJmgXYplPxbkCgW1P4rN/tBalS9hhny2gAvuTTc=</Q><DP>hOLjLvgLc9TztXvliR0IYGvukQjwagTaMLvxkNCIM7JKzaBcTtb5TRpg6v+oLsLaiZqzk6ttRy2Sm9cZ7Ilj5na1Pf4B+Ewr0DlrLl/urT/LderqB2oo0uM95gXMQ200mORNszH6dwbxY8mBV/txMCLaPZikaWq0gwX2BKaB2sk=</DP><DQ>HS6hhTlctXl0+/dFKQR/GruQgjo/hudj5F3e1rXgOw/j4yFOOdYDt8L+a+Y/JS2zAZBHgtVtjWb0bRlKbAsFFYJsaD83ulqB5OdDHxP9AoZfrco5kPLENxe6zYthnL7xg0ydtIwQ1LTnAgHLIW/FzdPBB68TCTH6RTjOISCYaG0=</DQ><InverseQ>1C8H6ll3tpSdjm2TrK7nYDVgMT6ckT2Sm0y/mN4kHW8j+4YkEv9pDbyUVypjBGMGD5FO/MuK/Pg+1MZaKkb2ExHnmCIzjmTRqKb+jz9WKFEMvUpOJ9zvlAsU0k6ElIlLoFO1aP8ULFBNDXqMbdENNIsGDCrkwGWHJcvxangy6Ew=</InverseQ><D>OlGt38UFRM3KjfB22dqiyLak3Jb+PeDt/NMBG1JONhM4gRrlhfbgmsznL3Fz/XlA9D9/yTtVRnSA+8J2UDe2fzvoJ1nHtzldWtIXnwE8cD1zImtIRck7BwAbYyJbfRV3iXqoxobRw8PX5KdL8Yc5ZmURmtTxSdnG+n/Mfr4WYpq03i0OA1Z8EIwJ41G1AIep1smQl1lQ6gZY4YqoGVyfFR7jlDcTOhr7A5aru9W006CMXFZzQOVimLIN5NjnSmI8wvbmwPhKGnP6FfKFQsOCApb8cOxJ2LC36czw0clEtMfzBiHRY33kcPm2QlEtxjcJBjVJzkK/4UVNYDSg4w0HMQ==</D></RSAKeyValue>";
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
            var signature = SecurityFunc.CreateSignature(AppPrivateKey, signBodyString.ToString());

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

        static void Test3(HttpClient httpClient)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/api/test/test3");
            var timestamp = UtcTime.CurrentTimeMillis();
            var requestId = Guid.NewGuid().ToString("n");

            TestReq testReq = new TestReq();
            testReq.Id = "123";
            var requestBody = JsonConvert.SerializeObject(testReq);

            var encryptBodyString = SecurityFunc.ToHex(SecurityFunc.ComputeSha256Hash(requestBody));

            #region 按规则构造加签字符串

            //第一步：构造字典
            IDictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("appid", AppId);
            dic.Add("timestamp", timestamp.ToString());
            dic.Add("requestId", requestId);
            dic.Add("body", encryptBodyString);

            // 第二步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(dic);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();
            // 第三步：把所有参数名和参数值串在一起
            StringBuilder signBodyString = new StringBuilder();
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key))
                {
                    signBodyString.Append(key).Append("=").Append(value).Append("&");
                }
            }

            #endregion

            var signature = SecurityFunc.CreateSignature(AppPrivateKey, signBodyString.ToString());

            requestMessage.Headers.Add("appid", AppId);
            requestMessage.Headers.Add("requestid", requestId);
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
