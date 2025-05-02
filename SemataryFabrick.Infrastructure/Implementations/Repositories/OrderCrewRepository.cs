using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class OrderCrewRepository(ApplicationContext context) : RepositoryBase<OrderCrew>(context), IOrderCrewRepository
{
    public Task AddOrderCrewAsync(OrderCrew orderCrew) => CreateAsync(orderCrew);

    public void DeleteOrderCrew(OrderCrew orderCrew) => Delete(orderCrew);

    public void UpdateOrderCrew(OrderCrew orderCrew) => Update(orderCrew);

    public async Task<IEnumerable<OrderCrew>> GetAllOrderCrewsAsync()
        => await Find().ToListAsync();

    public async Task<OrderCrew?> GetOrderCrewAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}