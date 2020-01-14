using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shopNowApp.Models
{
    public class ProductDTO
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string productImage { get; set; }
        public string catName { get; set; }
        public Nullable<decimal> productPrice { get; set; }
        public Nullable<bool> isAvailable { get; set; }
        public Nullable<System.DateTime> createdOn { get; set; }
        public string productDescription { get; set; }
    }
}