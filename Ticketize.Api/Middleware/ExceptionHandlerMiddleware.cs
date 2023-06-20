using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Ticketize.Application.Exceptions;

namespace Ticketize.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private static Task ConvertException(HttpContext context, Exception ex) 
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            string result = string.Empty;
            HttpStatusCode httpStatusCode;
            switch (ex)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.ValidationErrors);
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                default:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;
            if (string.IsNullOrEmpty(result))
            {
                result = JsonSerializer.Serialize(new {error = ex.Message});    
            }

            return Task.FromResult(result);
        }
    }
}
