using SemataryFabrick.Domain.Entities.Models.Items;

namespace SemataryFabrick.Domain.Entities.Models.CartModels;

public class CartItem
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }

    public Cart Cart { get; set; }
    public Item Product { get; set; }
}