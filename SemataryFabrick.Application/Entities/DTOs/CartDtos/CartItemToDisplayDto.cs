namespace SemataryFabrick.Application.Entities.DTOs.CartDtos;
public class CartItemToDisplayDto
{
    public string ImagePath { get; set; } = "images/items/";
    public string ItemName { get; set; }
    public decimal Price { get; set; }
    public int Discount { get; set; }
    public int Quantity { get; set; }
    public DateOnly? StartRentDate { get; set; }
    public DateOnly? EndRentDate { get; set; }
    public Guid CartItemId { get; set; }
    public Guid Discountid { get; set; }
}