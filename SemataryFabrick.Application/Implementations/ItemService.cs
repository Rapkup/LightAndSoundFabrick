using Microsoft.Extensions.Logging;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs.ProductItemDtos;
using SemataryFabrick.Application.Entities.Exceptions;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models.CartModels;
using SemataryFabrick.Domain.Entities.Models.Items;

namespace SemataryFabrick.Application.Implementations;
public class ItemService(IRepositoryManager repositoryManager, ILogger<ItemService> logger) : IItemService
{
    public async Task<IEnumerable<ItemDto>> GetItemsBySubCategoriesAsync(IEnumerable<Guid> subCategoryIds)
    {
        var items = await repositoryManager.Item.GetItemsBySubCategoriesAsync(subCategoryIds);
        return items.Select(ItemDto.FromEntity).ToList();
    }

    public async Task<IEnumerable<ItemDto>> GetItemsByCartItemIds(IEnumerable<Guid> cartItemsId)
    {
        var itemList = new List<ItemDto>();

        foreach (var cartItemId in cartItemsId)
        {

            var cartItem = await repositoryManager.CartItem.GetCartItemWithProductAsync(cartItemId);

            if (cartItem != null && cartItem.Product != null)
            {
                itemList.Add(ItemDto.FromEntity(cartItem.Product));
            }
            else
            {
                throw new EntityNotFoundException(nameof(CartItem)+" or "+nameof(Item), cartItemId);
            }
        }
        return itemList;
    }
}