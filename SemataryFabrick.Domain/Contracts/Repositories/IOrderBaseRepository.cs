using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IOrderBaseRepository
{
    void DeleteOrderBase(OrderBase orderBase);
    void UpdateOrderBase(OrderBase orderBase);
    Task AddOrderBaseAsync(OrderBase orderBase);
    Task<OrderBase?> GetOrderBaseAsync(Guid id);
    Task<IEnumerable<OrderBase>> GetAllOrderBasesAsync();
}