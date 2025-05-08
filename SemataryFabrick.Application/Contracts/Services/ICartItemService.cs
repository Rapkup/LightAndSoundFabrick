using SemataryFabrick.Application.Entities.DTOs.CartDtos;
using SemataryFabrick.Domain.Entities.Models.CartModels;

namespace SemataryFabrick.Application.Contracts.Services;
public interface ICartItemService
{
    Task UpdateCartItemAsync(CartItemToDisplayDto updateDto);
    Task DeleteCartItemAsync(Guid cartItemId);
    Task<CartItemToDisplayDto> GetCartItemByIdAsync(Guid cartItemId);
    Task AddOrUpdateCartItemAsync(Guid cartId, Guid productId, int quantity);
}