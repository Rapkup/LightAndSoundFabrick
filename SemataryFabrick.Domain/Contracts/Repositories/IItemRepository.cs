using SemataryFabrick.Domain.Entities.Models.Items;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IItemRepository
{
    void DeleteItem(Item item);
    void UpdateItem(Item item);
    Task AddItemAsync(Item item);
    Task<Item?> GetItemAsync(Guid id);
    Task<Item> GetItemBySubCategoryAsync(Guid subCategoryId);
    Task<IEnumerable<Item>> GetAllItemsAsync();
    Task<IEnumerable<Item>> GetItemsBySubCategoriesAsync(IEnumerable<Guid> subCategoryIds);
}