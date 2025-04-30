using SemataryFabrick.Domain.Entities.Enums;

namespace SemataryFabrick.Domain.Entities.Models.Users;
public class ApplicationUser
{
    public Guid Id { get; set; }
    public UserType UserType { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
