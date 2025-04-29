namespace SemataryFabrick.Domain.Entities.Models.Order;
public class RentOrder : OrderTypeBase
{
    public DateTime StartRentDate { get; set; }
    public DateTime EndRentDate { get; set; }
}
