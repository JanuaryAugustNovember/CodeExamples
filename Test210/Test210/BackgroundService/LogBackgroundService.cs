
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Test210.Infrastructure;
using Test210.Logging;

namespace Test210.BackgroundService
{
    public class LogBackgroundService : BackgroundService
    {
        private readonly IElasticRepository _elasticRepository;

        private readonly AppSettings _appSettings;

        private static Lazy<Dictionary<Guid, ApiLog>> BackgroundLogs = new Lazy<Dictionary<Guid, ApiLog>>();

        public static Dictionary<Guid, ApiLog> LogsInstance
        {
            get
            {
                return BackgroundLogs.Value;
            }
        }

        public LogBackgroundService(IOptions<AppSettings> appSettings, IElasticRepository elasticRepository)
        {
            _appSettings = appSettings.Value;
            _elasticRepository = elasticRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // stoppingToken.Register(() => _logger.LogDebug($" LogBackgroundService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                if (LogsInstance.Any())
                {
                    foreach (var log in LogBackgroundService.LogsInstance)
                        Console.WriteLine($"CorrelationId: {log.Value.CorrelationId}" +
                            $"\nLogType: {log.Value.LogType}" +
                            $"\nVersion: {log.Value.Version}" +
                            $"\nMethod: {log.Value.Method}" +
                            $"\nPath: {log.Value.Path}" +
                            $"\nBody: {log.Value.RequestBody}");

                    foreach (var log in LogsInstance)
                        _elasticRepository.SaveApiLog(log.Value);

                    LogBackgroundService.LogsInstance.Clear();
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

        // Done in BackgroundService
        //protected override async Task StopAsync(CancellationToken stoppingToken)
        //{
        //    // Run your graceful clean-up actions
        //}
    }
}
