using SemataryFabrick.Domain.Entities.Models.UserModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IDirectorRepository
{
    void DeleteDirector(Director director);
    void UpdateDirector(Director director);
    Task AddDirectorAsync(Director director);
    Task<Director?> GetDirectorAsync(Guid id);
    Task<IEnumerable<Director>> GetAllDirectorsAsync();
}