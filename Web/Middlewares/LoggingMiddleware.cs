using System;
using System.IO;
using System.Threading.Tasks;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Web.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(RequestDelegate request, FileLogger logger)
        {
            _next = request;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stream originalResponseBody = context.Response.Body;

            try
            {
                using (var responseBodyMemory = new MemoryStream())
                {
                    context.Response.Body = responseBodyMemory;

                    // Reading URL and Body from Request
                    var currentDate = DateTime.Now;
                    var loggingRequest = new LoggingRequest
                    {
                        Method = context.Request.Method,
                        Url = context.Request.Scheme + "://" + context.Request.Host.Value + context.Request.Path + context.Request.QueryString.Value,
                        DateTime = currentDate.ToShortDateString() + ' ' + currentDate.ToLongTimeString()
                    };

                    context.Request.EnableRewind();

                    var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
                    loggingRequest.Body = JsonConvert.DeserializeObject(requestBody);

                    context.Request.Body.Position = 0;

                    await _next(context);

                    // Reading Status code and Body from Response
                    var loggingResponse = new LoggingResponse
                    {
                        StatusCode = context.Response.StatusCode
                    };

                    responseBodyMemory.Position = 0;
                    var responseBody = JsonConvert.DeserializeObject(new StreamReader(responseBodyMemory).ReadToEnd());
                    loggingResponse.Body = responseBody;

                    var loggingObject = new LoggingObject { Request = loggingRequest, Response = loggingResponse };
                    var loggingObjectEncoded = JsonConvert.SerializeObject(loggingObject);

                    // Writing all info to file
                    if (loggingObject.Response.StatusCode == StatusCodes.Status500InternalServerError)
                    {
                        _logger.Log(LogLevel.Error, loggingObjectEncoded);
                    }
                    else
                    {
                        _logger.Log(LogLevel.Information, loggingObjectEncoded);
                    }

                    responseBodyMemory.Position = 0;
                    await responseBodyMemory.CopyToAsync(originalResponseBody);
                }
            }
            finally
            {
                context.Response.Body = originalResponseBody;
            }
        }
    }
}
