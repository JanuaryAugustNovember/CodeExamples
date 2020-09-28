using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.Swagger;

namespace DailyHelperApi
{
    public class Startup
    {
        //private string Version { get; set; }

        //public IConfiguration Configuration { get; }

        //public Startup(IConfiguration configuration, IHostingEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //        //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
        //        .AddEnvironmentVariables();

        //    Configuration = builder.Build();

        //    var appSettings = Configuration.GetSection("AppSettings");
        //    Version = appSettings.GetValue<string>("ApiVersion");
        //}

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        //    services.AddOptions();

        //    // VStudio added this by default, is it required?
        //    //services.AddControllers();

        //    services.ConfigureTransients(Configuration);
        //    services.ConfigureContext(Configuration);

        //    services.AddSwaggerGen(x =>
        //    {
        //        x.SwaggerDoc(this.Version, new Info { title = "Daily Helper Api", version = this.Version });
        //        x.DescribeAllEnumsAsStrings();
        //    });
        //}


        //public void Configure(IApplicationBuilder app, IHostingEnvironment env) //IWebHostEnvironment env) this was here by default instead of IHostingEnvironment
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseHttpsRedirection();

        //    app.UseRouting();

        //    app.UseAuthorization();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //    });
        //}
    }
}
