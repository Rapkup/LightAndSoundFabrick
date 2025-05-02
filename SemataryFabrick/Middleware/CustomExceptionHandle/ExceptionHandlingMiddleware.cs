using Npgsql;
using System.Net.Sockets;
using System.Net;
using SemataryFabrick.Application.Entities.Exceptions;

namespace SemataryFabrick.Server.Middleware.CustomExceptionHandle;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ExceptionResponse response;
        switch (exception)
        {
            case EntityNotFoundException ex:
                response = new ExceptionResponse(HttpStatusCode.BadRequest, ex.Message);
                logger.LogWarning(ex, response.Description);
                break;
            case EntityDuplicationException ex:
                response = new ExceptionResponse(HttpStatusCode.BadRequest, ex.Message);
                logger.LogWarning(ex, response.Description);
                break;
            case InvalidOperationException { InnerException: NpgsqlException { InnerException: SocketException } } ex:
                response = new ExceptionResponse(
                    HttpStatusCode.InternalServerError,
                    "Please verify connection to the Database."
                );
                logger.LogError(ex, response.Description);
                break;
            default:
                response = new ExceptionResponse(
                    HttpStatusCode.InternalServerError,
                    "Internal server error. Please retry later."
                );
                logger.LogError(exception, response.Description);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}