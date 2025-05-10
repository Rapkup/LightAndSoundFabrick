using SemataryFabrick.Application.Entities.DTOs.ProductItemDtos;

namespace SemataryFabrick.Application.Contracts.Services;
public interface IItemService
{
    Task<IEnumerable<ItemDto>> GetItemsBySubCategoriesAsync(IEnumerable<Guid> subCategoryIds);
    Task<IEnumerable<ItemDto>> GetItemsByCartItemIds(IEnumerable<Guid> cartItemsId);
}