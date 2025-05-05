using Microsoft.AspNetCore.Mvc.RazorPages;
using SemataryFabrick.Domain.Contracts.Services;

namespace SemataryFabrikUI.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICartItemService cartItemService;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
