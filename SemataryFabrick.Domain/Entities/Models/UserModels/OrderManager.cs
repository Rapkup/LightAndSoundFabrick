using SemataryFabrick.Domain.Entities.Models.Order.Order;

namespace SemataryFabrick.Domain.Entities.Models.Users;
public class OrderManager : ApplicationUser
{
    public IEnumerable<OrderBase> OrderBases { get; set; }
}