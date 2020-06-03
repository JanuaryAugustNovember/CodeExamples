using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace TopShelfService
{
    internal static class ConfigureService
    {
        internal static void Configure()
        {
            HostFactory.Run(c =>
            {
                c.Service<Service>(service =>
                {
                    service.ConstructUsing(s => new Service());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });
                c.RunAsLocalSystem();
                c.SetServiceName("Test Topshelf Service");
                c.SetDisplayName("Test Topshelf Service");
                c.SetDescription("A test windows service using Topshelf");
            });
        }
    }
}
