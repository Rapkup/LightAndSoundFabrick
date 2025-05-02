using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class DirectorRepository(ApplicationContext context)
        : RoleRepositoryBase<Director>(context),
        IDirectorRepository
{
    protected override UserType GetUserType() => UserType.Director;
    public Task AddDirectorAsync(Director director) => CreateAsync(director);

    public void DeleteDirector(Director director) => Delete(director);
    public void UpdateDirector(Director director) => Update(director);

    public async Task<IEnumerable<Director>> GetAllDirectorsAsync()
        => await Find().ToListAsync();
    public async Task<Director?> GetDirectorAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}