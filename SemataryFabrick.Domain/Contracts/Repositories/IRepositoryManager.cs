namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IRepositoryManager
{
    ICartItemRepository CartItem { get; }
    ICartRepository Cart { get; }
    IDirectorRepository Director { get; }
    IDiscountRepository Discount { get; }
    IIndividualCustomerRepository IndividualCustomer { get; }
    IItemInventoryRepository ItemInventory { get; }
    IItemRepository Item { get; }
    ILegalCustomerRepository LegalCustomer { get; }
    IOrderBaseRepository OrderBase { get; }
    IOrderCrewRepository OrderCrew { get; }
    IOrderItemRepository OrderItem { get; }
    IOrderManagerRepository OrderManager { get; }
    IProductCategoryRepository ProductCategory { get; }
    ISubCategoryRepository SubCategory { get; }
    ITechOrderLeadRepository TechOrderLead { get; }
    IWorkerRepository Worker { get; }
    IWorkTaskAssignmentRepository TaskAssignment { get; }
    IWorkTaskRepository WorkTask { get; }
    IUserRepository User { get; }
    Task SaveAsync();
}