using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models.CartModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
partial class CartItemRepository(ApplicationContext context) : RepositoryBase<CartItem>(context), ICartItemRepository
{
    public Task AddCartItemAsync(CartItem cartItem) => CreateAsync(cartItem);

    public void DeleteCartItem(CartItem cartItem) => Delete(cartItem);

    public void UpdateCartItem(CartItem cartItem) => Update(cartItem);

    public async Task<IEnumerable<CartItem>> GetAllCartItemsAsync()
        => await Find().ToListAsync();

    public async Task<CartItem?> GetCartItemAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}