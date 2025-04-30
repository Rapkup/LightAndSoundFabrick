using SemataryFabrick.Domain.Entities.Models.Order.Order;

namespace SemataryFabrick.Domain.Entities.Models.Order;
public class RentOrder : OrderTypeBase
{
    public DateTime StartRentDate { get; set; }
    public DateTime EndRentDate { get; set; }

    public OrderBase Order { get; set; }
}
