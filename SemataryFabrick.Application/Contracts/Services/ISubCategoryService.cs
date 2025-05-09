using SemataryFabrick.Application.Entities.DTOs;
using SemataryFabrick.Domain.Entities.Models;

namespace SemataryFabrick.Application.Contracts.Services;
public interface ISubCategoryService
{
    Task<IEnumerable<SubCategoryDto>> GetSubCategoriesByParentIdsAsync(IEnumerable<Guid> parentIds);
    Task<IEnumerable<SubCategoryDto>> GetSubCategoriesByIdsAsync(IEnumerable<Guid> subCategoryIds);
}