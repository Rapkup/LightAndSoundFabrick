using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Domain.Entities.Models.UserModels;
public class OrderManager : ApplicationUser
{
    public IEnumerable<OrderBase> OrderBases { get; set; }
}