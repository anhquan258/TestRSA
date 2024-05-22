using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.Hangfire.BackgroundJob
{
    public abstract class BaseBackgroundJob<T> : IDisposable
        where T : class
    {
        private bool IsInitialized = false;
        protected string UiSessionId { get; private set; } = string.Empty;

        protected virtual void DisposeJob() { }
        void IDisposable.Dispose()
        {
            if (!this.IsInitialized) { return; }
            this.DisposeJob();
        }

        protected void Initialize(string sessionid)
        {
            this.UiSessionId = sessionid ?? string.Empty;
            this.IsInitialized = true;
        }

        private Task ExecuteJob(object parameter, CancellationToken cancellationToken)
        {
            return parameter is T typed
                  ? this.Execute(typed, cancellationToken)
                  : parameter is JObject j
                      ? this.Execute(j.ToObject<T>(), cancellationToken)
                      : throw new ArgumentException(nameof(parameter));
        }

        protected abstract Task Execute(T arg, CancellationToken cancellationToken);

        public async Task InitializeAndExecute(string uiSessionId, object parameter, CancellationToken cancellationToken)
        {
            this.Initialize(uiSessionId);

            await this.ExecuteJob(parameter, cancellationToken);
        }
    }
}
