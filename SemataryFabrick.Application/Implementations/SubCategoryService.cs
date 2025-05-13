using Microsoft.Extensions.Logging;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs;
using SemataryFabrick.Application.Entities.Exceptions;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models.ProduIctItemModels;

namespace SemataryFabrick.Application.Implementations;
public class SubCategoryService(IRepositoryManager repositoryManager, ILogger<SubCategoryService> logger) : ISubCategoryService
{
    public async Task<IEnumerable<SubCategoryDto>> GetSubCategoriesByParentIdsAsync(IEnumerable<Guid> parentIds)
    {
        var subCategories = await repositoryManager.SubCategory.GetSubCategoriesByParentIdsAsync(parentIds);
        return subCategories.Select(SubCategoryDto.FromEntity).ToList();
    }

    public async Task<IEnumerable<SubCategoryDto>> GetSubCategoriesByIdsAsync(IEnumerable<Guid> subCategoryIds)
    {
        var subCategories = new List<SubCategoryDto>();

        foreach (var subCategoryId in subCategoryIds)
        {
            var subCategory = await repositoryManager.SubCategory.GetSubCategoryAsync(subCategoryId);

            if (subCategory == null)
            {
                throw new EntityNotFoundException(nameof(SubCategory), subCategoryId);
            }

            subCategories.Add(SubCategoryDto.FromEntity(subCategory));
        }

        return subCategories;
    }

    public async Task<IEnumerable<SubCategoryDto>> GetAllSubCategoriesAsync()
    {
        var subCategories = await repositoryManager.SubCategory.GetAllSubCategoriesAsync();

        return subCategories.Select(SubCategoryDto.FromEntity);
    }

    public async Task<IEnumerable<SubCategoryDto>?> GetSubCategoriesByParentIdAsync(Guid parentId)
    {
        var subCategories = await repositoryManager.SubCategory.GetSubCategoriesByParentId(parentId);

        if (subCategories == null)
        {
            throw new  EntityNotFoundException(nameof(SubCategoryDto), parentId);
        }
        return subCategories.Select(SubCategoryDto.FromEntity);
    }
}