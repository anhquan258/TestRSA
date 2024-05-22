using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.Hangfire.BackgroundJob
{
    public interface IBackgroundJobManager
    {
        /// <summary>
        /// Stops a running background job
        /// </summary>
        /// <param name="jobOrTempUiSessionId">NOTE: If using a tempUiSessionId that value is lost on restarts</param>
        /// <returns></returns>
        Task<bool> Cancel(string jobOrTempUiSessionId);

        /// <summary>
        /// Queues a typed job
        /// </summary>
        /// <typeparam name="TJob">Job Type which must be registered with DI</typeparam>
        /// <typeparam name="TJobParam">Parameter MUST be serializable
        /// <para>Properties of jobs should not contain file Data or large lists, if those are needed, please store the cache key in the paramter and use IRemoteCache to retrieve</para>
        /// </typeparam>
        /// <param name="param"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        string Enqueue<TJob, TJobParam>(TJobParam param, string sessionId = null)
            where TJob : BaseBackgroundJob<TJobParam>
            where TJobParam : class;

        /// <summary>
        /// Schedule a job in future
        /// </summary>
        /// <typeparam name="TJob">Job Type which must be registered with DI</typeparam>
        /// <typeparam name="TJobParam">Parameter MUST be serializable
        /// <para>Properties of jobs should not contain file Data or large lists, if those are needed, please store the cache key in the paramter and use IRemoteCache to retrieve</para>
        /// </typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        string Schedule<TJob, TJobParam>(TJobParam param, DateTimeOffset enqueueAt, string sessionId = null)
            where TJob : BaseBackgroundJob<TJobParam>
            where TJobParam : class;

        /// <summary>
        /// Add or update a recurring job
        /// <para>
        /// Never rely on contexts in jobs, always extract necessary information and pass as parameters to the Job method
        /// </para>
        /// </summary>
        /// <param name="job">Job to run in background</param>
        /// <returns></returns>
        string AddOrUpdate<TJob>([NotNull] string cronExpression) where TJob : BaseRecurringJob;

        /// <summary>
        /// Remove a recurring job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        void RemoveIfExists(string jobId);
    }
}
