﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Yande.Core.WebHelper
{
    public class HttpClientHelper : IHttpClientHelper
    {

        readonly ILogger<HttpClientHelper> _logger;

        public HttpClientHelper(ILogger<HttpClientHelper> logger)
        {
            _logger = logger;
        }

        public T Post<T>(string url, string data)
        {
            var result = default(T);
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.Timeout = new TimeSpan(0, 0, 10); // 10是秒数，用于设置超时时长
                    HttpContent content = new StringContent(data);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Connection.Add("keep-alive");
                    Task<HttpResponseMessage> res = client.PostAsync(url, content);
                    if (res.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        result = JsonConvert.DeserializeObject<T>(res.Result.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        _logger.LogError($"访问webapi方法状态码错误：\r url:{url} \r data:{data} \r 状态码:{res.Result.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"访问webapi方法异常：\r url:{url} \r data:{data} \r 异常信息:{ex.Message}");
                }
                finally
                {
                    client.Dispose();
                }
            }
            return result;
        }

        public T Post<T>(string url, string data, string account, string pwd)
        {
            var result = default(T);
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string authorization = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(account + ":" + pwd));
                    client.DefaultRequestHeaders.Add("authorization", authorization);

                    client.Timeout = new TimeSpan(0, 0, 10);
                    HttpContent content = new StringContent(data);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    Task<HttpResponseMessage> res = client.PostAsync(url, content);
                    if (res.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        result = JsonConvert.DeserializeObject<T>(res.Result.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        _logger.LogError($"访问带用户名参数的webapi方法状态码错误：\r url:{url} \r data:{data} \r 状态码:{res.Result.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"访问带用户名参数的webapi方法异常：\r url:{url} \r data:{data} \r 异常信息:{ex.Message}");
                }
                finally
                {
                    client.Dispose();
                }
            }
            return result;
        }

    }
}
