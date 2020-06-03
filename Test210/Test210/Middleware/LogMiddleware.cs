
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Test210.BackgroundService;
using Test210.Infrastructure;
using Test210.Logging;
using Test210.Repositories;

namespace Test210.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly AppSettings _appSettings;

        private readonly IElasticRepository _elasticRepo;

        public LogMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, IElasticRepository elasticRepo)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _elasticRepo = elasticRepo;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Value.Contains("swagger"))
            {
                LogBackgroundService.LogsInstance.Add(Guid.NewGuid(), await FormatRequest(context.Request));

                var apiLog = await FormatRequest(context.Request);

                try
                {
                    var originalBodyStream = context.Response.Body;

                    using (var responseBody = new MemoryStream())
                    {
                        context.Response.Body = responseBody;

                        await _next(context);

                        // LogBackgroundService.LogsInstance.Add(Guid.NewGuid(), await FormatResponse(context.Response));
                        apiLog.ResponseBody = await FormatResponseBody(context.Response);
                        LogBackgroundService.LogsInstance.Add(apiLog.CorrelationId, apiLog);

                        await responseBody.CopyToAsync(originalBodyStream);
                    }
                }
                catch(Exception exe)
                {
                    // LogBackgroundService.LogsInstance.Add(Guid.NewGuid(), await FormatResponse(context.Response, exe));
                    apiLog.ResponseBody = await FormatResponseBody(context.Response, exe);
                    LogBackgroundService.LogsInstance.Add(apiLog.CorrelationId, apiLog);
                }

            }
            else
            {
                await _next(context);
            }
        }

        private async Task<ApiLog> FormatRequest(HttpRequest request)
        {
            request.EnableRewind();
            var body = request.Body;

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyText = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;

            var log = new ApiLog
            {
                CorrelationId = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                LogType = LogType.Request,
                Version = _appSettings.Version,
                RequestBody = bodyText,
                Method = request.Method,
                Path = $"{request.Host}{request.Path}{request.QueryString}"
            };

            return log;
        }

        private async Task<ApiLog> FormatResponse(HttpResponse response, Exception exception = null)
        {
            if (exception == null)
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                var bodyText = await new StreamReader(response.Body).ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);

                var log = new ApiLog
                {
                    CorrelationId = Guid.NewGuid(),
                    LogType = LogType.Response,
                    Timestamp = DateTime.Now,
                    Version = _appSettings.Version,
                    ResponseBody = bodyText
                };

                return log;
            }
            else
            {
                var log = new ApiLog
                {
                    CorrelationId = Guid.NewGuid(),
                    LogType = LogType.Response,
                    Timestamp = DateTime.Now,
                    Version = _appSettings.Version,
                    ResponseBody = exception.ToString()
                };

                return log;
            }
        }

        private async Task<string> FormatResponseBody(HttpResponse response, Exception exception = null)
        {
            if (exception == null)
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                var bodyText = await new StreamReader(response.Body).ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);

                return bodyText;
            }
            else
            {
                return exception.ToString();
            }
        }
    }
}
