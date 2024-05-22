using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTT_Shop.Models.Entities
{
    public class ProductRate
    {
        public int idProduct { get; set; }
        public int idRate { get; set; }
        public decimal price { get; set; }
    }
}