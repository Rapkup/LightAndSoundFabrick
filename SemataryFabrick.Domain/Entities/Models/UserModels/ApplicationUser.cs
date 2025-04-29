using Microsoft.AspNetCore.Identity;

namespace SemataryFabrick.Domain.Entities.Models.Users;
public class ApplicationUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
