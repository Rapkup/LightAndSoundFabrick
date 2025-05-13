using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs.UserDtos;
using SemataryFabrick.Application.Entities.Exceptions;
using SemataryFabrick.Domain.Entities.Enums;

namespace SemataryFabrikUI.Pages.Authorization
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ICartService _cartService;

        [BindProperty]
        public UserLoginDto UserLogin { get; set; }

        public LoginModel(IUserService userService, ICartService cartService)
        {
            _userService = userService;
            _cartService = cartService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var user = await _userService.Login(UserLogin.username, UserLogin.password);

                if (user != null)
                {
                    HttpContext.Session.SetString("IsLoggedIn", "true");
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("UserType", user.UserType.ToString());
                    HttpContext.Session.SetString("Username", user.UserName);
                    await UpdateCartBadge();

                    switch (user.UserType)
                    {
                        case UserType.OrderManager:
                            return RedirectToPage("/Dashboard/OrderManager");

                        case UserType.TechOrderLead:
                            return RedirectToPage("/Dashboard/TechOrderLead");

                        case UserType.Worker:
                            return RedirectToPage("/Dashboard/Worker");

                        case UserType.Director:
                            return RedirectToPage("/Dashboard/Director");

                        default:
                            return RedirectToPage("/Index");
                    }
                }

                ModelState.AddModelError("", "Ќеверный логин или пароль");
            }
            catch (EntityNotFoundException)
            {
                ModelState.AddModelError("", "ѕользователь с таким именем или паролем не найден");
            }

            return Page();
        }
        private async Task UpdateCartBadge()
        {
            var userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
            var count = await _cartService.GetCartItemsCountAsync(userId);
            HttpContext.Session.SetInt32("CartItemsCount", count);
        }
    }
}