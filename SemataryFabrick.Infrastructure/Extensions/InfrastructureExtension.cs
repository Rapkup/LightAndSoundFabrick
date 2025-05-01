using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Extensions;
public static class InfrastructureExtension
{
    public static async Task AddInfrastructureAsync(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));

        services.AddDbContext<ApplicationContext>(options => { options.UseNpgsql(connectionString); });

        return services;
    }
}
