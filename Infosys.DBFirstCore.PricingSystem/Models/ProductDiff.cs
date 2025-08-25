using System;
using System.Collections.Generic;

namespace Infosys.DBFirstCore.DataAccessLayer.Models;

public partial class ProductDiff
{
    public int ProductDiffId { get; set; }

    public int ProductId { get; set; }

    public int MarkerId { get; set; }

    public decimal DiffValue { get; set; }

    public bool IsActive { get; set; }

    public virtual Marker Marker { get; set; }

    public virtual Product Product { get; set; }
}
