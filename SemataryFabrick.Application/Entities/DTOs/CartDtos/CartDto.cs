using SemataryFabrick.Domain.Entities.Models.CartModels;

namespace SemataryFabrick.Application.Entities.DTOs.CartDtos;
public record CartDto
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid CustomerId { get; set; }
    public DateOnly? EventDate { get; set; }

    public static CartDto FromEntity(Cart cart)
    {
        return new CartDto
        {
            Id = cart.Id,
            TotalPrice = cart.TotalPrice,
            CustomerId = cart.CustomerId,
            EventDate = cart.EventDate
        };
    }

    public Cart ToEntity()
    {
        return new Cart
        {
            Id = this.Id,
            TotalPrice = this.TotalPrice,
            CustomerId = this.CustomerId,
            EventDate = this.EventDate
        };
    }
}
