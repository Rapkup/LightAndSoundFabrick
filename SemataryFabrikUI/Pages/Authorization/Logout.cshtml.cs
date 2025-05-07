using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SemataryFabrikUI.Pages.Authorization
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnPost()
        {
            HttpContext.Session.Remove("IsLoggedIn");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserType");
            return RedirectToPage("/Index");
        }
    }
}
