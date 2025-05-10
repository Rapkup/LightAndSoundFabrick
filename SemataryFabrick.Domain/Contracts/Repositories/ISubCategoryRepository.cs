using SemataryFabrick.Domain.Entities.Models;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface ISubCategoryRepository
{
    void DeleteSubCategory(SubCategory subCategory);
    void UpdateSubCategory(SubCategory subCategory);
    Task AddSubCategoryAsync(SubCategory subCategory);
    Task<SubCategory?> GetSubCategoryAsync(Guid id);
    Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync();
    Task<IEnumerable<SubCategory>> GetSubCategoriesByParentIdsAsync(IEnumerable<Guid> parentIds);
}