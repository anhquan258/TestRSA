using Hangfire;
using Inventec.NetCore.Hangfire.BackgroundJob;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.Hangfire
{
    public class BackgroundJobManager : IBackgroundJobManager
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public BackgroundJobManager(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public string AddOrUpdate<TJob>([NotNull] string cronExpression) where TJob : BaseRecurringJob
        {
            var jobId = DateTime.Now.Ticks.ToString();
            RecurringJob.AddOrUpdate<TJob>(jobId, o => o.ExecuteJob(), cronExpression, new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });
            return jobId;
        }

        public async Task<bool> Cancel(string jobOrTempUiSessionId)
        {
            return await Task.FromResult(this._backgroundJobClient.Delete(jobOrTempUiSessionId));
        }

        public string Enqueue<TJob, TJobParam>(TJobParam param, string sessionId = null)
            where TJob : BaseBackgroundJob<TJobParam>
            where TJobParam : class
        {
            var jobId = this._backgroundJobClient.Enqueue<TJob>(o => o.InitializeAndExecute(sessionId, param, CancellationToken.None));
            return jobId;
        }

        public void RemoveIfExists(string recurringJobId)
        {
            RecurringJob.RemoveIfExists(recurringJobId);
        }

        public string Schedule<TJob, TJobParam>(TJobParam param, DateTimeOffset enqueueAt, string sessionId = null)
            where TJob : BaseBackgroundJob<TJobParam>
            where TJobParam : class
        {
            var jobId = this._backgroundJobClient.Schedule<TJob>(o => o.InitializeAndExecute(sessionId, param, CancellationToken.None), enqueueAt);
            return jobId;
        }
    }
}
