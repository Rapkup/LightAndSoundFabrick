using SemataryFabrick.Domain.Entities.Models.Order.Order;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Domain.Entities.Models.Users;

namespace SemataryFabrick.Domain.Entities.Models.UserModels;
public class TechOrderLead : ApplicationUser
{
    public IEnumerable<OrderBase> OrderBases { get; set; }  
    public IEnumerable<OrderCrew> OrderCrews { get; set; }
}
