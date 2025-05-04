using SemataryFabrick.Domain.Entities.Enums;

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
}