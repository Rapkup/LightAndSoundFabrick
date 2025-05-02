using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class TechOrderLeadRepository(ApplicationContext context)
        : RoleRepositoryBase<TechOrderLead>(context),
        ITechOrderLeadRepository
{
    protected override UserType GetUserType() => UserType.TechOrderLead;
    public Task AddTechOrderLeadAsync(TechOrderLead techOrderLead) => CreateAsync(techOrderLead);

    public void DeleteTechOrderLead(TechOrderLead techOrderLead) => Delete(techOrderLead);
    public void UpdateTechOrderLead(TechOrderLead techOrderLead) => Update(techOrderLead);

    public async Task<IEnumerable<TechOrderLead>> GetAllTechOrderLeadsAsync()
        => await Find().ToListAsync();
    public async Task<TechOrderLead?> GetTechOrderLeadAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}