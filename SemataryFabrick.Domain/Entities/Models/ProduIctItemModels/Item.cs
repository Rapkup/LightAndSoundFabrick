using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.CartModels;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Domain.Entities.Models.ProduIctItemModels;

namespace SemataryFabrick.Domain.Entities.Models.Items;
public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageName { get; set; }
    public string Description { get; set; } // JSON
    public decimal Price { get; set; }
    public ProductState Status { get; set; }
    public Guid InventoryId { get; set; }
    public Guid SubCategoryId { get; set; }

    public ItemInventory Inventory { get; set; }
    public SubCategory SubCategory { get; set; }
    public IEnumerable<CartItem> CartItems { get; set; }
    public IEnumerable<OrderItem> OrderItems { get; set; }
}
