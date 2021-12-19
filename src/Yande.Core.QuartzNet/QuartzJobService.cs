using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yande.Core.QuartzNet
{
    public static class QuartzJobService
    {
        public static void AddQuartzJobService(this IServiceCollection services)
        {
          //  if (services == null)
          //  {
          //      throw new ArgumentNullException(nameof(services));
          //  }

          //  services.AddSingleton<IJobFactory, YandeCoreJobFactory>();
          //  services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
          //  services.AddSingleton<QuartzMiddleJob>();

          //  services.AddSingleton<MyJobs>();
          //  services.AddSingleton(
          //      new YandeCoreJobSchedule(typeof(MyJobs), "0/1 * * * * ? ")
          //);

          //  services.AddHostedService<WeskyJobHostService>();
        }

    }
}
