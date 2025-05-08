using Microsoft.Extensions.Logging;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models;

namespace SemataryFabrick.Application.Implementations;
public class SubCategoryService(IRepositoryManager repositoryManager, ILogger<SubCategoryService> logger) : ISubCategoryService
{
    public async Task<IEnumerable<SubCategoryDto>> GetSubCategoriesByParentIdsAsync(IEnumerable<Guid> parentIds)
    {
        try
        {
            var subCategories = await repositoryManager.SubCategory.GetSubCategoriesByParentIdsAsync(parentIds);
            return subCategories.Select(SubCategoryDto.FromEntity).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving subCategories");
            return Enumerable.Empty<SubCategoryDto>();
        }
    }
}