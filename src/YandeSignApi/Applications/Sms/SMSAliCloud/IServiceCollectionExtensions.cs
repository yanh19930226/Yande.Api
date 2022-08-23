using Microsoft.Extensions.DependencyInjection;
using Sms;
using Sms.SMSAliCloud;
using System;
using YandeSignApi.Applications.Sms;

namespace SMS.AliCloud
{

    public static class ServiceCollectionExtensions
    {

        public static void AddAliCloudSMS(this IServiceCollection services, Action<SMSSetting> action)
        {
            services.Configure(action);

            services.AddTransient<ISMS, AliCloudSMS>();

        }
    }
}