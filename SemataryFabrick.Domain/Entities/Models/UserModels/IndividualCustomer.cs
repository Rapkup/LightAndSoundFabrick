using SemataryFabrick.Domain.Entities.Models.Order.Order;
using SemataryFabrick.Domain.Entities.Models.Users;

namespace SemataryFabrick.Domain.Entities.Models.UserModels;
public class IndividualCustomer : ApplicationUser
{
    public string PassportIdNumber { get; set; }

    public IEnumerable<OrderBase> OrderBases { get; set; }
}