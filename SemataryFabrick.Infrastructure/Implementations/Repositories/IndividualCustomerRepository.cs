using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class IndividualCustomerRepository (ApplicationContext context)
        : RoleRepositoryBase<IndividualCustomer>(context),
        IIndividualCustomerRepository
{
    protected override UserType GetUserType() => UserType.IndividualCustomer;
    public Task AddIndividualCustomerAsync(IndividualCustomer individualCustomer) => CreateAsync(individualCustomer);

    public void DeleteIndividualCustomer(IndividualCustomer individualCustomer) => Delete(individualCustomer);
    public void UpdateIndividualCustomer(IndividualCustomer individualCustomer) => Update(individualCustomer);

    public async Task<IEnumerable<IndividualCustomer>> GetAllIndividualCustomersAsync()
        => await Find().ToListAsync();
    public async Task<IndividualCustomer?> GetIndividualCustomerAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}