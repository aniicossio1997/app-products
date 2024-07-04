using Newtonsoft.Json;
using System.Net;

namespace app_products.Exceptions.Handler
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string message;
            if (exception is PersonalException personalException)
            {
                statusCode = personalException.StatusCode;
                message = exception.Message;
            }
            else if (exception is IncorrectIdException incorrectIdException)
            {
                statusCode = HttpStatusCode.NotFound; // Código 404 Not Found para IncorrectIdException
                message = exception.Message; // Usa el mensaje de la excepción original
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError; // Cualquier otra excepción, 500 Internal Server Error
                message = "Ocurrió un error inesperado.";
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = JsonConvert.SerializeObject(new { error = message });
            return context.Response.WriteAsync(result);
        }
    }

    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
