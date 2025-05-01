using SemataryFabrick.Domain.Entities.Models.UserModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface ILegalCustomerRepository
{
    void DeleteLegalCustomer(LegalCustomer legalCustomer);
    void UpdateLegalCustomer(LegalCustomer legalCustomer);
    Task AddLegalCustomerAsync(LegalCustomer legalCustomer);
    Task<LegalCustomer?> GetLegalCustomerAsync(Guid id);
    Task<IEnumerable<LegalCustomer>> GetAllLegalCustomersAsync();
}