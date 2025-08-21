using System;
using System.Collections.Generic;

namespace Infosys.QuickKart.DAL.Models;

public partial class Cart
{
    public string ProductId { get; set; }

    public string EmailId { get; set; }

    public short Quantity { get; set; }

    public virtual User Email { get; set; }

    public virtual Product Product { get; set; }
}
