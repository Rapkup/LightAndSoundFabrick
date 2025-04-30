using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Domain.Entities.Models.Users;

namespace SemataryFabrick.Domain.Entities.Models.UserModels;
public class Worker : ApplicationUser
{
    public IEnumerable<OrderCrew> OrderCrews { get; set; }
}
