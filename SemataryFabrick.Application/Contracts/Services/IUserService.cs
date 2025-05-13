using Microsoft.AspNetCore.Mvc.Rendering;
using SemataryFabrick.Application.Entities.DTOs.UserDtos;

namespace SemataryFabrick.Application.Contracts.Services;
public interface IUserService
{
    Task<UserDto> Login(string username, string password);
    Task<UserDto> Register(UserDto userForRegistry);

    Task<UserDto> GetUserAsync(Guid id);
    Task<IEnumerable<UserDto>> GetTechLeads();
    SelectList GetTechLeadsSelectList(IEnumerable<UserDto> techLeads);
}