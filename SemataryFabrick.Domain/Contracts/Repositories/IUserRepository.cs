using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.UserModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IUserRepository
{
    Task<ApplicationUser> Login(string username, string password);
    Task<bool> IsUserExist(string username);
    Task<ApplicationUser?> GetRandomByTypeAsync(UserType userType);
    Task<ApplicationUser> GetUserAsync(Guid id);
}