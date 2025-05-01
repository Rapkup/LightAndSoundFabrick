using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class OrderItemRepository(ApplicationContext context) : RepositoryBase<OrderItem>(context), IOrderItemRepository
{
    public Task AddOrderItemAsync(OrderItem orderItem) => CreateAsync(orderItem);

    public void DeleteOrderItem(OrderItem orderItem) => Delete(orderItem);

    public void UpdateOrderItem(OrderItem orderItem) => Update(orderItem);

    public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        => await Find().ToListAsync();

    public async Task<OrderItem?> GetOrderItemAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}