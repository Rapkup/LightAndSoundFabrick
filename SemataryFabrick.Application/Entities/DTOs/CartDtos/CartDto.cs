using SemataryFabrick.Domain.Entities.Models.CartModels;

namespace SemataryFabrick.Application.Entities.DTOs.CartDtos;
public record CartDto
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid CustomerId { get; set; }
    public DateOnly? EventDate { get; set; }

    public static CartDto CartToCartDto(Cart cart)
    {
        return new CartDto
        {
            Id = cart.Id,
            TotalPrice = cart.TotalPrice,
            CustomerId = cart.CustomerId,
            EventDate = cart.EventDate
        };
    }
}
