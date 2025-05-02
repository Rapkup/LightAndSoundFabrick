using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IOrderItemRepository
{
    void DeleteOrderItem(OrderItem orderItem);
    void UpdateOrderItem(OrderItem orderItem);
    Task AddOrderItemAsync(OrderItem orderItem);
    Task<OrderItem?> GetOrderItemAsync(Guid id);
    Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
}