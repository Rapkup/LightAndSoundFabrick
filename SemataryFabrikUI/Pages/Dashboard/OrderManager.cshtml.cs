using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs;
using SemataryFabrick.Application.Entities.DTOs.OrderDtos;
using SemataryFabrick.Application.Entities.DTOs.ProductItemDtos;
using SemataryFabrick.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace SemataryFabrikUI.Pages.Dashboard
{
    public class OrderMangerModel : PageModel
    {
        private readonly IOrderBaseService _orderService;
        public readonly IUserService _userService;
        private readonly IItemService _itemService;
        public readonly IWorkTaskService _workTaskService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly ISubCategoryService _subCategoryService;

        public OrderMangerModel(
            IOrderBaseService orderService,
            IUserService userService,
            IItemService itemService,
            IWorkTaskService workTaskService,
            IProductCategoryService productCategoryService,
            ISubCategoryService subCategoryService)
        {
            _orderService = orderService;
            _userService = userService;
            _itemService = itemService;
            _workTaskService = workTaskService;
            _productCategoryService = productCategoryService;
            _subCategoryService = subCategoryService;
        }

        // Свойства для отображения заказов
        public List<OrderBaseDto> NewOrders { get; set; } = new();
        public List<OrderBaseDto> InProgressOrders { get; set; } = new();
        public List<OrderBaseDto> PendingPaymentOrders { get; set; } = new();
        public List<OrderBaseDto> RentOrders { get; set; } = new();

        // Модели привязки
        [BindProperty]
        public UpdateOrderModel UpdateModel { get; set; } = new();

        [BindProperty]
        public Guid ConfirmOrderId { get; set; }

        [BindProperty]
        public Guid DenyOrderId { get; set; }

        [BindProperty]
        public EditItemModel EditItemModel { get; set; } = new();

        [BindProperty]
        public AddItemToOrderModel AddItemModel { get; set; } = new();

        // Данные для каскадных выпадающих списков
        public List<ProductCategoryDto> Categories { get; set; } = new();
        public Dictionary<Guid, List<SubCategoryDto>> Subcategories { get; set; } = new();
        public Dictionary<Guid, List<ItemDto>> Products { get; set; } = new();

        // Методы обработки запросов
        public async Task<IActionResult> OnPostDenyOrderAsync()
        {
            var order = await _orderService.GetOrderByIdAsync(DenyOrderId);
            order.OrderState = OrderState.Denied;
            await _orderService.UpdateOrderAsync(order);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateItemAsync()
        {
            if (!ModelState.IsValid)
                return Page();

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
            if (!ModelState.IsValid)
                return Page();

            await _orderService.AddOrderItemAsync(
                AddItemModel.OrderId,
                AddItemModel.ProductId,
                AddItemModel.Quantity);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthorized())
                return RedirectToPage("/Login");

            var managerId = Guid.Parse(HttpContext.Session.GetString("UserId"));
            var allOrders = (await _orderService.GetOrdersByManagerWithItemsAsync(managerId)).ToList();

            // Разделение заказов по состояниям
            NewOrders = allOrders
                .Where(o => o.OrderState == OrderState.Stock)
                .Select(OrderBaseDto.FromEntity)
                .ToList();

            InProgressOrders = allOrders
                .Where(o => o.OrderState is OrderState.ApprovedByManager or OrderState.ProccessedByTechLead)
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

            // Загрузка данных для каскадных списков
            await LoadCategoryData();

            return Page();
        }

        private async Task LoadCategoryData()
        {
            Categories = (await _productCategoryService.GetAllProductCategoriesAsync()).ToList();

            foreach (var category in Categories)
            {
                var subcategories = await _subCategoryService.GetSubCategoriesByParentIdAsync(category.Id);
                Subcategories[category.Id] = subcategories.ToList();

                foreach (var subcategory in subcategories)
                {
                    var products = await _itemService.GetItemsBySubCategoriesAsync([subcategory.Id]);
                    Products[subcategory.Id] = products.ToList();
                }
            }
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

        // Вспомогательные методы для отображения статусов аренды
        public string GetRentStatus(OrderBaseDto order)
        {
            if (order.PaymentState == PaymentStatus.PaymentConfirmation)
                return "Pending Confirmation";
            
            if (DateTime.Now > order.EndRentDate?.ToDateTime(TimeOnly.MinValue))
                return "Overdue";
            
            return order.PaymentState.ToString();
        }

        public string GetRentStatusBadge(OrderBaseDto order)
        {
            return GetRentStatus(order) switch
            {
                "Overdue" => "bg-danger",
                "Pending Confirmation" => "bg-warning",
                "Paid" => "bg-success",
                _ => "bg-secondary"
            };
        }
    }

    // Модели для привязки данных
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

        [Range(1, 100)]
        public int Quantity { get; set; }
    }

    public class AddItemToOrderModel
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; } = 1;
    }
}