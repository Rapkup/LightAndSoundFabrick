using SemataryFabrick.Domain.Entities.Models;

namespace SemataryFabrick.Domain.Contracts.Repositories;

public interface IDiscountRepository
{
    void DeleteDiscount(Discount discount);
    void UpdateDiscount(Discount discount);
    Task AddDiscountAsync(Discount discount);
    Task<Discount?> GetDiscountAsync(Guid id);
    Task<IEnumerable<Discount>> GetAllDiscountsAsync();
}