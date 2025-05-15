using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Infrastructure.Extensions.InMemoryDb;
using System.ComponentModel.DataAnnotations;

namespace SemataryFabrikUI.Pages.Dashboard;

[BindProperties]
public class OrderMangerModel : PageModel
{
    public readonly InMemoryDatabase _db;

    public OrderMangerModel(InMemoryDatabase db)
    {
        _db = db;
    }

    // Properties for order operations
    [BindProperty] public Guid SelectedTechLeadId { get; set; }
    [BindProperty] public decimal NewTotalPrice { get; set; }
    [BindProperty] public Guid SelectedOrderId { get; set; }

    // Properties for item management
    [BindProperty] public Guid SelectedCategoryId { get; set; }
    [BindProperty] public Guid SelectedSubCategoryId { get; set; }
    [BindProperty] public Guid SelectedItemId { get; set; }
    [BindProperty][Range(1, 100)] public int Quantity { get; set; } = 1;

    // Data collections
    public List<SelectListItem> TechLeads { get; set; } = new();
    public List<OrderBase> NewOrders { get; set; } = new();
    public List<OrderBase> InProgressOrders { get; set; } = new();
    public List<OrderBase> PaymentConfirmationOrders { get; set; } = new();
    public List<OrderBase> RentOrders { get; set; } = new();

    public void OnGet()
    {
        LoadOrders();
        InitializeTechLeads();
    }

    private void LoadOrders()
    {
        var currentUserId = GetCurrentOrderManagerId();

        NewOrders = _db.Orders
            .Where(o => o.OrderState == OrderState.Stock
            && o.OrderManagerId == currentUserId)
            .Select(o => MapOrderWithRelatedData(o))
            .ToList();

        InProgressOrders = _db.Orders
            .Where(o => o.OrderState is OrderState.ApprovedByManager
                or OrderState.ProccessedByTechLead
                 && o.OrderManagerId == currentUserId)
              .Select(o => MapOrderWithRelatedData(o))
            .ToList();

        PaymentConfirmationOrders = _db.Orders
            .Where(o => o.PaymentState == PaymentStatus.PaymentConfirmation
             && o.OrderManagerId == currentUserId)
              .Select(o => MapOrderWithRelatedData(o))
            .ToList();

        RentOrders = _db.Orders
            .Where(o => o.OrderType == OrderType.Rent
             && o.OrderManagerId == currentUserId)
              .Select(o => MapOrderWithRelatedData(o))
            .ToList();
    }

    private OrderBase MapOrderWithRelatedData(OrderBase order)
    {
        // Загрузка базовых данных пользователей
        order.Customer = _db.Users
            .FirstOrDefault(u => u.Id == order.CustomerId);

        order.OrderManager = _db.Users
            .OfType<OrderManager>()
            .FirstOrDefault(om => om.Id == order.OrderManagerId);

        order.TechOrderLead = _db.Users
            .OfType<TechOrderLead>()
            .FirstOrDefault(t => t.Id == order.TechOrderLeadId);

        // Загрузка элементов заказа с продуктами и скидками
        order.OrderItems = _db.OrderItems
            .Where(oi => oi.OrderBaseId == order.Id)
            .Select(oi => new OrderItem
            {
                Id = oi.Id,
                OrderBaseId = oi.OrderBaseId,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                DiscountId = oi.DiscountId,
                Product = _db.Items
                    .Find(i => i.Id == oi.ProductId),
                Discount = _db.Discounts
                    .FirstOrDefault(d => d.Id == oi.DiscountId)
            }).ToList();

        // Загрузка команд с полной иерархией
        order.OrderCrews = _db.OrderCrews
            .Where(oc => oc.OrderBaseId == order.Id)
            .Select(oc => new OrderCrew
            {
                Id = oc.Id,
                OrderBaseId = oc.OrderBaseId,
                TechLeadId = oc.TechLeadId,
                WorkDate = oc.WorkDate,
                TechOrderLead = _db.Users
                    .OfType<TechOrderLead>()
                    .FirstOrDefault(t => t.Id == oc.TechLeadId),
                WorkTaskAssignments = _db.WorkTaskAssignments
                    .Where(wta => wta.OrderCrewId == oc.Id)
                    .Select(wta => new WorkTaskAssignment
                    {
                        Id = wta.Id,
                        OrderCrewId = wta.OrderCrewId,
                        WorkTaskId = wta.WorkTaskId,
                        IsCompleted = wta.IsCompleted,
                        WorkTask = _db.WorkTasks
                            .FirstOrDefault(wt => wt.Id == wta.WorkTaskId)
                    }).ToList(),
                Workers = _db.WorkerCrewRelations
                    .Where(r => r.CrewId == oc.Id)
                    .Join(_db.Users.OfType<Worker>(),
                        relation => relation.WorkerId,
                        worker => worker.Id,
                        (relation, worker) => worker)
                    .ToList()
            }).ToList();


        return order;
    }
    private Guid GetCurrentOrderManagerId()
    {
        var username = HttpContext.Session.GetString("Username");

        return _db.Users.OfType<OrderManager>().First(u => u.UserName == username).Id;
    }

