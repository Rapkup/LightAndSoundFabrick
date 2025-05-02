using System.Net;

namespace SemataryFabrick.Server.Middleware.CustomExceptionHandle;

public record ExceptionResponse(HttpStatusCode StatusCode, string Description);