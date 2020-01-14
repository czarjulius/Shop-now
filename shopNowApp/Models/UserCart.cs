using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using shopNowDataAccess;


namespace shopNowApp.Models
{
    public class UserCart
    {
        public USER user { get; set; }
        public List<CART> cart { get; set; }

        public UserCart()
        {
            user = new USER();
            cart = new List<CART>();
        }
    }
}