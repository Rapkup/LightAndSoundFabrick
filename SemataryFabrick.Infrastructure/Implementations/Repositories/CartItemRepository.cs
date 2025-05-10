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

    public async Task<int> GetCartItemsCountByCartIdAsync(Guid cartId)
    => await Find(ci => ci.CartId == cartId).CountAsync();

    public async Task<IEnumerable<CartItem>> GetAllCartItemsAsync()
        => await Find().ToListAsync();

    public async Task<CartItem?> GetCartItemAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();

    public async Task<CartItem?> GetCartItemWithProductAsync(Guid cartItemId)
    {
        return await Find(ci => ci.Id == cartItemId)
                .Include(ci => ci.Product)
                .Include(ci => ci.Discount)
                .FirstOrDefaultAsync();
    }

    public async Task<CartItem?> GetCartItemByCartAndProductAsync(Guid cartId, Guid productId)
    {
        return await context.CartItems
            .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
    }
    public async Task DeleteCartItemsByCartId(Guid cartId)
    {
        await context.CartItems
            .Where(ci => ci.CartId == cartId)
            .ExecuteDeleteAsync();
    }
    public async Task<IEnumerable<CartItem>?> GetCartItemsByCartId(Guid cartId)
    {
        var items = await context.CartItems
            .Where(ci => ci.CartId == cartId)
            .ToListAsync();

        return items.Any() ? items : null;
    }
}