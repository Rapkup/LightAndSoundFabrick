using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.Order;

namespace SemataryFabrick.Domain.Entities.Models.Items;
public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } // JSON
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
    public Guid? DiscountId { get; set; }
    public Guid InventoryId { get; set; }
    public Guid SubCategoryId { get; set; }

    public OrderItem OrderItem { get; set; }
    public Discount Discount { get; set; }
    public ItemInventory Inventory { get; set; }
    public SubCategory SubCategory { get; set; }
    public IEnumerable<CartItem> CartItems { get; set; }
}
