using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infosys.QuickKart.DAL.Models
{
    public class CartProduct
    {
        [Key]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public short Quantity { get; set; }
        public int QuantityAvailable { get; set; }

    }
}
