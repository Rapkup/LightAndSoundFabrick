using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class OrderBaseRepository(ApplicationContext context) : RepositoryBase<OrderBase>(context), IOrderBaseRepository
{
    public Task AddOrderBaseAsync(OrderBase orderBase) => CreateAsync(orderBase);

    public void DeleteOrderBase(OrderBase orderBase) => Delete(orderBase);

    public void UpdateOrderBase(OrderBase orderBase) => Update(orderBase);

    public async Task<IEnumerable<OrderBase>> GetAllOrderBasesAsync()
        => await Find().ToListAsync();

    public async Task<OrderBase?> GetOrderBaseAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}