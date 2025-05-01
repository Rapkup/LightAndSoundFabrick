using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models.Items;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class ItemInventoryRepository(ApplicationContext context) : RepositoryBase<ItemInventory>(context), IItemInventoryRepository
{
    public Task AddItemInventoryAsync(ItemInventory ItemInventory) => CreateAsync(ItemInventory);

    public void DeleteItemInventory(ItemInventory ItemInventory) => Delete(ItemInventory);

    public void UpdateItemInventory(ItemInventory ItemInventory) => Update(ItemInventory);

    public async Task<IEnumerable<ItemInventory>> GetAllItemInventoriesAsync()
        => await Find().ToListAsync();

    public async Task<ItemInventory?> GetItemInventoryAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}