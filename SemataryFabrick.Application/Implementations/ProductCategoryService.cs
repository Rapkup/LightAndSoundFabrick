using Microsoft.Extensions.Logging;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models;

namespace SemataryFabrick.Application.Implementations;
public class ProductCategoryService(IRepositoryManager repositoryManager, ILogger<ProductCategoryService> logger) : IProductCategoryService
{
    public async Task<IEnumerable<ProductCategoryDto>> GetAllProductCategoriesAsync()
    {
        try
        {
            var categories = await repositoryManager.ProductCategory.GetAllProductCategoriesAsync();
            return categories.Select(ProductCategoryDto.FromEntity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving categories");
            return Enumerable.Empty<ProductCategoryDto>();
        }
    }
}