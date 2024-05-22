using Inventec.NetCore.AcsToken.Authen;
using Inventec.NetCore.AcsToken.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.AcsToken.Services
{
    public static class ServiceRegistrar
    {
        public static void AddAcsAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication("AcsToken").AddScheme<AcsAuthenSchemeOptions, AcsAuthenticationHandler>(AcsAuthenSchemeOptions.Scheme, op => { });
            services.AddSingleton<IAcsUserContextService, AcsUserContextService>();
            services.Configure<AcsOptionConfig>(Configuration.GetSection(nameof(AcsOptionConfig)));

        }
    }
}
