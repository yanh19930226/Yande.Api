using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YandeSignSdk.Commons;
using YandeSignSdk.Models;

namespace YandeSignSdk
{
    /// <summary>
    /// YandeSignClient
    /// </summary>
    public class YandeSignClient
    {
        private HttpClient _client { get; set; }
        private readonly EnvEnum _envEnum;
        private readonly string _signtype;
        private readonly string _appId;
        private readonly string _privateKey;

        public YandeSignClient(EnvEnum envEnum, string signtype, string appId, string privateKey)
        {
            _envEnum = envEnum;
            _signtype= signtype;
            _appId= appId;
            _privateKey = privateKey;
            _client = new HttpClient();
        }

        private string GetApiBaseUrl()
        {
            switch (_envEnum)
            {
                case EnvEnum.Dev:
                    return "http://121.40.183.223/apiTest";
                case EnvEnum.Prod:
                    return "https://www.zhiguanyin.com.cn";
                default:
                    return "http://121.40.183.223/apiTest";
            }
        }

        /// <summary>
        /// 构造参数字典
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private Dictionary<string, string> ToDictionary(object obj)
        {
            if (null == obj)
            {
                return null;
            }

            Dictionary<string, string> map = new Dictionary<string, string>();

            Type t = obj.GetType(); // 获取对象对应的类， 对应的类型

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance); // 获取当前type公共属性

            foreach (PropertyInfo p in pi)
            {
                MethodInfo m = p.GetGetMethod();

                if (m != null && m.IsPublic)
                {
                    // 进行判NULL处理 
                    if (m.Invoke(obj, new object[] { }) != null)
                    {
                        if (m.ReturnType == typeof(string))
                        {
                            map.Add(p.Name, (string)m.Invoke(obj, new object[] { })); // 向字典添加元素
                        }
                        else
                        {
                            map.Add(p.Name, JsonConvert.SerializeObject(m.Invoke(obj, new object[] { }))); // 向字典添加元素
                        }
                    }
                }
            }
            return map;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public K PostRequestAsync<T, K>(BaseRequset<T, K> request)
        {
            var client = new RestClient(GetApiBaseUrl());
            var rq = new RestRequest(request.Uri, DataFormat.Json);

            var timestamp = UtcTime.CurrentTimeMillis();
            var nonce = Guid.NewGuid().ToString("n");
            var requestBodyString = JsonConvert.SerializeObject(request.Param);
            var method = "POST";

            string signBodyString = $"{method}\n{request.Uri}\n{timestamp}\n{nonce}\n{requestBodyString}\n";

            //使用app的秘钥进行加签
            string prikey = RsaKeyConvert.RSAPrivateKeyJava2DotNet(_privateKey);
            var signature = SecurityFunc.RSASignCSharp(signBodyString.ToString(), prikey);

            //设置请求头
            rq.AddHeader("signtype", _signtype);
            rq.AddHeader("appid", _appId);
            rq.AddHeader("nonce", nonce);
            rq.AddHeader("timestamp", timestamp.ToString());
            rq.AddHeader("signature", signature);

            rq.AddJsonBody(JsonConvert.SerializeObject(request.Param));

            var httpResponse = client.Post(rq).Content;

            var result = JsonConvert.DeserializeObject<K>(httpResponse);

            return result;

        }
    }
}
