using SemataryFabrick.Application.Extensions;
using SemataryFabrick.Infrastructure.Extensions;
using SemataryFabrickUI.Middleware.CustomExceptionHandle;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();


await builder.Services.AddInfrastructureAsync(builder.Configuration);
await builder.Services.AddApplicationLayerAsync(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
