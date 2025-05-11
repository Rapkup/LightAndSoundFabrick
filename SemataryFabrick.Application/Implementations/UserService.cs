using Microsoft.Extensions.Logging;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs.UserDtos;
using SemataryFabrick.Application.Entities.Exceptions;
using SemataryFabrick.Domain.Entities.Models.UserModels;

namespace SemataryFabrick.Application.Implementations;
public class UserService(IRepositoryManager repositoryManager, ILogger<UserService> logger) : IUserService
{
    public async Task<UserDto> Login(string username, string password)
    {
        logger.LogInformation("Executing service method {methodName}", nameof(Login));

        var userAuthorized = await repositoryManager.User.Login(username, password);

        if (userAuthorized == null)
        {
            throw new EntityNotFoundException(nameof(ApplicationUser), username);
        }

        logger.LogInformation("User {username} authenticated", username);

        return UserDto.ToUserDto(userAuthorized);
    }
    public async Task<UserDto> Register(UserDto userForRegistry)
    {
        logger.LogInformation("Executing service method {methodName}", nameof(Register));

        bool isUserExist = await repositoryManager.User.IsUserExist(userForRegistry.UserName);

        if (isUserExist)
        {
            throw new EntityDuplicationException(nameof(UserDto), nameof(userForRegistry.UserName), userForRegistry.UserName);
        }

        ApplicationUser userToRegister = UserDto.ToApplicationUser(userForRegistry);

        if (userForRegistry.UserType == Domain.Entities.Enums.UserType.IndividualCustomer)
        {
            await repositoryManager.IndividualCustomer.AddIndividualCustomerAsync((IndividualCustomer)userToRegister);
            await repositoryManager.SaveAsync();
            logger.LogInformation("IndividualCustomer {username} registered successfully", userForRegistry.UserName);
        }
        else
        {
            await repositoryManager.LegalCustomer.AddLegalCustomerAsync((LegalCustomer)userToRegister);
            await repositoryManager.SaveAsync();
            logger.LogInformation("LegalCustomer {username} registered successfully", userForRegistry.UserName);
        }

        return UserDto.ToUserDto(userToRegister);
    }

    public async Task<UserDto> GetUserAsync(Guid id)
    {
        return UserDto.ToUserDto(await repositoryManager.User.GetUserAsync(id));
    }
}