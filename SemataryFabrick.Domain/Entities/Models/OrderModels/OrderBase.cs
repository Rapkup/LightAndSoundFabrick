using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Domain.Entities.Models.Users;

namespace SemataryFabrick.Domain.Entities.Models.Order.Order;
public class OrderBase
{
    public Guid Id { get; set; }
    public string EventAddress { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderType OrderType { get; set; }
    public PaymentStatus PaymentState { get; set; }
    public Guid OrderTypeInstanceId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid OrderManagerId { get; set; }
    public Guid TechOrderLeadId { get; set; }

    public OrderTypeBase OrderTypeInstance { get; set; }
    public ApplicationUser Customer { get; set; }
    public OrderManager OrderManager { get; set; }
    public TechOrderLead TechOrderLead { get; set; }
    public IEnumerable<OrderItem> OrderItems { get; set; }
    public IEnumerable<OrderCrew> OrderCrews { get; set; }
}