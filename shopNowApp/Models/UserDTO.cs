using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shopNowApp.Models
{
    public class UserDTO
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> createdOn { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public Nullable<bool> isAdmin { get; set; }
        public Nullable<int> cartId { get; set; }
    }
}