using SemataryFabrick.Domain.Entities.Models.UserModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IUserRepository
{
    Task<ApplicationUser> Login(string username, string password);
    Task<bool> IsUserExist(string username);
}