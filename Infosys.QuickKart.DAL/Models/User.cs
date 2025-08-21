using System;
using System.Collections.Generic;

namespace Infosys.QuickKart.DAL.Models;

public partial class User
{
    public string EmailId { get; set; }

    public string UserPassword { get; set; }

    public byte? RoleId { get; set; }

    public string Gender { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string Address { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual Role Role { get; set; }
}
