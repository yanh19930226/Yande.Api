using Microsoft.Extensions.DependencyInjection;
using SMS.TencentCloud.Models;
using System;
using YandeSignApi.Applications.Sms;

namespace SMS.TencentCloud
{

    public static class ServiceCollectionExtensions
    {

        public static void AddTencentCloudSMS(this IServiceCollection services, Action<SMSSetting> action)
        {
            services.Configure(action);

            services.AddTransient<ISMS, TencentCloudSMS>();
        }
    }
}