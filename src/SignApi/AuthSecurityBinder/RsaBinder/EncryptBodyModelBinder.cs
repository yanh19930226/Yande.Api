using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SignApi.Commons;
using System.Threading.Tasks;

namespace SignApi.AuthSecurityBinder.RsaBinder
{
    public class EncryptBodyModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var httpContext = bindingContext.HttpContext;
            //if (bindingContext.ModelType != typeof(string))
            //    return;
            string authorization = httpContext.Request.Headers["AuthSecurity-Authorization"];
            if (!string.IsNullOrWhiteSpace(authorization))
            {
                //有参数接收就反序列化并且进行校验
                if (bindingContext.ModelType != null)
                {
                    //获取请求体
                    var encryptBody = await httpContext.Request.RequestBodyAsync();
                    if (string.IsNullOrWhiteSpace(encryptBody))
                        return;

                    //将请求体解密绑定到模型上
                    var rsaOptions = httpContext.RequestServices.GetService<RsaOptions>();

                    var body = RsaFunc.Decrypt(rsaOptions.PrivateKey, encryptBody);

                    var request = JsonConvert.DeserializeObject(body, bindingContext.ModelType);
                    if (request == null)
                    {
                        return;
                    }
                    bindingContext.Result = ModelBindingResult.Success(request);
                }
            }
        }
    }
}
