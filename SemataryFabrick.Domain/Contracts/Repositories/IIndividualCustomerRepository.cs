using SemataryFabrick.Domain.Entities.Models.UserModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IIndividualCustomerRepository
{
    void DeleteIndividualCustomer(IndividualCustomer individualCustomer);
    void UpdateIndividualCustomer(IndividualCustomer individualCustomer);
    Task AddIndividualCustomerAsync(IndividualCustomer individualCustomer);
    Task<IndividualCustomer?> GetIndividualCustomerAsync(Guid id);
    Task<IEnumerable<IndividualCustomer>> GetAllIndividualCustomersAsync();
}