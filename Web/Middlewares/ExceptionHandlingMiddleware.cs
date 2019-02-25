using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Web.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private const string JsonResponseType = "application/json";
        private readonly RequestDelegate _request;

        public ExceptionHandlingMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception exception)
            {
                await HandlingException(context, exception);
            }
        }

        private async Task HandlingException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = JsonResponseType;
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new
            {
                Error = exception.Message,
                exception.StackTrace
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
