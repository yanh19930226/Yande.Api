using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In66.Authentication.Rsa
{
    public static class RequestExtension
    {

        public static async Task<string> RequestBodyAsync(this HttpRequest request)
        {
            var buffsize = (int)(request.ContentLength ?? 10240);
            if (buffsize > 0)
            {
                string datas;
                request.EnableBuffering();
                request.Body.Position = 0;
                using (var reader = new StreamReader(request.Body, encoding: Encoding.UTF8, true, buffsize, true))
                {
                    datas = await reader.ReadToEndAsync();
                    request.Body.Position = 0;
                }
                return datas;
            }
            return string.Empty;
        }
    }
}
