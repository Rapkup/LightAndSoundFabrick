using SemataryFabrick.Domain.Entities.Models.Items;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IItemInventoryRepository
{
    void DeleteItemInventory(ItemInventory itemInventory);
    void UpdateItemInventory(ItemInventory itemInventory);
    Task AddItemInventoryAsync(ItemInventory itemInventory);
    Task<ItemInventory?> GetItemInventoryAsync(Guid id);
    Task<IEnumerable<ItemInventory>> GetAllItemInventoriesAsync();
}