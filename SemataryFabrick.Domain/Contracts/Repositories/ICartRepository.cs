using SemataryFabrick.Domain.Entities.Models.CartModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface ICartRepository
{
    void DeleteCart(Cart cart);
    void UpdateCart(Cart cart);
    Task AddCartAsync(Cart cart);
    Task<Cart?> GetCartAsync(Guid id);
    Task<IEnumerable<Cart>> GetAllCartsAsync();
}