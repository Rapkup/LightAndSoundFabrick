using SemataryFabrick.Domain.Entities.Models.UserModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IOrderManagerRepository
{
    void DeleteOrderManager(OrderManager orderManager);
    void UpdateOrderManager(OrderManager orderManager);
    Task AddOrderManagerAsync(OrderManager orderManager);
    Task<OrderManager?> GetOrderManagerAsync(Guid id);
    Task<IEnumerable<OrderManager>> GetAllOrderManagersAsync();
}