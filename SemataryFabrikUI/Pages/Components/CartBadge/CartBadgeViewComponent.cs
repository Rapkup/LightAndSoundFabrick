using Microsoft.AspNetCore.Mvc;
using SemataryFabrick.Application.Contracts.Services;

namespace SemataryFabrikUI.Pages.Components.CartBadge;

public class CartBadgeViewComponent : ViewComponent
{
    private readonly ICartService _cartService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartBadgeViewComponent(
        ICartService cartService,
        IHttpContextAccessor httpContextAccessor)
    {
        _cartService = cartService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userIdString = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
        {
            return View(0);
        }

        var count = await _cartService.GetCartItemsCountAsync(userId);
        return View(count);
    }
}
