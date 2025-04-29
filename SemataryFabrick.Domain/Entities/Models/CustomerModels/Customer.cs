using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.Users;

namespace SemataryFabrick.Domain.Entities.Models.Customers;

public class Customer : ApplicationUser
{
    public CustomerType customerType { get; set; }
}
