using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models.ProduIctItemModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class SubCategoryRepository(ApplicationContext context) : RepositoryBase<SubCategory>(context), ISubCategoryRepository
{
    public Task AddSubCategoryAsync(SubCategory subCategory) => CreateAsync(subCategory);

    public void DeleteSubCategory(SubCategory subCategory) => Delete(subCategory);

    public void UpdateSubCategory(SubCategory subCategory) => Update(subCategory);

    public async Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync()
        => await Find().ToListAsync();

    public async Task<SubCategory?> GetSubCategoryAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();

    public async Task<IEnumerable<SubCategory>> GetSubCategoriesByParentIdsAsync(IEnumerable<Guid> parentIds)
    {
        return await context.SubCategories
            .Where(sc => parentIds.Contains(sc.ParentCategoryId))
            .ToListAsync();
    }

    public async Task<IEnumerable<SubCategory>?> GetSubCategoriesByParentId(Guid productCategoryId)
    {
        return await Find(sc => sc.ParentCategoryId == productCategoryId).ToListAsync();
    }
}