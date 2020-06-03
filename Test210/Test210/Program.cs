
using System.Diagnostics;
using System.IO;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.AspNetCore.Server.HttpSys;

namespace Test210
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);

            // commented out for docker purposes
            //var host = WebHost.CreateDefaultBuilder(args)
            //    .UseContentRoot(pathToContentRoot)
            //    .UseStartup<Startup>()
            //    .UseHttpSys(options =>
            //    {
            //        options.Authentication.Schemes = AuthenticationSchemes.None;
            //        options.Authentication.AllowAnonymous = true;
            //        //o.UrlPrefixes.Add("http://+:5001");
            //    })
            //    .Build();

            var host = WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(pathToContentRoot)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    
                })
                //.UseUrls("http://*:5003")
                .UseUrls("http://0.0.0.0:5003") // linux containers need the 0s
                .Build();

            if (Debugger.IsAttached)
                host.Run();
            else
                host.Run(); //host.RunAsService(); // commented out for docker purposes
        }
    }
}
