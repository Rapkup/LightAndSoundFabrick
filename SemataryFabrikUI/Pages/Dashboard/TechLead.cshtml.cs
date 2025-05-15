using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Infrastructure.Extensions.InMemoryDb;

namespace SemataryFabrikUI.Pages;

public class TechLeadModel : PageModel
{
    private readonly InMemoryDatabase _db;

    [BindProperty] public Guid SelectedOrderId { get; set; }
    [BindProperty] public Guid SelectedCrewId { get; set; }

    [BindProperty] public DateOnly? NewCrewWorkDate { get; set; }

    [BindProperty] public List<Guid> SelectedWorkers { get; set; } = new();

    public List<OrderBase> ActiveOrders { get; set; } = new();
    public List<OrderBase> ProcessedOrders { get; set; } = new();
    public List<OrderBase> HistoryOrders { get; set; } = new();

    public TechLeadModel(InMemoryDatabase db) => _db = db;

    public List<Worker> GetAllWorkers()
    => _db.Users.OfType<Worker>().ToList();
    public void OnGet()
    {
        var currentUserId = GetCurrentTechLeadId();

        ActiveOrders = _db.Orders
            .Where(o => o.TechOrderLeadId == currentUserId
                && o.OrderState == OrderState.ApprovedByManager)
            .ToList();

        ProcessedOrders = _db.Orders
            .Where(o => o.TechOrderLeadId == currentUserId
                && o.OrderState == OrderState.ProccessedByTechLead)
            .ToList();

        HistoryOrders = _db.Orders
            .Where(o => o.TechOrderLeadId == currentUserId
                && o.OrderState == OrderState.Done)
            .ToList();
    }
    public IActionResult OnGetOrderDetails(Guid orderId)
    {
        var order = _db.Orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null) return NotFound();
        return Partial("_OrderDetailsModal", order); // Передаем конкретный заказ
    }

    public string GetOrderDescription(OrderBase order)
    {
        var details = new List<string>
        {
            $"Тип: {GetOrderTypeName(order.OrderType)}",
            $"Адрес: {order.EventAddress ?? "Не указан"}",
            $"Сумма: {order.TotalPrice:C}"
        };

        if (order.OrderItems?.Count > 0)
            details.Add($"Оборудование: {order.OrderItems.Count} позиций");

        return string.Join("<br>", details);
    }

    public int CalculateProgress(OrderBase order)
    {
        if (order?.OrderCrews == null) return 0;

        var totalTasks = order.OrderCrews
            .Where(c => c.WorkTaskAssignments != null)
            .SelectMany(c => c.WorkTaskAssignments)
            .Count();

        var completedTasks = order.OrderCrews
            .Where(c => c.WorkTaskAssignments != null)
            .SelectMany(c => c.WorkTaskAssignments)
            .Count(t => t.IsCompleted);

        return totalTasks > 0
            ? (int)Math.Round((double)completedTasks / totalTasks * 100)
            : 0;
    }

    #region Order Actions
    public IActionResult OnPostCompleteOrder(Guid orderId)
    {
        var order = _db.Orders.First(o => o.Id == orderId);
        order.OrderState = OrderState.ProccessedByTechLead;
        return RedirectToPage();
    }

    public IActionResult OnPostReturnOrder(Guid orderId)
    {
        var order = _db.Orders.First(o => o.Id == orderId);
        order.OrderState = OrderState.ApprovedByManager;
        return RedirectToPage();
    }
    #endregion

    #region Crew Management
    public IActionResult OnPostAddCrew()
    {
        if (!NewCrewWorkDate.HasValue || SelectedWorkers.Count == 0)
        {
            TempData["Error"] = "Заполните все обязательные поля!";
            return RedirectToPage();
        }

        var order = _db.Orders.FirstOrDefault(o => o.Id == SelectedOrderId);
        if (order == null) return NotFound();

        var newCrew = new OrderCrew
        {
            Id = Guid.NewGuid(),
            WorkDate = NewCrewWorkDate.Value,
            TechLeadId = GetCurrentTechLeadId(),
            WorkTaskAssignments = new List<WorkTaskAssignment>(),
            Workers = new List<Worker>()
        };

        foreach (var workerId in SelectedWorkers)
        {
            var worker = _db.Users.OfType<Worker>().First(w => w.Id == workerId);
            newCrew.Workers.Add(worker);
            _db.WorkerCrewRelations.Add((workerId, newCrew.Id));
        }

        order.OrderCrews ??= new List<OrderCrew>();
        order.OrderCrews.Add(newCrew);

        // Сброс значений
        NewCrewWorkDate = null;
        SelectedWorkers.Clear();

        return RedirectToPage();
    }

    public IActionResult OnPostRemoveCrew(Guid crewId)
    {
        var order = _db.Orders.FirstOrDefault(o => o.OrderCrews != null && o.OrderCrews.Any(c => c.Id == crewId));
        if (order != null)
        {
            var crewToRemove = order.OrderCrews.FirstOrDefault(c => c.Id == crewId);
            if (crewToRemove != null)
            {
                order.OrderCrews.Remove(crewToRemove);
            }
        }
        return RedirectToPage();
    }

    public IActionResult OnPostUpdateCrew(Guid crewId, DateTime workDate)
    {
        var crew = GetCrewById(crewId);
        crew.WorkDate = DateOnly.FromDateTime(workDate);
        return RedirectToPage();
    }

    public JsonResult OnGetCrewDetails(Guid crewId)
    {
        var crew = GetCrewById(crewId);
        return new JsonResult(new
        {
            workDate = crew.WorkDate.ToDateTime(TimeOnly.MinValue)
        });
    }
    #endregion

    #region Task Management
    public IActionResult OnPostAddTask(string taskDescription)
    {
        var crew = GetCrewById(SelectedCrewId);
        crew.WorkTaskAssignments ??= new List<WorkTaskAssignment>();
        crew.WorkTaskAssignments.Add(new WorkTaskAssignment
        {
            Id = Guid.NewGuid(),
            WorkTask = new WorkTask { Description = taskDescription }
        });
        return RedirectToPage();
    }

    public IActionResult OnPostToggleTask(Guid taskId)
    {
        var task = GetTaskById(taskId);
        task.IsCompleted = !task.IsCompleted;
        return RedirectToPage();
    }

    public IActionResult OnPostDeleteTask(Guid taskId)
    {
        var crew = _db.Orders.SelectMany(o => o.OrderCrews)
            .FirstOrDefault(c => c.WorkTaskAssignments != null && c.WorkTaskAssignments.Any(t => t.Id == taskId));
        if (crew != null)
        {
            var taskToRemove = crew.WorkTaskAssignments.FirstOrDefault(t => t.Id == taskId);
            if (taskToRemove != null)
            {
                crew.WorkTaskAssignments.Remove(taskToRemove);
            }
        }
        return RedirectToPage();
    }
    #endregion

    #region Worker Management
    public IActionResult OnPostAddWorkerToCrew(Guid workerId, Guid crewId)
    {
        if (!_db.WorkerCrewRelations.Any(r => r.WorkerId == workerId && r.CrewId == crewId))
        {
            _db.WorkerCrewRelations.Add((workerId, crewId));

            // Обновляем объект команды
            var crew = GetCrewById(crewId);
            crew.Workers ??= new List<Worker>();
            crew.Workers.Add(_db.Users.OfType<Worker>().First(w => w.Id == workerId));
        }
        return RedirectToPage();
    }

    public IActionResult OnPostRemoveWorkerFromCrew(Guid workerId, Guid crewId)
    {
        var relation = _db.WorkerCrewRelations.FirstOrDefault(r => r.WorkerId == workerId && r.CrewId == crewId);
        if (relation.CrewId != null && relation.WorkerId != null)
        {
            _db.WorkerCrewRelations.Remove(relation);
        }
        return RedirectToPage();
    }
    #endregion

    #region Helpers
    private string GetOrderTypeName(OrderType type) => type switch
    {
        OrderType.Individual => "Индивидуальный",
        OrderType.Rent => "Аренда",
        _ => "Другой"
    };

    private Guid GetCurrentTechLeadId()
    {
        var username = HttpContext.Session.GetString("Username");

        return _db.Users.OfType<TechOrderLead>().First(u => u.UserName == username).Id;
    }
    private OrderCrew GetCrewById(Guid crewId) =>
        _db.Orders.SelectMany(o => o.OrderCrews).First(c => c.Id == crewId);

    private WorkTaskAssignment GetTaskById(Guid taskId) =>
        _db.Orders.SelectMany(o => o.OrderCrews)
            .SelectMany(c => c.WorkTaskAssignments)
            .First(t => t.Id == taskId);

    public List<Worker> GetWorkersForCrew(Guid crewId)
    {
        return _db.WorkerCrewRelations
            .Where(r => r.CrewId == crewId)
            .Join(_db.Users.OfType<Worker>(),
                r => r.WorkerId,
                w => w.Id,
                (r, w) => w)
            .ToList();
    }

    public List<Worker> GetAvailableWorkers()
    {
        var allWorkers = _db.Users.OfType<Worker>().ToList();

        /*   var busyWorkers = _db.WorkerCrewRelations
               .Select(r => r.WorkerId)
               .Distinct()
               .ToList();*/

        return allWorkers/*
            .Where(w => !busyWorkers.Contains(w.Id))*/
            .ToList();
    }

    public List<OrderCrew> GetOrderCrews(Guid orderId)
    {
        return _db.Orders
            .Where(o => o.Id == orderId)
            .SelectMany(o => o.OrderCrews)
            .Select(oc => new OrderCrew
            {
                Id = oc.Id,
                WorkDate = oc.WorkDate,
                WorkTaskAssignments = oc.WorkTaskAssignments ?? new List<WorkTaskAssignment>(),
                Workers = oc.Workers ?? new List<Worker>()
            })
            .ToList();
    }
    #endregion
}