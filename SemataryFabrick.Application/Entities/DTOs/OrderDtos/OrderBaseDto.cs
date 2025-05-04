using SemataryFabrick.Domain.Entities.Enums;

namespace SemataryFabrick.Application.Entities.DTOs.OrderDtos;
public record OrderBaseDto
{
    public Guid Id { get; set; }
    public string EventAddress { get; set; }
    public decimal TotalPrice { get; set; }
    public DateOnly? EventDate { get; set; }
    public DateOnly? StartRentDate { get; set; }
    public DateOnly? EndRentDate { get; set; }
    public OrderType OrderType { get; set; }
    public PaymentStatus PaymentState { get; set; }
    public Guid CustomerId { get; set; }
    public Guid OrderManagerId { get; set; }
    public Guid TechOrderLeadId { get; set; }
}
