using Notes.WebApi.Middleware;

namespace Notes.WebApi
{
    public static class DependencyInjection
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
