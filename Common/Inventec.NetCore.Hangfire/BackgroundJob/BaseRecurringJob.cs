using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.Hangfire.BackgroundJob
{
    public abstract class BaseRecurringJob : IDisposable
    {
        public Task ExecuteJob()
        {
            return this.Execute();
        }

        protected abstract Task Execute();

        protected virtual void DisposeJob() { }

        void IDisposable.Dispose()
        {
            DisposeJob();
        }
    }
}
