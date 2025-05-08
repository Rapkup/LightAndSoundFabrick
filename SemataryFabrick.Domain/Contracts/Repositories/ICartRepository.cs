using SemataryFabrick.Domain.Entities.Models.CartModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface ICartRepository
{
    void DeleteCart(Cart cart);
    void UpdateCart(Cart cart);
    Task AddCartAsync(Cart cart);
    Task<Cart?> GetCartAsync(Guid id);
    Task<Cart?> GetCartByUserIdAsync(Guid userId);
    Task<Cart?> GetCartWithRelatedItemsByIdAsync(Guid cartId);
    Task<Cart?> GetCartWithRelatedItemsAndProductsByUserIdAsync(Guid userId);
    Task<IEnumerable<Cart>> GetAllCartsAsync();
}