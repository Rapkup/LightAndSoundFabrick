using Microsoft.Extensions.Logging;
using SemataryFabrick.Application.Entities.DTOs.CartDtos;
using SemataryFabrick.Application.Entities.Exceptions;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.CartModels;
using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Application.Contracts.Services;
public interface ICartService
{
    Task<int> GetCartItemsCountAsync(Guid userId);

    Task<IEnumerable<CartItemToDisplayDto>?> GetCartItemsToDisplayByUserIdAsync(Guid userId);
    Task DeleteCartAsync(Guid userId);
    Task<CartDto> GetCartByUserIdAsync(Guid userId);
    Task PlaceAnOrder(CartDto cartDto);
    Task UpdateCartAsync(CartDto cartDto);
}