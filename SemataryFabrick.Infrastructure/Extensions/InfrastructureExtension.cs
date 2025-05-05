using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Infrastructure.Implementations.Contexts;
using SemataryFabrick.Infrastructure.Implementations.Repositories;

namespace SemataryFabrick.Infrastructure.Extensions;
public static class InfrastructureExtension
{
    public static async Task AddInfrastructureAsync(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext(configuration)
            .AddRepositories()
            .AddDataSeeders();

        await services.AddSeedDataForDevelopmentAsync();
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));

        services.AddDbContext<ApplicationContext>(options => { options.UseNpgsql(connectionString); });

        return services;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        return services;
    }

    private static IServiceCollection AddDataSeeders(this IServiceCollection services)
    {
        services.AddScoped<DataSeederExtension>();

        return services;
    }

    private static async Task AddSeedDataForDevelopmentAsync(this IServiceCollection services)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            if (!context.Items.Any())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<DataSeederExtension>();

                await seeder.SeedDataAsync();
            }

        }
    }
}
