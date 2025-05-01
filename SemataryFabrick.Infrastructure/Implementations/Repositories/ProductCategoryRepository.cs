using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class ProductCategoryRepository(ApplicationContext context) : RepositoryBase<ProductCategory>(context), IProductCategoryRepository
{
    public Task AddProductCategoryAsync(ProductCategory productCategory) => CreateAsync(productCategory);

    public void DeleteProductCategory(ProductCategory productCategory) => Delete(productCategory);

    public void UpdateProductCategory(ProductCategory productCategory) => Update(productCategory);

    public async Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync()
        => await Find().ToListAsync();

    public async Task<ProductCategory?> GetProductCategoryAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}