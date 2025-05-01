using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class LegalCustomerRepository(ApplicationContext context)
        : RoleRepositoryBase<LegalCustomer>(context),
        ILegalCustomerRepository
{
    protected override UserType GetUserType() => UserType.LegalCustomer;
    public Task AddLegalCustomerAsync(LegalCustomer legalCustomer) => CreateAsync(legalCustomer);

    public void DeleteLegalCustomer(LegalCustomer legalCustomer) => Delete(legalCustomer);
    public void UpdateLegalCustomer(LegalCustomer legalCustomer) => Update(legalCustomer);

    public async Task<IEnumerable<LegalCustomer>> GetAllLegalCustomersAsync()
        => await Find().ToListAsync();
    public async Task<LegalCustomer?> GetLegalCustomerAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}