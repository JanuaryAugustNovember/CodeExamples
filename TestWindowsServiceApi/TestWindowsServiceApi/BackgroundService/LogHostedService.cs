
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestWindowsServiceApi.Logging;

namespace TestWindowsServiceApi.BackgroundService
{
    public class LogHostedService : IHostedService, IDisposable
    {
        // private readonly ILogger _logger;
        private Timer _timer;

        private static Lazy<Dictionary<Guid, Log>> BackgroundLogs = new Lazy<Dictionary<Guid, Log>>();

        public static Dictionary<Guid, Log> LogsInstance
        {
            get
            {
                return BackgroundLogs.Value;
            }
        }

        public LogHostedService(ILogger<LogHostedService> logger)
        {
            //_logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("Timed Background Service is starting.");

            Task.Factory.StartNew(() =>
            {
                _timer = new Timer(SaveLogs, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            }, TaskCreationOptions.LongRunning);


            return Task.CompletedTask;
        }

        private void SaveLogs(object state)
        {
            foreach(var log in LogHostedService.LogsInstance)
                Console.WriteLine(log.Value);

            LogHostedService.LogsInstance.Clear();

            //_logger.LogInformation("Timed Background Service is working.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
