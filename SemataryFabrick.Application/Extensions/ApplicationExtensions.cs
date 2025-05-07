using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemataryFabrick.Application.Implementations;
using SemataryFabrick.Application.Contracts.Services;

namespace SemataryFabrick.Application.Extensions;
public static class ApplicationExtensions
{
    public static async Task AddApplicationLayerAsync(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICartItemService, CartItemService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<IItemInventoryService, ItemInventoryService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IOrderBaseService, OrderBaseService>();
        services.AddScoped<IOrderCrewService, OrderCrewService>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        services.AddScoped<IProductCategoryService, ProductCategoryService>();
        services.AddScoped<ISubCategoryService, SubCategoryService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWorkTaskAssignmentService, WorkTaskAssignmentService>();
        services.AddScoped<IWorkTaskService, WorkTaskService>();

        return services;
    }
}
