using System;
using System.Collections.Generic;

namespace Infosys.QuickKart.DAL.Models;

public partial class Rating
{
    public string EmailId { get; set; }

    public string ProductId { get; set; }

    public string ProductName { get; set; }

    public byte ReviewRating { get; set; }

    public string ReviewComments { get; set; }

    public virtual User Email { get; set; }

    public virtual Product Product { get; set; }

    public virtual Product ProductNameNavigation { get; set; }
}
