using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.Mediator
{
    public static class ServiceRegistrar
    {
        public static void AddMediator(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(options => options.AsScoped(), assemblies);
            services.AddScoped<Publisher>();
        }
    }
}
