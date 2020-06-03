using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestWindowsServiceApi.Middleware
{
    public class LogResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogResponseMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Response.ContentLength == null)
            {
                _logger.Log(LogLevel.Information, 1, $"RESPONSE:[null]", null);
                await _next(context);
                return;
            }

            if (context.Response.ContentLength == 0)
            {
                _logger.Log(LogLevel.Information, 1, $"RESPONSE:[zero length]", null);
                await _next(context);
                return;
            }



            var bodyStream = context.Response.Body;

            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await _next(context);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = new StreamReader(responseBodyStream).ReadToEnd();
            _logger.Log(LogLevel.Information, 1, $"RESPONSE LOG: {responseBody}");
            responseBodyStream.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(bodyStream);
        }
    }
}
