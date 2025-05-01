using SemataryFabrick.Domain.Entities.Models.UserModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface ITechOrderLeadRepository
{
    void DeleteTechOrderLead(TechOrderLead techOrderLead);
    void UpdateTechOrderLead(TechOrderLead techOrderLead);
    Task AddTechOrderLeadAsync(TechOrderLead techOrderLead);
    Task<TechOrderLead?> GetTechOrderLeadAsync(Guid id);
    Task<IEnumerable<TechOrderLead>> GetAllTechOrderLeadsAsync();
}