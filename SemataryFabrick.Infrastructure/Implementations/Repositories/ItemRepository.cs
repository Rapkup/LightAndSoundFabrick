using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models.Items;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class ItemRepository(ApplicationContext context) : RepositoryBase<Item>(context), IItemRepository
{
    public Task AddItemAsync(Item item) => CreateAsync(item);

    public void DeleteItem(Item item) => Delete(item);

    public void UpdateItem(Item item) => Update(item);

    public async Task<IEnumerable<Item>> GetAllItemsAsync()
        => await Find().ToListAsync();

    public async Task<Item?> GetItemAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}