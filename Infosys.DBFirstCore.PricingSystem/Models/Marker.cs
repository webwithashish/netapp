using System;
using System.Collections.Generic;

namespace Infosys.DBFirstCore.DataAccessLayer.Models;

public partial class Marker
{
    public int MarkerId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<MarkerPrice> MarkerPrices { get; set; } = new List<MarkerPrice>();

    public virtual ICollection<ProductDiff> ProductDiffs { get; set; } = new List<ProductDiff>();
}
