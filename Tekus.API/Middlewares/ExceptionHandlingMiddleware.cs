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
            catch (NotFoundException ex) // 👈 MÁS ESPECÍFICA PRIMERO
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await WriteError(context, ex.Message);
            }
            catch (DomainException ex) // 👈 MÁS GENERAL DESPUÉS
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await WriteError(context, ex.Message);
            }
            catch (Exception) // 👈 ÚLTIMO RECURSO
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await WriteError(context, "Unexpected error");
            }
        }

        private static async Task WriteError(HttpContext context, string message)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new { error = message })
            );
        }
    }
}
