using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class UserRepository(ApplicationContext context) : IUserRepository
{
    public async Task<ApplicationUser?> Login(string username, string password)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.UserName == username || u.Email == username);

        if (user == null || user.Password != password)
            return null;

        return user switch
        {
            IndividualCustomer individual => individual,
            LegalCustomer legal => legal,
            _ => user
        };
    }

    public async Task<bool> IsUserExist(string username)
    {
        bool userExists = await context.Users
            .AnyAsync(u => u.UserName == username || u.Email == username);

        if (userExists)
            return true;

        return false;
    }

    public async Task<ApplicationUser?> GetRandomByTypeAsync(UserType userType)
    {
        return await context.Users
            .Where(u => u.UserType == userType)
            .OrderBy(_ => Guid.NewGuid())
            .FirstOrDefaultAsync();
    }
}