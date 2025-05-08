using Microsoft.Extensions.Logging;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs.ProductItemDtos;
using SemataryFabrick.Domain.Contracts.Repositories;

namespace SemataryFabrick.Application.Implementations;
public class ItemService(IRepositoryManager repositoryManager, ILogger<ItemService> logger) : IItemService
{
    public async Task<IEnumerable<ItemDto>> GetItemsBySubCategoriesAsync(IEnumerable<Guid> subCategoryIds)
    {
        try
        {
            var items = await repositoryManager.Item.GetItemsBySubCategoriesAsync(subCategoryIds);
            return items.Select(ItemDto.FromEntity).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving items");
            return Enumerable.Empty<ItemDto>();
        }
    }
}