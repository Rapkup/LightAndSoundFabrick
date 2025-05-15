using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SemataryFabrick.Infrastructure.Extensions.InMemoryDb;
public class DatabaseInitializerHostedService : IHostedService
{
    private readonly IServiceProvider _sp;

    public DatabaseInitializerHostedService(IServiceProvider sp) => _sp = sp;

    public Task StartAsync(CancellationToken ct)
    {
        using var scope = _sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<InMemoryDatabase>();
        new InMemoryDataSeeder(db).SeedAll();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}