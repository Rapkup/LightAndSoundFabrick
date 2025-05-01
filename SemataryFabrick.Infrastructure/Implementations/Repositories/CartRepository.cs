using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models.CartModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class CartRepository(ApplicationContext context) : RepositoryBase<Cart>(context), ICartRepository
{
    public Task AddCartAsync(Cart Cart) => CreateAsync(Cart);

    public void DeleteCart(Cart Cart) => Delete(Cart);

    public void UpdateCart(Cart Cart) => Update(Cart);

    public async Task<IEnumerable<Cart>> GetAllCartsAsync()
        => await Find().ToListAsync();

    public async Task<Cart?> GetCartAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}