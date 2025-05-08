using SemataryFabrick.Domain.Entities.Models.Items;

namespace SemataryFabrick.Domain.Entities.Models.OrderModels;
public class OrderItem
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid OrderBaseId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? DiscountId { get; set; }


    public OrderBase OrderBase { get; set; }
    public Item Product { get; set; }
    public Discount Discount { get; set; }
}
