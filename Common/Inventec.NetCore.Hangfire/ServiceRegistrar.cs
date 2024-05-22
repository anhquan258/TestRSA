using Hangfire.Dashboard;
using Hangfire;
using Inventec.NetCore.Hangfire.BackgroundJob;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Hangfire.Oracle.Core;
using Newtonsoft.Json;

namespace Inventec.NetCore.Hangfire
{
    public static class ServiceRegistrar
    {
        //[Obsolete]
        public static void AddHangfireOragle(this IServiceCollection services, string connectionString, string schemaName)
        {
            services.AddHangfire(o =>
            {
                o.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
                o.UseSimpleAssemblyNameTypeSerializer();
                o.UseRecommendedSerializerSettings();
                o.UseStorage(new OracleStorage(connectionString,
                    new OracleStorageOptions
                    {
                        InvisibilityTimeout = TimeSpan.FromHours(3),
                        QueuePollInterval = TimeSpan.FromMilliseconds(500),
                        SchemaName = schemaName
                    })
                    );
                o.UseSerializerSettings(new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            });
            //services.AddScoped<IBackgroundJobClient, BackgroundJobClient>();
            services.AddScoped<IBackgroundJobManager, BackgroundJobManager>();

            services.AddHangfireServer(o =>
            {
                o.ServerName = "Inventec";
            });
        }

        public static IApplicationBuilder UseBackgroundJobServer(this IApplicationBuilder app)
        {
            var options = new DashboardOptions
            {
                Authorization = new[] {
                    new DashboardAuthorizationFilter(new[]
                    {
                        new HangfireUserCredentials
                        {
                            Username = "dunglh",
                            Password = "Abc@1234"
                        }
                    },
                    true)
                }
            };
            // for local development, process all valid queues
            app.UseHangfireDashboard("/hangfire", options);

            return app;
        }

        public static IEndpointConventionBuilder MapHangfireDashboard(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapHangfireDashboard();
        }

        public static void AddHangfireSchedule<TJob>(this IApplicationBuilder app, string jobId, string cronExpression) where TJob : BaseRecurringJob
        {
            RecurringJob.AddOrUpdate<TJob>(jobId, o => o.ExecuteJob(), cronExpression, new RecurringJobOptions { TimeZone = TimeZoneInfo.Local });
        }

    }
}