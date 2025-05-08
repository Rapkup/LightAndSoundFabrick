using SemataryFabrick.Domain.Entities.Models.Items;

namespace SemataryFabrick.Domain.Entities.Models.CartModels;

public class CartItem
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public DateOnly? StartRentDate { get; set; }
    public DateOnly? EndRentDate { get; set; }
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? DiscountId { get; set; }

    public Cart Cart { get; set; }
    public Item Product { get; set; }
    public Discount Discount { get; set; }
}