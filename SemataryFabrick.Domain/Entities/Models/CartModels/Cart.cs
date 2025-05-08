using SemataryFabrick.Domain.Entities.Models.UserModels;

namespace SemataryFabrick.Domain.Entities.Models.CartModels;

public class Cart
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public DateOnly? EventDate { get; set; }
    public Guid CustomerId { get; set; }

    public ApplicationUser Customer { get; set; }
    public IEnumerable<CartItem> Items { get; set; }
}