    private void InitializeTechLeads()
    {
        TechLeads = _db.Users
            .OfType<TechOrderLead>()
            .Select(t => new SelectListItem(
                $"{t.FirstName} {t.LastName}",
                t.Id.ToString()))
            .ToList();
    }

    #region Order Actions
    public IActionResult OnPostApproveOrder(Guid id)
    {
        var order = _db.Orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            order.OrderState = OrderState.ApprovedByManager;
        }
        return RedirectToPage();
    }

    public IActionResult OnPostRejectOrder(Guid id)
    {
        var order = _db.Orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            order.OrderState = OrderState.Denied;
        }
        return RedirectToPage();
    }

    public IActionResult OnPostProcessOrder(Guid id)
    {
        var order = _db.Orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            order.OrderState = OrderState.Done;
            order.PaymentState = PaymentStatus.PaymentConfirmation;
        }
        return RedirectToPage();
    }

    public IActionResult OnPostConfirmPayment(Guid id)
    {
        var order = _db.Orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            order.PaymentState = PaymentStatus.Paid;
        }
        return RedirectToPage();
    }
    #endregion

    #region Tech Lead & Price Updates
    public IActionResult OnPostUpdateTechLead()
    {
        var order = _db.Orders.FirstOrDefault(o => o.Id == SelectedOrderId);
        if (order != null)
        {
            order.TechOrderLeadId = SelectedTechLeadId;
          
        }
        return RedirectToPage();
    }

    public IActionResult OnPostUpdatePrice()
    {
        var order = _db.Orders.FirstOrDefault(o => o.Id == SelectedOrderId);
        if (order != null)
        {
            order.TotalPrice = NewTotalPrice;
        }
        return RedirectToPage();
    }
    #endregion

    #region Item Management
    public JsonResult OnGetSubCategories(Guid categoryId)
    {
        var subCategories = _db.SubCategories
            .Where(sc => sc.ParentCategoryId == categoryId)
            .Select(sc => new { sc.Id, sc.Name })
            .ToList();
        return new JsonResult(subCategories);
    }

    public JsonResult OnGetItems(Guid subCategoryId)
    {
        var items = _db.Items
            .Where(i => i.SubCategoryId == subCategoryId)
            .Select(i => new { i.Id, i.Name, Price = i.Price.ToString("C") })
            .ToList();
        return new JsonResult(items);
    }

    public JsonResult OnGetOrderItems(Guid orderId)
    {
        var order = _db.Orders
            .FirstOrDefault(o => o.Id == orderId);

        if (order == null) return new JsonResult(new { });

        var items = order.OrderItems.Select(oi => new
        {
            id = oi.Id,
            productName = oi.Product?.Name ?? "N/A",
            price = oi.Product?.Price.ToString("C") ?? "N/A",
            quantity = oi.Quantity,
            total = (oi.Quantity * (oi.Product?.Price ?? 0)).ToString("C")
        });

        return new JsonResult(items);
    }

    public IActionResult OnPostAddItem()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var order = _db.Orders.FirstOrDefault(o => o.Id == SelectedOrderId);
        var item = _db.Items.FirstOrDefault(i => i.Id == SelectedItemId);

        if (order == null || item == null)
        {
            return NotFound();
        }

        var existingItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == SelectedItemId);

        if (existingItem != null)
        {
            existingItem.Quantity += Quantity;
        }
        else
        {
            var newOrderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = SelectedItemId,
                Quantity = Quantity,
                OrderBaseId = order.Id 
            };

            order.OrderItems.Add(newOrderItem);
            _db.OrderItems.Add(newOrderItem); 
        }

        order.TotalPrice = CalculateOrderTotal(order);
        return RedirectToPage();
    }

    public IActionResult OnPostRemoveItem(Guid itemId, Guid orderId)
    {
        var order = _db.Orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null) return NotFound();

        var item = order.OrderItems.FirstOrDefault(oi => oi.Id == itemId);
        if (item != null)
        {
            order.OrderItems.Remove(item);
            order.TotalPrice = CalculateOrderTotal(order);
        }

        return RedirectToPage();
    }


    private decimal CalculateOrderTotal(OrderBase order)
    {
        return order.OrderItems.Sum(oi =>
            _db.Items.First(i => i.Id == oi.ProductId).Price * oi.Quantity);
    }
    #endregion
}