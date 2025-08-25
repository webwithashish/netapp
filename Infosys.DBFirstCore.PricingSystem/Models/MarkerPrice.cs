using System;
using System.Collections.Generic;

namespace Infosys.DBFirstCore.DataAccessLayer.Models;

public partial class MarkerPrice
{
    public int MarkerPriceId { get; set; }

    public int MarkerId { get; set; }

    public decimal Value { get; set; }

    public DateOnly ValidFrom { get; set; }

    public DateOnly? ValidTo { get; set; }

    public bool IsActive { get; set; }

    public virtual Marker Marker { get; set; }
}
