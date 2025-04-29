using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.Customers;

namespace SemataryFabrick.Domain.Entities.Models.Order.Order;
public class OrderBase
{
    public Guid Id { get; set; }
    public string EventAddress { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderType OrderType { get; set; }
    public PaymentState PaymentState { get; set; }
    public Guid OrderTypeInstanceId { get; set; }
    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; }
    public OrderTypeBase OrderTypeInstance { get; set; }
    public IEnumerable<OrderItem> OrderItems { get; set; }

}