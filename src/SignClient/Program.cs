using Newtonsoft.Json;
using SignApi.Commons;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace SignClient
{
    internal class Program
    {
        private const string SysPublicKey = "<RSAKeyValue><Modulus>x29/gr6KX2UwgDrXE44bcg9/F1A4PcmINeBlIEbjR3Yv/hj/mNj1hlPedMrDpGK37A1xZ6W46eoXvDSY2c3mbJwlbpevjCz/GEBshUkDHXRN/kzemr3rOGI+sL47RwBSaCKG1L1oZ0zD5hAuApx0W5noKjGDPAVElW/OvjQ0vHiA1UDYazaqMXw8jBx4uXZR1tjJQ8+f6hvOE+pc7Qq3wHmNkfCw+u8Llg+p+MtIfQ5kK6+R0/E4dJ8IjuftDKBCRkD2yDKo25tLIx0q/SN8DFNvxUIPMoaHYUP7GAHbRTSYMvTVUm+MIAFLc4YetsxRhWw8TD9AMIM7jo87Vq4ENQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private const string AppPrivateKey = "<RSAKeyValue><Modulus>vKQbdjZ4b4foZOq4RkUfQ4COkJ6htxW+S1fTZvTC8HbaaicTYWi9WICzeTd5PuAaDBwztk0HsS3r6ds1HSy//Wb7JsBE8ynrALaVPApNn54yQsTqEPmuGiTMYEBFIdyNwKzgdxFz6MWO7An2yWsenC0IEpSdntL918eVixt4aKOl39mtftSK2vVBhL+tljLzTkk6KZvjFGmGxf4dZFeSlU7H8BQ+zQKfkyViDeKgewFqrRsc2JGCCKChr1paVampBE/lpb6hzUPLUUQbpFHKBlIwtmut7m+Ly0fylbO0lSO06Q3CoEPLYfc81dTnPvMKRO6SEgFPpQBepqhLzedRTw==</Modulus><Exponent>AQAB</Exponent><P>7zl5GNpKU89rcew0fnQUp/uMdPsb3+MAYkQgzXYxqV6FSLKuxIoU1E7UMXswGESHaVO10KO2LqubgjIJvmosFvFSeB2IvUnUax6/K6fMR+mddzAiOFPhH3DHGVHyJ9NVOLSSZxnXGEqZMLPxWxO0cfmW5yJPJxIUKnZpgy+IaKk=</P><Q>yd6RC+nJYq+0fKET12eaYQZNb/8uxywDIL5RFGS09coJfCsN+U4iM/RHFxghmVeAAdFxvF0uERdXfxnD8MyHHvQAEnjyXqKRJIKRVHHQ8VZ5Rq9nr3xGhZeIYk780J6fRS2VyJmgXYplPxbkCgW1P4rN/tBalS9hhny2gAvuTTc=</Q><DP>hOLjLvgLc9TztXvliR0IYGvukQjwagTaMLvxkNCIM7JKzaBcTtb5TRpg6v+oLsLaiZqzk6ttRy2Sm9cZ7Ilj5na1Pf4B+Ewr0DlrLl/urT/LderqB2oo0uM95gXMQ200mORNszH6dwbxY8mBV/txMCLaPZikaWq0gwX2BKaB2sk=</DP><DQ>HS6hhTlctXl0+/dFKQR/GruQgjo/hudj5F3e1rXgOw/j4yFOOdYDt8L+a+Y/JS2zAZBHgtVtjWb0bRlKbAsFFYJsaD83ulqB5OdDHxP9AoZfrco5kPLENxe6zYthnL7xg0ydtIwQ1LTnAgHLIW/FzdPBB68TCTH6RTjOISCYaG0=</DQ><InverseQ>1C8H6ll3tpSdjm2TrK7nYDVgMT6ckT2Sm0y/mN4kHW8j+4YkEv9pDbyUVypjBGMGD5FO/MuK/Pg+1MZaKkb2ExHnmCIzjmTRqKb+jz9WKFEMvUpOJ9zvlAsU0k6ElIlLoFO1aP8ULFBNDXqMbdENNIsGDCrkwGWHJcvxangy6Ew=</InverseQ><D>OlGt38UFRM3KjfB22dqiyLak3Jb+PeDt/NMBG1JONhM4gRrlhfbgmsznL3Fz/XlA9D9/yTtVRnSA+8J2UDe2fzvoJ1nHtzldWtIXnwE8cD1zImtIRck7BwAbYyJbfRV3iXqoxobRw8PX5KdL8Yc5ZmURmtTxSdnG+n/Mfr4WYpq03i0OA1Z8EIwJ41G1AIep1smQl1lQ6gZY4YqoGVyfFR7jlDcTOhr7A5aru9W006CMXFZzQOVimLIN5NjnSmI8wvbmwPhKGnP6FfKFQsOCApb8cOxJ2LC36czw0clEtMfzBiHRY33kcPm2QlEtxjcJBjVJzkK/4UVNYDSg4w0HMQ==</D></RSAKeyValue>";
        private const string AppId = "1";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static void Test2(HttpClient httpClient)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/api/test/test2");
            var timestamp = UtcTime.CurrentTimeMillis();
            var requestId = Guid.NewGuid().ToString("n");
            var requestBody = JsonConvert.SerializeObject(new { Id = "123" });
            //var requestBody = JsonConvert.SerializeObject(new { Id1 = "123" });//测试模型校验
            var encryptBody = RsaFunc.Encrypt(SysPublicKey, requestBody);
            var signBody = requestId + AppId + timestamp + encryptBody;
            var signature = RsaFunc.CreateSignature(AppPrivateKey, signBody);
            var rsaAuthorization = requestId + "." + AppId + "." + timestamp + "." + signature;

            requestMessage.Headers.Add("AuthSecurity-Authorization", rsaAuthorization);
            requestMessage.Content = new StringContent(encryptBody, Encoding.UTF8, "application/json");
            var response = httpClient.SendAsync(requestMessage).GetAwaiter().GetResult();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
            {
                Console.WriteLine("错了");
            }
            var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var rsaSignature = response.Headers.Contains("AuthSecurity-Signature")
                ? response.Headers.FirstOrDefault(o => o.Key == "AuthSecurity-Signature").Value.FirstOrDefault() : null;
            if (rsaSignature == null)
            {

                Console.WriteLine($"响应结果:{responseBody}");
                return;
            }

            var responseSignBody = requestId + AppId + responseBody;
            if (!RsaFunc.ValidateSignature(SysPublicKey, responseSignBody, rsaSignature))
            {

                Console.WriteLine("非法响应");
                return;
            }

            var responseJson = RsaFunc.Decrypt(AppPrivateKey, responseBody);
            Console.WriteLine($"响应结果:{responseJson}");
        }

        static void Test3(HttpClient httpClient)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/api/test/test3");
            var timestamp = UtcTime.CurrentTimeMillis();
            var requestId = Guid.NewGuid().ToString("n");
            var requestBody = JsonConvert.SerializeObject(new { Id = "123" });
            var encryptBody = RsaFunc.Encrypt(SysPublicKey, requestBody);
            var signBody = requestId + AppId + timestamp + encryptBody;
            var signature = RsaFunc.CreateSignature(AppPrivateKey, signBody);
            var rsaAuthorization = requestId + "." + AppId + "." + timestamp + "." + signature;

            requestMessage.Headers.Add("AuthSecurity-Authorization", rsaAuthorization);
            requestMessage.Content = new StringContent(encryptBody, Encoding.UTF8, "application/json");
            var response = httpClient.SendAsync(requestMessage).GetAwaiter().GetResult();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
            {
                Console.WriteLine("错了");
            }
            var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var rsaSignature = response.Headers.Contains("AuthSecurity-Signature")
                ? response.Headers.FirstOrDefault(o => o.Key == "AuthSecurity-Signature").Value.FirstOrDefault() : null;
            if (rsaSignature == null)
            {

                Console.WriteLine($"响应结果:{responseBody}");
                return;
            }

            var responseSignBody = requestId + AppId + responseBody;
            if (!RsaFunc.ValidateSignature(SysPublicKey, responseSignBody, rsaSignature))
            {

                Console.WriteLine("非法响应");
                return;
            }

            var responseJson = RsaFunc.Decrypt(AppPrivateKey, responseBody);
            Console.WriteLine($"响应结果:{responseJson}");
        }
    }
}
