using FluentValidation;
using Notes.Application.Common.Exceptions;
using System.Text.Json;

namespace Notes.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                Console.WriteLine("try _next delegate");
                await _next(context);
            }
            catch (Exception e)
            {
                Console.WriteLine("oops... an error!");
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int code;
            string result = string.Empty;
            switch (exception)
            {
                case ValidationException validationException:
                    code = StatusCodes.Status400BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case NotFoundException:
                    code = StatusCodes.Status404NotFound;
                    break;
                default:
                    code = StatusCodes.Status500InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }
            return context.Response.WriteAsync(result);
        }
    }
}
