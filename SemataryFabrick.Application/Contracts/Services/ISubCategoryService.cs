using SemataryFabrick.Application.Entities.DTOs;

namespace SemataryFabrick.Application.Contracts.Services;
public interface ISubCategoryService
{
    Task<IEnumerable<SubCategoryDto>> GetSubCategoriesByParentIdsAsync(IEnumerable<Guid> parentIds);
    Task<IEnumerable<SubCategoryDto>> GetSubCategoriesByIdsAsync(IEnumerable<Guid> subCategoryIds);
    Task<IEnumerable<SubCategoryDto>> GetAllSubCategoriesAsync();
}