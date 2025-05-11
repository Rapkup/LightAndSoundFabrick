using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrikUI.Pages;

public class AccountModel : PageModel
{
    private readonly ApplicationContext _context;
    private readonly IUserService _userService;
    private readonly IOrderBaseService _orderBaseService;

    public ApplicationUser CurrentUser { get; set; }
    public List<OrderBase> Orders { get; set; }
    public List<OrderBase> DoneOrders { get; set; }
    public List<OrderBase> UnpaidOrders { get; set; }
    public List<OrderBase> ActiveOrders { get; set; }
    public List<OrderBase> PendingConfirmationOrders { get; set; }

    public AccountModel(ApplicationContext context, IUserService userService, IOrderBaseService orderBaseService)
    {
        _context = context;
        _userService = userService;
        _orderBaseService = orderBaseService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        if (HttpContext.Session.GetString("IsLoggedIn") != "true")
        {
            return RedirectToPage("/Authorization/Login");
        }

        var userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
        var userType = HttpContext.Session.GetString("UserType");

        //CurrentUser = await _userService.GetUserAsync(userId);

        CurrentUser = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);


        var orders = await _orderBaseService.GetUserOrdersByStateOrPayStatusAsync(userId, null, null);

       // Orders = orders.Select(o => OrderBaseDto.FromEntity(o)).ToList();

        Orders = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.CustomerId == userId)
            .AsNoTracking()
            .ToListAsync();

        DoneOrders = Orders.Where(o => o.OrderState == OrderState.Done).ToList();
        UnpaidOrders = Orders.Where(o => o.PaymentState == PaymentStatus.Unpaid).ToList();
        ActiveOrders = Orders.Where(o => o.OrderState == OrderState.ApprovedByManager ||
                                        o.OrderState == OrderState.ProccessedByTechLead).ToList();
        PendingConfirmationOrders = Orders.Where(o => o.PaymentState == PaymentStatus.PaymentConfirmation).ToList();

        return Page();
    }

    public IActionResult OnGetOrderDetails([FromQuery] Guid orderId)
    {
        var order = _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product) 
            .FirstOrDefault(o => o.Id == orderId);

        if (order == null) return NotFound();

        return Partial("_OrderDetailsPartial", order);
    }

    public async Task<IActionResult> OnPostUpdatePaymentStatusAsync(Guid orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order != null && order.PaymentState == PaymentStatus.Unpaid)
        {
            order.PaymentState = PaymentStatus.PaymentConfirmation;
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }
}