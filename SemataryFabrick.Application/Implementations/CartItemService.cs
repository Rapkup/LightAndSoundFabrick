using Microsoft.Extensions.Logging;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs.CartDtos;
using SemataryFabrick.Application.Entities.Exceptions;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models;
using SemataryFabrick.Domain.Entities.Models.CartModels;

namespace SemataryFabrick.Application.Implementations;
public class CartItemService(IRepositoryManager repositoryManager, ILogger<CartItemService> logger) : ICartItemService
{
    public async Task UpdateCartItemAsync(CartItemToDisplayDto updateDto)
    {
        logger.LogInformation("Executing service method {methodName} with {@data}", nameof(UpdateCartItemAsync),
          new { updateDto.CartItemId, updateDto });

        var cartItem = await repositoryManager.CartItem.GetCartItemAsync(updateDto.CartItemId);

        if (cartItem is null)
        {
            throw new EntityNotFoundException(nameof(CartItem), updateDto.CartItemId);
        }

        cartItem.Quantity = updateDto.Quantity;

        if (cartItem.StartRentDate.HasValue && cartItem.EndRentDate.HasValue)
        {
            cartItem.StartRentDate = updateDto.StartRentDate;
            cartItem.EndRentDate = updateDto.EndRentDate;
        }

        if (updateDto.Discountid != Guid.Empty && updateDto.Discountid != cartItem.DiscountId)
        {
            var discount = await repositoryManager.Discount.GetDiscountAsync(updateDto.Discountid);

            if (discount == null)
            {
                throw new EntityNotFoundException(nameof(Discount), updateDto.Discountid);
            }

            cartItem.DiscountId = updateDto.Discountid;
        }
        else if (updateDto.Discountid == Guid.Empty)
        {
            cartItem.DiscountId = null;
        }

        repositoryManager.CartItem.UpdateCartItem(cartItem);
        await repositoryManager.SaveAsync();

        logger.LogInformation("Cart item {cartItemId} updated successfully", updateDto.CartItemId);
    }

    public async Task UpdateRentalDates(Guid cartItemId, DateOnly start, DateOnly end)
    {
        var item = await repositoryManager.CartItem.GetCartItemAsync(cartItemId);
        item.StartRentDate = start;
        item.EndRentDate = end;
        repositoryManager.CartItem.UpdateCartItem(item);
        await repositoryManager.SaveAsync();
    }

    public async Task UpdateQuantity(Guid cartItemId, int quantity)
{
    var item = await repositoryManager.CartItem.GetCartItemAsync(cartItemId);
    item.Quantity = quantity;
    repositoryManager.CartItem.UpdateCartItem(item);
    await repositoryManager.SaveAsync();
}

    public async Task DeleteCartItemAsync(Guid cartItemId)
    {
        logger.LogInformation("Executing service method {methodName} for cart item {cartItemId}",
            nameof(DeleteCartItemAsync), cartItemId);

        var cartItem = await repositoryManager.CartItem.GetCartItemAsync(cartItemId);

        if (cartItem is null)
        {
            throw new EntityNotFoundException(nameof(CartItem), cartItemId);
        }

        repositoryManager.CartItem.DeleteCartItem(cartItem);
        await repositoryManager.SaveAsync();

        logger.LogInformation("Cart item {cartItemId} deleted successfully", cartItemId);
    }
    public async Task<CartItemToDisplayDto> GetCartItemByIdAsync(Guid cartItemId)
    {
        var cartItem = await repositoryManager.CartItem.GetCartItemWithProductAsync(cartItemId);

        if (cartItem == null)
            throw new EntityNotFoundException(nameof(CartItem), cartItemId);

        return new CartItemToDisplayDto
        {
            CartItemId = cartItem.Id,
            ItemName = cartItem.Product.Name,
            ImagePath = Path.Combine("images/items/", cartItem.Product.ImageName),
            Price = cartItem.Product.Price,
            Quantity = cartItem.Quantity,
            Discount = cartItem.Discount?.DiscountPercent ?? 0,
            StartRentDate = cartItem.StartRentDate,
            EndRentDate = cartItem.EndRentDate
        };
    }
}