using SemataryFabrick.Domain.Entities.Models.Order.Order;

namespace SemataryFabrick.Domain.Entities.Models.Order;
public class IndividualOrder : OrderTypeBase
{
    public DateTime EventDate { get; set; }

    public OrderBase Order { get; set; }
}
