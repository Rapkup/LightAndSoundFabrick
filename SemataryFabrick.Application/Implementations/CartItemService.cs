using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Domain.Entities.Models.CartModels;

namespace SemataryFabrick.Application.Implementations;
public class CartItemService : ICartItemService
{
    public Task AddCartItem(Guid id)
    {
        throw new NotImplementedException();
    }

    public void DeleteCartItem(Guid id)
    {
        throw new NotImplementedException();
    }

    public void DeleteCartItemRange(IEnumerable<Guid> Ids)
    {
        throw new NotImplementedException();
    }

    public Task<CartItem> GetCartIdWithProduct(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<CartItem> GetCartItemsByCartIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}