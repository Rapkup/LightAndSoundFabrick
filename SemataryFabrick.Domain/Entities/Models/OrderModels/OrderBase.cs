using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.UserModels;

namespace SemataryFabrick.Domain.Entities.Models.OrderModels;
public class OrderBase
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }

    public string? EventAddress { get; set; }
    public DateOnly? EventDate { get; set; }
    public DateOnly? StartRentDate { get; set; }
    public DateOnly? EndRentDate { get; set; }
    public OrderState OrderState { get; set; }
    public OrderType OrderType { get; set; }
    public PaymentStatus PaymentState { get; set; }
    public Guid CustomerId { get; set; }
    public Guid OrderManagerId { get; set; }
    public Guid TechOrderLeadId { get; set; }

    public ApplicationUser Customer { get; set; }
    public OrderManager OrderManager { get; set; }
    public TechOrderLead TechOrderLead { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<OrderCrew> OrderCrews { get; set; }
}