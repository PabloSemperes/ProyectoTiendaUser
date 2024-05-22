using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTT_Shop.Models.Entities
{
    public class Product
    {
        public int idProduct { get; set; }
        public int stock { get; set; }
        public bool enabled { get; set; }
        public List<ProductDescription> descriptions { get; set; }
        public List<ProductRate> rates { get; set; }
    }
}