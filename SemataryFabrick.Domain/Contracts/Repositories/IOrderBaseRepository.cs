using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IOrderBaseRepository
{
    void DeleteOrderBase(OrderBase orderBase);
    void UpdateOrderBase(OrderBase orderBase);
    Task AddOrderBaseAsync(OrderBase orderBase);
    Task<OrderBase?> GetOrderBaseAsync(Guid id);
    Task<IEnumerable<OrderBase>> GetAllOrderBasesAsync();
    Task<IEnumerable<OrderBase>?> GetOrdersBaseWithRelatedItemsByUserId(Guid userId);
    Task<OrderBase?> GetOrderBaseWithItemsAsync(Guid id);
    Task<IEnumerable<OrderBase>> GetOrdersByManagerWithItemsAsync(Guid managerId);
    Task<OrderBase?> GetOrderWithCrewsAndTasksAsync(Guid id);
    Task UpdateOrderWithItemsAsync(OrderBase order);
    Task AddOrderItemAsync(Guid orderId, OrderItem item);

    Task RemoveOrderItemAsync(Guid orderId, Guid itemId);
    Task UpdateOrderStatusAsync(Guid orderId, OrderState state);
    Task<IEnumerable<OrderBase>> GetOrdersByStatusAsync(OrderState state);
    Task<int> GetCompletedTasksCountAsync(Guid orderId);
    Task<int> GetTotalTasksCountAsync(Guid orderId);
}