﻿using SemataryFabrick.Domain.Entities.Models.Customers;

namespace SemataryFabrick.Domain.Entities.Models.CartModels;

public class Cart
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; }
    public IEnumerable<CartItem> Items { get; set; }
}