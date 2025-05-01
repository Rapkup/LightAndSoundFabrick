using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Domain.Entities.Models.UserModels;
public class Worker : ApplicationUser
{
    public IEnumerable<OrderCrew> OrderCrews { get; set; }
}
