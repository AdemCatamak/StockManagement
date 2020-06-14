using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StockManagement.Exceptions;
using ErrorHttpResponse = StockManagement.Api.Contracts.Responses.ErrorHttpResponse;

namespace StockManagement.Api.WebMiddleware
{
    public class GeneralExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GeneralExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<GeneralExceptionHandlerMiddleware> logger)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                await HandleException(e, logger, context);
            }
        }

        private async Task HandleException(Exception exception, ILogger logger, HttpContext context)
        {
            string logMessage = await BuildLogMessageAsync(context, exception);

            string message = exception.Message;
            string typeOfException = exception.GetType().Name;
            HttpStatusCode httpStatusCode;
            LogLevel logLevel;

            if (exception is ValidationException)
            {
                httpStatusCode = HttpStatusCode.BadRequest;
                logLevel = LogLevel.Information;
            }
            else if (exception is NotFoundException)
            {
                httpStatusCode = HttpStatusCode.NotFound;
                logLevel = LogLevel.Information;
            }
            else if (exception is ConflictException)
            {
                httpStatusCode = HttpStatusCode.Conflict;
                logLevel = LogLevel.Information;
            }
            else
            {
                message = "Unknown error Occurs";
                typeOfException = null;
                httpStatusCode = HttpStatusCode.InternalServerError;
                logLevel = LogLevel.Error;
            }

            logger.Log(logLevel, default, typeof(GeneralExceptionHandlerMiddleware), exception, (type, ex) => logMessage);

            var errorHttpResponse = new ErrorHttpResponse(message, typeOfException);
            string errorHttpContentStr = JsonConvert.SerializeObject(errorHttpResponse);

            context.Response.StatusCode = (int) httpStatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(errorHttpContentStr);
        }

        private async Task<string> BuildLogMessageAsync(HttpContext context, Exception exception)
        {
            await using (var requestBodyStream = new MemoryStream())
            {
                IEnumerable<string> headerLine = context.Request.Headers.Select(pair => $"{pair.Key} => {string.Join("|", pair.Value.ToList())}");
                string headerText = string.Join(Environment.NewLine, headerLine);

                await context.Request.Body.CopyToAsync(requestBodyStream);
                requestBodyStream.Seek(0, SeekOrigin.Begin);

                string requestBodyText = await new StreamReader(requestBodyStream).ReadToEndAsync();

                string message = $"Context-TraceId: {context.TraceIdentifier}{Environment.NewLine}" +
                                 $"Headers: {headerText}{Environment.NewLine}" +
                                 $"Request: {context.Request.Scheme} {context.Request.Host}{context.Request.Path} {context.Request.QueryString}{Environment.NewLine}" +
                                 $"{requestBodyText}{Environment.NewLine}" +
                                 $"Exception: {exception}";

                requestBodyStream.Seek(0, SeekOrigin.Begin);
                context.Request.Body = requestBodyStream;

                return message;
            }
        }
    }
}