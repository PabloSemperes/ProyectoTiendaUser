using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTT_Shop.Models.Entities
{
    public class Cart
    {
        public Product product { get; set; }
        public int quantity { get; set; }
        public decimal totalPrice { get; set; }
    }
}