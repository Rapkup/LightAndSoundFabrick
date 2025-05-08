using Microsoft.Extensions.Logging;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs.CartDtos;
using SemataryFabrick.Application.Entities.Exceptions;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.CartModels;
using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Application.Implementations;
public class CartService(IRepositoryManager repositoryManager, ILogger<CartService> logger) : ICartService
{
    public async Task<int> GetCartItemsCountAsync(Guid userId)
    {
        logger.LogInformation("Executing service method {methodName}", nameof(GetCartItemsCountAsync));

        try
        {
            var cart = await repositoryManager.Cart.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                return 0;
            }

            var carItemsCount = await repositoryManager.CartItem.GetCartItemsCountByCartIdAsync(cart.Id);

            logger.LogInformation("Executed succsesful service method {methodName}", nameof(GetCartItemsCountAsync));

            return carItemsCount;
        }
        catch
        {
            return 0;
        }
    }

    public async Task<IEnumerable<CartItemToDisplayDto>?> GetCartItemsToDisplayByUserIdAsync(Guid userId)
    {
        logger.LogInformation("Executing service method {methodName}", nameof(GetCartItemsToDisplayByUserIdAsync));

        var itemCount = await GetCartItemsCountAsync(userId);

        if (itemCount == 0)
        {
            throw new EntityNotFoundException(nameof(CartItem), userId);
        }

        var cartItemList = new List<CartItemToDisplayDto>();

        var cartWithItems =
            await repositoryManager.Cart.GetCartWithRelatedItemsAndProductsByUserIdAsync(userId);


        return cartWithItems.Items.Select(ci => new CartItemToDisplayDto
        {
            ImagePath = Path.Combine("images/items/", ci.Product.ImageName),
            ItemName = ci.Product.Name,
            Price = ci.Product.Price,
            Discount = ci.Discount?.DiscountPercent ?? 0,
            Quantity = ci.Quantity,
            StartRentDate = ci.StartRentDate,
            EndRentDate = ci.EndRentDate,
            CartItemId = ci.Id
        }).ToList();
    }

    public async Task<CartDto> GetCartByUserIdAsync(Guid userId)
    {
        var cart = await repositoryManager.Cart.GetCartByUserIdAsync(userId);

        if (cart is null)
        {
            throw new EntityNotFoundException(nameof(Cart), "By userId: " + userId);
        }

        return CartDto.CartToCartDto(cart);
    }

    public async Task DeleteCartAsync(Guid userId)
    {
        logger.LogInformation("Executing service method {methodName} with {@data}", nameof(DeleteCartAsync),
            new { userId });

        var cart = await repositoryManager.Cart.GetCartByUserIdAsync(userId);

        if (cart is null)
        {
            throw new EntityNotFoundException(nameof(Cart), "By customerId: " + userId);
        }

        repositoryManager.Cart.DeleteCart(cart);

        await repositoryManager.SaveAsync();

        logger.LogInformation("Executed service method {methodName} with {@data}", nameof(DeleteCartAsync),
           new { cart.Id });
    }

    public async Task PlaceAnOrder(CartDto cartDto)
    {
        var cart = await repositoryManager.Cart.GetCartWithRelatedItemsByIdAsync(cartDto.Id);

        if (cart is null)
        {
            throw new EntityNotFoundException(nameof(Cart), "By customerId: " + cartDto.Id);
        }

        var orderType = cart.EventDate.HasValue
        ? OrderType.Individual
        : OrderType.Rent;

        var randomOrderManager = await repositoryManager.User
            .GetRandomByTypeAsync(UserType.OrderManager);
        var randomTechLead = await repositoryManager.User
            .GetRandomByTypeAsync(UserType.TechOrderLead);

        var order = new OrderBase
        {
            Id = Guid.NewGuid(),
            TotalPrice = cart.TotalPrice,
            EventDate = cart.EventDate,
            StartRentDate = cart.Items.First().StartRentDate,
            EndRentDate = cart.Items.First().EndRentDate,
            OrderType = orderType,
            PaymentState = PaymentStatus.Unpaid,
            CustomerId = cart.CustomerId,
            OrderManagerId = randomOrderManager.Id,
            TechOrderLeadId = randomTechLead.Id
        };

        await repositoryManager.OrderBase.AddOrderBaseAsync(order);

        var itemList = new List<OrderItem>();

        foreach (var cartItem in cart.Items)
        {
            itemList.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                Quantity = cartItem.Quantity,
                ProductId = cartItem.ProductId,
                DiscountId = cartItem.DiscountId,
                OrderBaseId = order.Id
            });
        }

        await repositoryManager.OrderItem.AddOrderItemsRangeAsync(itemList);
        await repositoryManager.SaveAsync();

        repositoryManager.Cart.DeleteCart(cart);
        await repositoryManager.SaveAsync();


    }

    public async Task UpdateCartAsync(CartDto cartDto)
    {
        logger.LogInformation("Executing service method {methodName} with {@data}",
            nameof(UpdateCartAsync), cartDto);

        var cart = await repositoryManager.Cart.GetCartAsync(cartDto.Id);

        if (cart == null)
        {
            throw new EntityNotFoundException(nameof(Cart), cartDto.Id);
        }

        cart.TotalPrice = cartDto.TotalPrice;
        cart.EventDate = cartDto.EventDate;

        repositoryManager.Cart.UpdateCart(cart);
        await repositoryManager.SaveAsync();

        logger.LogInformation("Cart {cartId} updated successfully", cartDto.Id);
    }
}