using System.Net;
using System.Text.Json;
using Tekus.Domain.Exceptions;

namespace Tekus.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                if (context.Response.HasStarted) throw;

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await WriteError(context, ex.Message, "NOT_FOUND");
            }
            catch (DomainException ex)
            {
                if (context.Response.HasStarted) throw;

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await WriteError(context, ex.Message, "VALIDATION_ERROR");
            }
            catch (Exception)
            {
                if (context.Response.HasStarted) throw;

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await WriteError(context, "Ocurrió un error inesperado", "UNEXPECTED_ERROR");
            }
        }

        private static async Task WriteError(
            HttpContext context,
            string message,
            string code)
        {
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new
                {
                    error = message,
                    code
                })
            );
        }
    }
}