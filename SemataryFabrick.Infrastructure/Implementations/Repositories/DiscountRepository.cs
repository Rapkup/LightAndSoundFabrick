using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class DiscountRepository(ApplicationContext context) : RepositoryBase<Discount>(context), IDiscountRepository
{
    public Task AddDiscountAsync(Discount Discount) => CreateAsync(Discount);

    public void DeleteDiscount(Discount Discount) => Delete(Discount);

    public void UpdateDiscount(Discount Discount) => Update(Discount);

    public async Task<IEnumerable<Discount>> GetAllDiscountsAsync()
        => await Find().ToListAsync();

    public async Task<Discount?> GetDiscountAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}