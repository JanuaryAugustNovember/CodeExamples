using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestWindowsServiceApi.Logging;

namespace TestWindowsServiceApi.BackgroundService
{
    public class LogBackgroundService : BackgroundService
    {
        // private readonly ILogger<LogBackgroundService> _logger;

        private static Lazy<Dictionary<Guid, Log>> BackgroundLogs = new Lazy<Dictionary<Guid, Log>>();

        public static Dictionary<Guid, Log> LogsInstance
        {
            get
            {
                return BackgroundLogs.Value;
            }
        }

        public LogBackgroundService(
            //ILogger<LogBackgroundService> logger
            )
        {
            //Constructor’s parameters validations...      
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // _logger.LogDebug($"GracePeriodManagerService is starting.");

            // stoppingToken.Register(() => _logger.LogDebug($" GracePeriod background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogDebug($"GracePeriod task doing background work.");

                foreach (var log in LogBackgroundService.LogsInstance)
                    Console.WriteLine(log.Value);

                LogBackgroundService.LogsInstance.Clear();

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }

            // _logger.LogDebug($"GracePeriod background task is stopping.");
        }

        //protected override async Task StopAsync(CancellationToken stoppingToken)
        //{
        //    // Run your graceful clean-up actions
        //}
    }
}
