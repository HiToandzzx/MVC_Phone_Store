using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSMobile.Models
{
    public class CartItem
    {
        public string PicPhone { get; set; }
        public string PhonesID { get; set; }
        public string PhonesName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}