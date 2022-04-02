using DotXxlJob.Core;
using DotXxlJob.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yande.Api.Jobs
{
    [JobHandler("demoJobHandler")]
    public class DemoJobHandler : AbstractJobHandler
    {
        public override Task<ReturnT> Execute(JobExecuteContext context)
        {
            context.JobLogger.Log("yande.Api receive demo job handler,parameter:{0}", context.JobParameter);

            return Task.FromResult(ReturnT.SUCCESS);
        }
    }
}
