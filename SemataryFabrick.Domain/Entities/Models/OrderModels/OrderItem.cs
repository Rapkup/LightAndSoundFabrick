using SemataryFabrick.Domain.Entities.Models.Items;
using SemataryFabrick.Domain.Entities.Models.Order.Order;

namespace SemataryFabrick.Domain.Entities.Models.Order;
public class OrderItem
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid OrderBaseId { get; set; }
    public Guid ProductId { get; set; }

    public OrderBase OrderBase { get; set; }
    public Item Product { get; set; }
}
