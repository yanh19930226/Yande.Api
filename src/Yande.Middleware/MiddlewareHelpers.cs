using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yande.Middleware
{
        public static class MiddlewareHelpers
        {
            public static IApplicationBuilder UseXxlJobExecutor(this IApplicationBuilder @this)
            {
                return @this.UseMiddleware<XxlJobExecutorMiddleware>();
            }
        }
}
