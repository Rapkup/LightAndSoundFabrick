using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IOrderCrewRepository
{
    void DeleteOrderCrew(OrderCrew orderCrew);
    void UpdateOrderCrew(OrderCrew orderCrew);
    Task AddOrderCrewAsync(OrderCrew orderCrew);
    Task<OrderCrew?> GetOrderCrewAsync(Guid id);
    Task<IEnumerable<OrderCrew>> GetAllOrderCrewsAsync();
}