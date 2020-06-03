using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWindowsServiceApi.Middleware
{
    public class LogRequestMiddleware
    {
        private readonly RequestDelegate next;

        public LogRequestMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    var requestBodyStream = new MemoryStream();
        //    var originalRequestBody = context.Request.Body;

        //    await context.Request.Body.CopyToAsync(requestBodyStream);
        //    requestBodyStream.Seek(0, SeekOrigin.Begin);

        //    var url = UriHelper.GetDisplayUrl(context.Request);
        //    var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
        //    //_logger.Log(LogLevel.Information, 1, $"REQUEST METHOD: {context.Request.Method}, REQUEST BODY: {requestBodyText}, REQUEST URL: {url}");

        //    Console.WriteLine($"~~~~~~~~~~ URL: {url}");
        //    Console.WriteLine($"~~~~~~~~~~ Method: {context.Request.Method}");
        //    Console.WriteLine($"~~~~~~~~~~ Request: {requestBodyText}");

        //    requestBodyStream.Seek(0, SeekOrigin.Begin);
        //    context.Request.Body = requestBodyStream;

        //    await next(context);
        //    context.Request.Body = originalRequestBody;
        //}

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Value.Contains("swagger"))
            {
                //_logger.LogInformation(await FormatRequest(context.Request));
                Console.WriteLine(await FormatRequest(context.Request));

                var originalBodyStream = context.Response.Body;

                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await next(context);

                    //_logger.LogInformation(await FormatResponse(context.Response));
                    Console.WriteLine(await FormatResponse(context.Response));

                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            else
            {
                await next(context);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableRewind();
            var body = request.Body;

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;

            return $"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Request {request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Response {text}";
        }
    }
}
