using System;
using System.Collections.Generic;

namespace Infosys.QuickKart.DAL.Models;

public partial class ProductDetail
{
    public string ProductId { get; set; }

    public string ProductName { get; set; }

    public string Description { get; set; }

    public string Seller { get; set; }

    public int Ratings { get; set; }

    public virtual Product Product { get; set; }
}
