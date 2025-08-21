using System;
using System.Collections.Generic;

namespace Infosys.QuickKart.DAL.Models;

public partial class Product
{
    public string ProductId { get; set; }

    public string ProductName { get; set; }

    public byte? CategoryId { get; set; }

    public decimal Price { get; set; }

    public int QuantityAvailable { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category Category { get; set; }

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

    public virtual ICollection<Rating> RatingProductNameNavigations { get; set; } = new List<Rating>();

    public virtual ICollection<Rating> RatingProducts { get; set; } = new List<Rating>();
}
