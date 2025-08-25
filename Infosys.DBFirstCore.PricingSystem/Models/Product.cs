using System;
using System.Collections.Generic;

namespace Infosys.DBFirstCore.DataAccessLayer.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal BasePrice { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ProductDiff> ProductDiffs { get; set; } = new List<ProductDiff>();
}
