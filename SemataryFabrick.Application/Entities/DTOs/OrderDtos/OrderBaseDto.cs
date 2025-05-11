using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Application.Entities.DTOs.OrderDtos;
public record OrderBaseDto
{
    public Guid Id { get; set; }
    public string EventAddress { get; set; }
    public decimal TotalPrice { get; set; }
    public DateOnly? EventDate { get; set; }
    public DateOnly? StartRentDate { get; set; }
    public DateOnly? EndRentDate { get; set; }
    public OrderState OrderState { get; set; }
    public OrderType OrderType { get; set; }
    public PaymentStatus PaymentState { get; set; }
    public Guid CustomerId { get; set; }
    public Guid OrderManagerId { get; set; }
    public Guid TechOrderLeadId { get; set; }

    public static OrderBaseDto FromEntity(OrderBase entity)
    {
        return new OrderBaseDto
        {
            Id = entity.Id,
            EventAddress = entity.EventAddress,
            TotalPrice = entity.TotalPrice,
            EventDate = entity.EventDate,
            StartRentDate = entity.StartRentDate,
            EndRentDate = entity.EndRentDate,
            OrderState = entity.OrderState,
            OrderType = entity.OrderType,
            PaymentState = entity.PaymentState,
            CustomerId = entity.CustomerId,
            OrderManagerId = entity.OrderManagerId,
            TechOrderLeadId = entity.TechOrderLeadId
        };
    }
    public OrderBase ToEntity()
    {
        return new OrderBase
        {
            Id = this.Id,
            EventAddress = this.EventAddress,
            TotalPrice = this.TotalPrice,
            EventDate = this.EventDate,
            StartRentDate = this.StartRentDate,
            EndRentDate = this.EndRentDate,
            OrderState = this.OrderState,
            OrderType = this.OrderType,
            PaymentState = this.PaymentState,
            CustomerId = this.CustomerId,
            OrderManagerId = this.OrderManagerId,
            TechOrderLeadId = this.TechOrderLeadId
        };
    }
}
