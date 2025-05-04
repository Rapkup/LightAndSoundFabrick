using System.Net;

namespace SemataryFabrickUI.Middleware.CustomExceptionHandle;

public record ExceptionResponse(HttpStatusCode StatusCode, string Description);