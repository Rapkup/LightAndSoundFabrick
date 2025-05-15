using SemataryFabrick.Domain.Entities.Models.CartModels;
using SemataryFabrick.Domain.Entities.Models.Items;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Domain.Entities.Models.ProduIctItemModels;
using SemataryFabrick.Domain.Entities.Models;
using SemataryFabrick.Domain.Entities.Models.UserModels;

namespace SemataryFabrick.Infrastructure.Extensions.InMemoryDb;
public class InMemoryDatabase
{
    public List<ApplicationUser> Users { get; } = new();
    public List<Discount> Discounts { get; } = new();
    public List<ProductCategory> ProductCategories { get; } = new();
    public List<SubCategory> SubCategories { get; } = new();
    public List<Item> Items { get; } = new();
    public List<ItemInventory> Inventories { get; } = new();
    public List<OrderBase> Orders { get; } = new();
    public List<Cart> Carts { get; } = new();
    public List<WorkTaskAssignment> WorkTaskAssignments { get; } = new();
    public List<WorkTask> WorkTasks { get; } = new();
    public List<OrderItem> OrderItems { get; } = new();
    public List<CartItem> CartItems { get; } = new();

    public List<OrderCrew> OrderCrews { get; } = new();

    // Связь многие-ко-многим для Worker и OrderCrew
    public List<(Guid WorkerId, Guid CrewId)> WorkerCrewRelations { get; } = new();

    // Методы для работы с пользователями
    public List<T> GetUsersByType<T>() where T : ApplicationUser
        => Users.OfType<T>().ToList();

    // Методы для управления связями
    public void AddWorkerToCrew(Guid workerId, Guid crewId)
        => WorkerCrewRelations.Add((workerId, crewId));

    public List<Worker> GetWorkersForCrew(Guid crewId)
        => WorkerCrewRelations
            .Where(x => x.CrewId == crewId)
            .Join(Users.OfType<Worker>(),
                relation => relation.WorkerId,
                worker => worker.Id,
                (relation, worker) => worker)
            .ToList();
}