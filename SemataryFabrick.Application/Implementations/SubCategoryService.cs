using Microsoft.Extensions.Logging;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs;
using SemataryFabrick.Application.Entities.Exceptions;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models;

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
}