using SemataryFabrick.Domain.Entities.Models;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IProductCategoryRepository
{
    void DeleteProductCategory(ProductCategory productCategory);
    void UpdateProductCategory(ProductCategory productCategory);
    Task AddProductCategoryAsync(ProductCategory productCategory);
    Task<ProductCategory?> GetProductCategoryAsync(Guid id);
    Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync();
}