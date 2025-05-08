using SemataryFabrick.Domain.Entities.Models.CartModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface ICartItemRepository
{
    void DeleteCartItem(CartItem cartItem);
    void UpdateCartItem(CartItem cartItem);
    Task AddCartItemAsync(CartItem cartItem);
    Task<CartItem?> GetCartItemAsync(Guid id);
    Task<int> GetCartItemsCountByCartIdAsync(Guid cartId);
    Task<IEnumerable<CartItem>> GetAllCartItemsAsync();
    Task<CartItem?> GetCartItemWithProductAsync(Guid cartItemId);
}