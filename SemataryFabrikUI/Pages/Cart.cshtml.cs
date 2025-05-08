using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs.CartDtos;
using SemataryFabrick.Application.Entities.Exceptions;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models.CartModels;
using System.ComponentModel.DataAnnotations;

namespace SemataryFabrikUI.Pages
{
    public class CartModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly ICartItemService _cartItemService;
        private readonly ILogger<CartModel> _logger;

        public IEnumerable<CartItemToDisplayDto> CartItems { get; set; } = new List<CartItemToDisplayDto>();
        public decimal TotalPrice { get; set; }
        public DateOnly? EventDate { get; set; }
        public bool HasRentalItems { get; set; }

        public CartModel(
            ICartService cartService,
            ICartItemService cartItemService,
            ILogger<CartModel> logger)
        {
            _cartService = cartService;
            _cartItemService = cartItemService;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var userId = GetUserId();
            try
            {
                await LoadCartData(userId);
                var cartDto = await _cartService.GetCartByUserIdAsync(userId);
                EventDate = cartDto?.EventDate;
            }
            catch (EntityNotFoundException)
            {
                CartItems = new List<CartItemToDisplayDto>();
                TotalPrice = 0;
                EventDate = null;
            }
            HasRentalItems = CartItems.Any(i => i.StartRentDate.HasValue);
        }

        private async Task LoadCartData(Guid userId)
        {
            try
            {
                CartItems = await _cartService.GetCartItemsToDisplayByUserIdAsync(userId);
                TotalPrice = CalculateTotalPrice();
            }
            catch (EntityNotFoundException)
            {
                CartItems = new List<CartItemToDisplayDto>();
                TotalPrice = 0;
            }
        }

        private decimal CalculateTotalPrice()
        {
            return CartItems.Sum(item =>
                item.Price * item.Quantity * (1 - item.Discount / 100m));
        }

        public async Task<IActionResult> OnPostUpdateRentalDatesAsync(
            Guid cartItemId,
            DateOnly startRentDate,
            DateOnly endRentDate)
        {

            try
            {
                var currentItem = await _cartItemService.GetCartItemByIdAsync(cartItemId);
                var updateDto = new CartItemToDisplayDto
                {
                    CartItemId = currentItem.CartItemId,
                    Quantity = currentItem.Quantity,
                    StartRentDate = startRentDate,
                    EndRentDate = endRentDate,
                    Discountid = currentItem.Discountid,
                };

                await _cartItemService.UpdateCartItemAsync(updateDto);
                await ReloadCartData();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, "Cart item not found: {CartItemId}", cartItemId);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveItemAsync(Guid cartItemId)
        {
            try
            {
                await _cartItemService.DeleteCartItemAsync(cartItemId);
                await UpdateCartBadge();
                await ReloadCartData();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, "Error removing cart item: {CartItemId}", cartItemId);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostClearCartAsync()
        {
            var userId = GetUserId();
            try
            {
                await _cartService.DeleteCartAsync(userId);
                await ReloadCartData();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, "Cart not found for user: {UserId}", userId);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostPlaceOrderAsync()
        {
            var userId = GetUserId();
            try
            {
                var cart = await _cartService.GetCartByUserIdAsync(userId);
                if (cart != null) await _cartService.PlaceAnOrder(cart);
                await ReloadCartData();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex, "Error placing order for user: {UserId}", userId);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateEventDateAsync(string eventDate)
        {
            var userId = GetUserId();
            try
            {
                var cartDto = await _cartService.GetCartByUserIdAsync(userId);
                if (cartDto != null)
                {
                    cartDto.EventDate = ParseDateFromString(eventDate);
                    await _cartService.UpdateCartAsync(cartDto);
                    await ReloadCartData();
                }
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, "Cart not found for user: {UserId}", userId);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateItemAsync(Guid cartItemId, int quantity)
        {
            try
            {
                var currentItem = await _cartItemService.GetCartItemByIdAsync(cartItemId);
                var updateDto = new CartItemToDisplayDto
                {
                    CartItemId = currentItem.CartItemId,
                    Quantity = quantity,
                    StartRentDate = currentItem.StartRentDate,
                    EndRentDate = currentItem.EndRentDate,
                    Discountid = currentItem.Discountid,
                };

                await _cartItemService.UpdateCartItemAsync(updateDto);
                await ReloadCartData();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex, "Error updating item quantity: {CartItemId}", cartItemId);
            }
            return RedirectToPage();
        }

        private Guid GetUserId()
        {
            return Guid.Parse(HttpContext.Session.GetString("UserId"));
        }

        private async Task ReloadCartData()
        {
            await LoadCartData(GetUserId());
        }

        private static DateOnly? ParseDateFromString(string dateString)
        {
            return string.IsNullOrEmpty(dateString) ?
                null :
                DateOnly.Parse(dateString);
        }

        private async Task UpdateCartBadge()
        {
            var count = await _cartService.GetCartItemsCountAsync(GetUserId());
            HttpContext.Session.SetInt32("CartItemsCount", count);
        }
    }
}