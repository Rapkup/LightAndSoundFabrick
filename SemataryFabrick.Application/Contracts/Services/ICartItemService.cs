using SemataryFabrick.Domain.Entities.Models.CartModels;

namespace SemataryFabrick.Application.Contracts.Services;
public interface ICartItemService
{
    Task<CartItem> GetCartIdWithProduct(Guid id);
    Task<CartItem> GetCartItemsByCartIdAsync(Guid id);
    Task AddCartItem(Guid id);
    void DeleteCartItem(Guid id);
    void DeleteCartItemRange(IEnumerable<Guid> Ids);
}