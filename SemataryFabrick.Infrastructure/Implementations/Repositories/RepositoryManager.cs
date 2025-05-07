using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationContext _context;
    private readonly Lazy<ICartItemRepository> _cartItemRepository;
    private readonly Lazy<ICartRepository> _cartRepository;
    private readonly Lazy<IDirectorRepository> _directorRepository;
    private readonly Lazy<IDiscountRepository> _discountRepository;
    private readonly Lazy<IIndividualCustomerRepository> _individualCustomerRepository;
    private readonly Lazy<IItemInventoryRepository> _itemInventoryRepository;
    private readonly Lazy<IItemRepository> _itemRepository;
    private readonly Lazy<ILegalCustomerRepository> _legalCustomerRepository;
    private readonly Lazy<IOrderBaseRepository> _orderBaseRepository;
    private readonly Lazy<IOrderCrewRepository> _orderCrewRepository;
    private readonly Lazy<IOrderItemRepository> _orderItemRepository;
    private readonly Lazy<IOrderManagerRepository> _orderManagerRepository;
    private readonly Lazy<IProductCategoryRepository> _productCategoryRepository;
    private readonly Lazy<ISubCategoryRepository> _subCategoryRepository;
    private readonly Lazy<ITechOrderLeadRepository> _techOrderLeadRepository;
    private readonly Lazy<IWorkerRepository> _workerRepository;
    private readonly Lazy<IWorkTaskAssignmentRepository> _workTaskAssignmentRepository;
    private readonly Lazy<IWorkTaskRepository> _workTaskRepository;
    private readonly Lazy<IUserRepository> _userRepository;

    public RepositoryManager(ApplicationContext context)
    {
        _context = context;
        _cartItemRepository = new Lazy<ICartItemRepository>(() => new CartItemRepository(_context));
        _cartRepository = new Lazy<ICartRepository>(() => new CartRepository(_context));
        _discountRepository = new Lazy<IDiscountRepository>(() => new DiscountRepository(_context));
        _itemInventoryRepository = new Lazy<IItemInventoryRepository>(() => new ItemInventoryRepository(_context));
        _itemRepository = new Lazy<IItemRepository>(() => new ItemRepository(_context));
        _orderBaseRepository = new Lazy<IOrderBaseRepository>(() => new OrderBaseRepository(_context));
        _orderCrewRepository = new Lazy<IOrderCrewRepository>(() => new OrderCrewRepository(_context));
        _orderItemRepository = new Lazy<IOrderItemRepository>(() => new OrderItemRepository(_context));
        _productCategoryRepository = new Lazy<IProductCategoryRepository>(() => new ProductCategoryRepository(_context));
        _subCategoryRepository = new Lazy<ISubCategoryRepository>(() => new SubCategoryRepository(_context));
        _workTaskAssignmentRepository = new Lazy<IWorkTaskAssignmentRepository>(() => new WorkTaskAssignmentRepository(_context));
        _workTaskRepository = new Lazy<IWorkTaskRepository>(() => new WorkTaskRepository(_context));
        _directorRepository = new Lazy<IDirectorRepository>(() => new DirectorRepository(_context));
        _individualCustomerRepository = new Lazy<IIndividualCustomerRepository>(() => new IndividualCustomerRepository(_context));
        _legalCustomerRepository = new Lazy<ILegalCustomerRepository>(() => new LegalCustomerRepository(_context));
        _orderManagerRepository = new Lazy<IOrderManagerRepository>(() => new OrderManagerRepository(_context));
        _techOrderLeadRepository = new Lazy<ITechOrderLeadRepository>(() => new TechOrderLeadRepository(_context));
        _workerRepository = new Lazy<IWorkerRepository>(() => new WorkerRepository(_context));
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
    }

    public ICartItemRepository CartItem => _cartItemRepository.Value;
    public ICartRepository Cart => _cartRepository.Value;
    public IDirectorRepository Director => _directorRepository.Value;
    public IDiscountRepository Discount => _discountRepository.Value;
    public IIndividualCustomerRepository IndividualCustomer => _individualCustomerRepository.Value;
    public IItemInventoryRepository ItemInventory => _itemInventoryRepository.Value;
    public IItemRepository Item => _itemRepository.Value;
    public ILegalCustomerRepository LegalCustomer => _legalCustomerRepository.Value;
    public IOrderBaseRepository OrderBase => _orderBaseRepository.Value;
    public IOrderCrewRepository OrderCrew => _orderCrewRepository.Value;
    public IOrderItemRepository OrderItem =>_orderItemRepository.Value;
    public IOrderManagerRepository OrderManager => _orderManagerRepository.Value;
    public IProductCategoryRepository ProductCategory => _productCategoryRepository.Value;
    public ISubCategoryRepository SubCategory => _subCategoryRepository.Value;
    public ITechOrderLeadRepository TechOrderLead => _techOrderLeadRepository.Value;
    public IWorkerRepository Worker => _workerRepository.Value;
    public IWorkTaskAssignmentRepository TaskAssignment => _workTaskAssignmentRepository.Value;
    public IWorkTaskRepository WorkTask => _workTaskRepository.Value;
    public IUserRepository User => _userRepository.Value;

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}