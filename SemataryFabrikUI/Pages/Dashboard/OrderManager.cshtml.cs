using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs.OrderDtos;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using System.ComponentModel.DataAnnotations;

namespace SemataryFabrikUI.Pages.Dashboard
{
    public class OrderMangerModel : PageModel
    {
        private readonly IOrderBaseService _orderService;
        private readonly IUserService _userService;
        private readonly IItemService _itemService;
        private readonly IWorkTaskService _workTaskService;

        public OrderMangerModel(
            IOrderBaseService orderService,
            IUserService userService,
            IItemService itemService,
            IWorkTaskService workTaskService)
        {
            _orderService = orderService;
            _userService = userService;
            _itemService = itemService;
            _workTaskService = workTaskService;
        }

        public List<OrderBaseDto> NewOrders { get; set; } = new();
        public List<OrderBaseDto> InProgressOrders { get; set; } = new();
        public List<OrderBaseDto> PendingPaymentOrders { get; set; } = new();
        public List<OrderBaseDto> RentOrders { get; set; } = new();

        [BindProperty]
        public UpdateOrderModel UpdateModel { get; set; } = new();

        [BindProperty]
        public Guid ConfirmOrderId { get; set; }


        [BindProperty] public Guid DenyOrderId { get; set; }
        [BindProperty] public EditItemModel EditItemModel { get; set; }
        [BindProperty] public AddItemToOrderModel AddItemModel { get; set; }

        public async Task<IActionResult> OnPostDenyOrderAsync()
        {
            var order = await _orderService.GetOrderByIdAsync(DenyOrderId);
            order.OrderState = OrderState.Denied;
            await _orderService.UpdateOrderAsync(order);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateItemAsync()
        {
            await _orderService.UpdateOrderItemAsync(EditItemModel.OrderItemId, EditItemModel.Quantity);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveItemAsync(Guid orderItemId)
        {
            await _orderService.RemoveOrderItemAsync(orderItemId);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddItemAsync()
        {
            await _orderService.AddOrderItemAsync(
                AddItemModel.OrderId,
                AddItemModel.ProductId,
                AddItemModel.Quantity);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetOrderDetailsAsync(Guid orderId)
        {
            var order = await _orderService.GetOrderWithItemsAsync(orderId);
            return Partial("_OrderItemsPartial", order);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthorized())
                return RedirectToPage("/Login");

            var managerId = Guid.Parse(HttpContext.Session.GetString("UserId"));


            var allOrders = (await _orderService.GetOrdersByManagerWithItemsAsync(managerId)).ToList();

            // Преобразование в DTO
            NewOrders = allOrders
                .Where(o => o.OrderState == OrderState.Stock)
                .Select(OrderBaseDto.FromEntity)
                .ToList();

            InProgressOrders = allOrders
                .Where(o => o.OrderState == OrderState.ApprovedByManager ||
                           o.OrderState == OrderState.ProccessedByTechLead)
                .Select(OrderBaseDto.FromEntity)
                .ToList();

            PendingPaymentOrders = allOrders
                .Where(o => o.PaymentState == PaymentStatus.PaymentConfirmation)
                .Select(OrderBaseDto.FromEntity)
                .ToList();

            RentOrders = allOrders
                .Where(o => o.OrderType == OrderType.Rent)
                .Select(OrderBaseDto.FromEntity)
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateOrderAsync()
        {
            if (!ModelState.IsValid)
                return Page();


            var order = await _orderService.GetOrderByIdAsync(UpdateModel.OrderId);

            if (order == null)
            {
                ModelState.AddModelError("", "Order not found");
                return Page();
            }

            order.TechOrderLeadId = UpdateModel.TechLeadId;
            order.TotalPrice = UpdateModel.TotalPrice;

            await _orderService.UpdateOrderAsync(order);

            return RedirectToPage();

        }

        public async Task<IActionResult> OnPostConfirmPaymentAsync()
        {

            var order = await _orderService.GetOrderByIdAsync(ConfirmOrderId);

            if (order == null)
            {
                ModelState.AddModelError("", "Order not found");
                return Page();
            }

            order.PaymentState = PaymentStatus.Paid;
            await _orderService.UpdateOrderAsync(order);

            return RedirectToPage();

        }

        private bool IsAuthorized()
        {
            return HttpContext.Session.GetString("UserType") == UserType.OrderManager.ToString();
        }
    }

    public class UpdateOrderModel
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        [Display(Name = "Technical Lead")]
        public Guid TechLeadId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be positive")]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
    }

    public class EditItemModel
    {
        public Guid OrderItemId { get; set; }
        [Range(1, 100)] public int Quantity { get; set; }
    }

    public class AddItemToOrderModel
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        [Range(1, 100)] public int Quantity { get; set; } = 1;
    }
}
