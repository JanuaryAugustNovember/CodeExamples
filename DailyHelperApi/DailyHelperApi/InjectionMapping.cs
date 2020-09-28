using DailyHelperApi.Domain.Internal;
using DailyHelperApi.Repository;
using DailyHelperApi.Repository.Context;
using DailyHelperApi.Repository.Interfaces;
using DailyHerlperApi.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DailyHelperApi
{
    public static class InjectionMapping
    {
        public static void ConfigureTransients(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerApplication, CustomerApplication>();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings")); //Inject IOptions to utilize
        }

        public static void ConfigureContext(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            services.AddDbContext<SourceContext>(options => options.UseSqlServer(Configuration.GetSection("AppSettings").GetValue<string>("SourceConnectionString")));
        }
    }
}
