using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Domain.Entities.Models.UserModels;
public class TechOrderLead : ApplicationUser
{
    public IEnumerable<OrderBase> OrderBases { get; set; }  
    public IEnumerable<OrderCrew> OrderCrews { get; set; }
}
