
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nest;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using Test210.BackgroundService;
using Test210.Infrastructure;
using Test210.Middleware;
using Test210.Repositories;

namespace Test210
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public static bool DidItWork { get; set; }

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            // https://www.humankode.com/asp-net-core/logging-with-elasticsearch-kibana-asp-net-core-and-docker
            //var elasticUri = configuration["AppSettings:ElasticSearchUri"];
            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.FromLogContext()
            //    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
            //    {
            //        AutoRegisterTemplate = true
            //    })
            //    .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // https://github.com/stevejgordon/IHostedServiceSample/pull/1/commits/55efb8707f6e0a92671e4181481dd81f508c83bc
            // services.AddSingleton<IHostedService, LogBackgroundService>();
            services.AddHostedService<LogBackgroundService>();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddTransient<IElasticRepository, ElasticRepository>();
            services.AddTransient<IApplication, Application>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Test .NET Core API", Version = "v1" });
            });

            // research this
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, Microsoft.AspNetCore.Hosting.IApplicationLifetime applicationLifetime, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            applicationLifetime.ApplicationStopping.Register(OnShutDown);
            applicationLifetime.ApplicationStarted.Register(OnStartup);

            app.LogRequestMiddleware();

            app.UseMvc();

            loggerFactory.AddSerilog();

            // microsoft.aspnetcore.staticfiles is needed for it to load swagger HTML correctly
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });
        }


        private void OnStartup()
        {
            // what was I testing here?
            // var restService = new RestService("http://localhost:8081/api/Test");
            // string configValue = restService.SendRequest<string>(RestSharp.Method.GET);


            Console.WriteLine("************************************* Application Started *************************************");
        }

        private void OnShutDown()
        {
            Console.WriteLine("************************************* Application Stopped *************************************");
            // code here for shutdown logic
            // alert
        }
    }
}
