using SemataryFabrick.Application.Entities.DTOs.CartDtos;

namespace SemataryFabrick.Application.Contracts.Services;
public interface ICartItemService
{
    Task UpdateCartItemAsync(CartItemToDisplayDto updateDto);
    Task DeleteCartItemAsync(Guid cartItemId);
    Task<CartItemToDisplayDto> GetCartItemByIdAsync(Guid cartItemId);
    Task AddOrUpdateCartItemAsync(Guid cartId, Guid productId, int quantity);
}