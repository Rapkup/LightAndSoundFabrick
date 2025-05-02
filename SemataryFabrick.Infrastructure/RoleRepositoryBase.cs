using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure;
public abstract class RoleRepositoryBase<TRole>(ApplicationContext context) 
    : RepositoryBase<TRole>(context) 
    where TRole : ApplicationUser, new()
{
    protected override async Task CreateAsync(TRole entity)
    {
        entity.UserType = GetUserType();
        await base.CreateAsync(entity);
    }

    protected abstract UserType GetUserType();
}
