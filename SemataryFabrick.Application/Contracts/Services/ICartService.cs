using SemataryFabrick.Application.Entities.DTOs.CartDtos;

namespace SemataryFabrick.Application.Contracts.Services;
public interface ICartService
{
    Task<int> GetCartItemsCountAsync(Guid userId);
    Task<IEnumerable<CartItemToDisplayDto>?> GetCartItemsToDisplayByUserIdAsync(Guid userId);
    Task<CartDto> GetCartByUserIdAsync(Guid userId);
    Task<CartDto> GetOrCreateCartAsync(Guid userId);
    Task<CartDto> GetOrCreateCartAsync(Guid userId, DateOnly eventDate);
    Task PlaceAnOrder(CartDto cartDto);
    Task DeleteCartAsync(Guid userId);
    Task UpdateCartAsync(CartDto cartDto);
    Task AddCartItemsAsync(Guid cartId, IEnumerable<CartItemDto> cartItems);
}