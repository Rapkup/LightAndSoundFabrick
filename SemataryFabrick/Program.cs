using SemataryFabrick.Infrastructure.Extensions;
using SemataryFabrick.Server.Middleware.CustomExceptionHandle;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

await builder.Services.AddInfrastructureAsync(builder.Configuration);

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();