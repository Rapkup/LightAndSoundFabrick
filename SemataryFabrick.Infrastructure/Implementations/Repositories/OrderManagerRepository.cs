using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class OrderManagerRepository(ApplicationContext context)
        : RoleRepositoryBase<OrderManager>(context),
        IOrderManagerRepository
{
    protected override UserType GetUserType() => UserType.OrderManager;
    public Task AddOrderManagerAsync(OrderManager orderManager) => CreateAsync(orderManager);

    public void DeleteOrderManager(OrderManager orderManager) => Delete(orderManager);
    public void UpdateOrderManager(OrderManager orderManager) => Update(orderManager);

    public async Task<IEnumerable<OrderManager>> GetAllOrderManagersAsync()
        => await Find().ToListAsync();
    public async Task<OrderManager?> GetOrderManagerAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}