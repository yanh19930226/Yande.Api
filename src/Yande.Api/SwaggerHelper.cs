using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Yande.Api
{
    public class SwaggerHelper
    {
        //递归结果集
        List<InterfaceRequestField> requestFieldList = new List<InterfaceRequestField>();
        public void Run(string path)
        {
            //文档标题信息
            InterfaceVersion interfaceVersion = new InterfaceVersion();
            //所有接口信息
            List<InterfaceMethod> interfaceMethods = new List<InterfaceMethod>();
            //所有接口字段信息
            List<InterfaceRequestField> requestFields = new List<InterfaceRequestField>();

            //读取Json
            JObject jToken = ReadJsonTxt(path);

            var info = jToken.SelectToken("info");
            var paths = jToken.SelectToken("paths");
            var schemas = jToken.SelectToken("components.schemas");

            //文档信息
            interfaceVersion.title = info["title"].ToString();
            interfaceVersion.version = info["version"].ToString();

            foreach (var item in paths)
            {
                InterfaceMethod model = new InterfaceMethod();
                model.route = ((JProperty)item).Name.ToString();
                model.type = ((JProperty)item.First().First()).Name.ToString();
                string controllerName = item.First().First().First["tags"][0].ToString();
                model.name = controllerName + "-" + RouteName(model.route);

                foreach (var item2 in item)
                {
                    //model.summary = item2.SelectToken("post.['summary']").Value<string>();
                    interfaceMethods.Add(model);
                    //请求实体名称
                    var requestModelName = RequestModelName(item2);
                    //请求字段集合
                    requestFields.AddRange(SbRequestData(schemas, requestModelName, model.name, ParamTypeEnum.入参));
                    requestFieldList.Clear();
                    //返回实体及字段
                    var responseModelName = ResponseModelName(item2);
                    //请求字段集合
                    requestFields.AddRange(SbRequestData(schemas, responseModelName, model.name, ParamTypeEnum.出参));
                    requestFieldList.Clear();
                }
            }
            //生成word帮助类
            new WordHelper().CreateWordFile(interfaceVersion, interfaceMethods, requestFields);
        }

        /// <summary>
        /// 无限套娃 递归
        /// </summary>
        /// <param name="schemas">实体类集合</param>
        /// <param name="modelName">实体类名称</param>
        /// <param name="methodName">方法名称</param>
        /// <returns></returns>
        private List<InterfaceRequestField> SbRequestData(JToken schemas, string modelName, string methodName, ParamTypeEnum paramType)
        {
            List<InterfaceRequestField> result = new List<InterfaceRequestField>();
            string requestModelName = string.Empty;
            requestModelName = modelName;
            //获取请求字段
            foreach (var item in schemas.SelectToken($"{modelName}.properties"))
            {
                string fieldName = ((JProperty)item).Name.ToString();
                var fieldType = item.Parent.SelectToken($"{fieldName}.type").ToString();

                //必填参数集合
                var mustList = MustList(schemas, requestModelName);

                var fieldInfo = JsonConvert.DeserializeObject<FieldDescription>(item.First().ToString());
                requestFieldList.Add(FieldData(item, fieldInfo, mustList, methodName, paramType));

                //第一个集合没有参数
                if (fieldType == "array")
                {
                    requestModelName = RequestModelName2(item, fieldName);
                    //递归
                    SbRequestData(schemas, requestModelName, methodName, paramType);
                }
            }
            return requestFieldList;
        }

        /// <summary>
        /// 通过方法 找请求实体名称
        /// </summary>
        /// <param name="json">paths</param>
        /// <returns></returns>
        private string RequestModelName(JToken json)
        {
            var requestModelName = json.SelectToken("post.['requestBody'].['content'].['application/json'].schema")["$ref"].Value<string>();
            List<string> names = requestModelName.Split('/').ToList();
            return names[names.Count - 1];
        }

        /// <summary>
        /// 集合中包含集合 继续找下一个集合
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name">字段名称</param>
        /// <returns></returns>
        private string RequestModelName2(JToken json, string name)
        {
            var requestModelName = json.Parent.SelectToken($"{name}.items")["$ref"].Value<string>();
            List<string> names = requestModelName.Split('/').ToList();
            return names[names.Count - 1];
        }

        /// <summary>
        /// 返回实体名称
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private string ResponseModelName(JToken json)
        {
            var responseModelName = json.SelectToken("post.['responses'].200.['content'].['application/json'].schema")["$ref"].Value<string>();
            List<string> names = responseModelName.Split('/').ToList();
            return names[names.Count - 1];
        }


        /// <summary>
        /// 获取实体必填参数
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name">方法实体类名称</param>
        /// <returns></returns>
        private List<string> MustList(JToken json, string name)
        {
            List<string> mustList = new List<string>();
            var str = json.SelectToken($"{name}.required");
            if (str == null)
            {
                return mustList;
            }
            if (string.IsNullOrWhiteSpace(str.ToString()))
            {
                return mustList;
            }
            var list = str.ToString().Split(',').ToList();
            foreach (var flied in list)
            {
                string f = flied.TrimStart('[').TrimEnd(']').Replace("\r\n", "").TrimStart('"').TrimEnd('"').Trim().TrimStart('"');
                mustList.Add(f);
            }
            return mustList;
        }

        private InterfaceRequestField FieldData(JToken json, FieldDescription field, List<string> mustList, string type, ParamTypeEnum paramType)
        {
            InterfaceRequestField result = new InterfaceRequestField();
            result.code = ((JProperty)json).Name.ToString();
            result.type = field.type;
            result.description = field.description;
            result.isMust = mustList.Contains(result.code) ? true : false;
            result.MethodType = type;
            result.paramType = paramType.GetHashCode();
            result.maxLength = field.maxLength;
            return result;
        }

        public JObject ReadJsonTxt(string path)
        {
            StreamReader SR = File.OpenText(path);
            JsonTextReader JTR = new JsonTextReader(SR);
            JObject json = (JObject)JToken.ReadFrom(JTR);
            json = JObject.Parse(JsonConvert.SerializeObject(json));
            return json;
        }


        /// <summary>
        /// 路由方法名称
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string RouteName(string str)
        {
            string name = string.Empty;
            List<string> routes = str.Split('/').ToList();
            name = routes[routes.Count - 1];
            return name;
        }
    }

    public class InterfaceVersion
    {
        public string title { get; set; }
        public string version { get; set; }
    }

    public class InterfaceMethod
    {
        /// <summary>
        /// 路由
        /// </summary>
        public string route { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// controller 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 接口说明
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// 请求集合
        /// </summary>
        public string requesyBody { get; set; }

        /// <summary>
        /// 返回实体
        /// </summary>
        public string responses { get; set; }
    }


    public class InterfaceRequestField
    {
        /// <summary>
        /// 字段
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool isMust { get; set; }

        /// <summary>
        /// 方法类型
        /// </summary>
        public string MethodType { get; set; }


        /// <summary>
        /// 字段长度
        /// </summary>
        public int maxLength { get; set; }

        /// <summary>
        /// 1.入参 2.出参
        /// </summary>
        public int paramType { get; set; }
    }

    public enum ParamTypeEnum
    {
        入参 = 1,
        出参 = 2
    }

    public class FieldDescription
    {
        public string type { get; set; }

        public string description { get; set; }
        /// <summary>
        /// 字段长度
        /// </summary>
        public int maxLength { get; set; }
    }
}
