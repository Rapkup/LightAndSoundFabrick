using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.UserModels;

namespace SemataryFabrick.Application.Entities.DTOs.UserDtos;
public record UserDto
{
    public Guid Id { get; set; }
    public UserType UserType { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PassportIdNumber { get; set; }

    public string? CompanyName { get; set; }
    public string? TaxIdNumber { get; set; }
    public string? LegalAddress { get; set; }
    public string? ContactPersonFullName { get; set; }
    public bool? isGovernment { get; set; }
    public string? GovernmentCode { get; set; }

    public static UserDto ToUserDto(ApplicationUser user)
    {
        var dto = new UserDto
        {
            Id = user.Id,
            UserType = user.UserType,
            UserName = user.UserName,
            Password = user.Password,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        if (user is IndividualCustomer individual)
        {
            dto.PassportIdNumber = individual.PassportIdNumber;
        }

        if (user is LegalCustomer legal)
        {
            dto.CompanyName = legal.CompanyName;
            dto.TaxIdNumber = legal.TaxIdNumber;
            dto.LegalAddress = legal.LegalAddress;
            dto.ContactPersonFullName = legal.ContactPersonFullName;
            dto.isGovernment = legal.isGovernment;
            dto.GovernmentCode = legal.GovernmentCode;
        }

        return dto;
    }

    public static ApplicationUser ToApplicationUser(UserDto userDto)
    {
        return userDto.UserType switch
        {
            UserType.IndividualCustomer => new IndividualCustomer
            {
                Id = userDto.Id,
                UserType = userDto.UserType,
                UserName = userDto.UserName,
                Password = userDto.Password,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                PassportIdNumber = userDto.PassportIdNumber 
            },

            UserType.LegalCustomer => new LegalCustomer
            {
                Id = userDto.Id,
                UserType = userDto.UserType,
                UserName = userDto.UserName,
                Password = userDto.Password,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                CompanyName = userDto.CompanyName,
                TaxIdNumber = userDto.TaxIdNumber,
                LegalAddress = userDto.LegalAddress,
                ContactPersonFullName = userDto.ContactPersonFullName,
                isGovernment = userDto.isGovernment,
                GovernmentCode = userDto.GovernmentCode
            },

            _ => throw new ArgumentException($"Unsupported user type: {userDto.UserType}")
        };
    }

}