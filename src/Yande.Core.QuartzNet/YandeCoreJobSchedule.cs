using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yande.Core.QuartzNet
{
    public class YandeCoreJobSchedule
    {
        public YandeCoreJobSchedule(Type jobType, string cronExpression)
        {
            this.JobType = jobType ?? throw new ArgumentNullException(nameof(jobType));
            CronExpression = cronExpression ?? throw new ArgumentNullException(nameof(cronExpression));
        }
        /// <summary>
        /// Job类型
        /// </summary>
        public Type JobType { get; private set; }
        /// <summary>
        /// Cron表达式
        /// </summary>
        public string CronExpression { get; private set; }
        /// <summary>
        /// Job状态
        /// </summary>
        public JobStatus JobStatu { get; set; } = JobStatus.Init;
    }

    /// <summary>
    /// 运行状态
    /// </summary>
    public enum JobStatus : byte
    {
        [Description("Initialization")]
        Init = 0,
        [Description("Running")]
        Running = 1,
        [Description("Scheduling")]
        Scheduling = 2,
        [Description("Stopped")]
        Stopped = 3,

    }
}
