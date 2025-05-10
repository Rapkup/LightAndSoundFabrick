using SemataryFabrick.Application.Entities.DTOs;

namespace SemataryFabrick.Application.Contracts.Services;
public interface IProductCategoryService
{
    Task<IEnumerable<ProductCategoryDto>> GetAllProductCategoriesAsync();
}