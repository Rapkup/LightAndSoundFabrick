using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs.UserDtos;
using SemataryFabrick.Application.Entities.Exceptions;
using SemataryFabrick.Domain.Entities.Enums;

namespace SemataryFabrikUI.Pages.Authorization
{
    public class RegistrationModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public UserDto UserDto { get; set; } = new();

        public RegistrationModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UserDto.UserType == UserType.IndividualCustomer)
            {
                if (string.IsNullOrEmpty(UserDto.PassportIdNumber))
                    ModelState.AddModelError("UserDto.PassportIdNumber", "Passport ID is required");
            }
            else if (UserDto.UserType == UserType.LegalCustomer)
            {
                if (string.IsNullOrEmpty(UserDto.CompanyName))
                    ModelState.AddModelError("UserDto.CompanyName", "Company name is required");
            }

            if (!ModelState.IsValid)
                return Page();

            if (UserDto.isGovernment != true)
            {
                UserDto.GovernmentCode = null;
            }

            try
            {
                var user = await _userService.Register(UserDto);

                if (user != null)
                {
                    HttpContext.Session.SetString("IsLoggedIn", "true");
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("UserType", user.UserType.ToString());
                    HttpContext.Session.SetString("Username", user.UserName);

                    return RedirectToPage("/Index");
                }
                ModelState.AddModelError("", "Ошибка регистрации");
            }
            catch (EntityDuplicationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }
            return Page();
        }
    }
}