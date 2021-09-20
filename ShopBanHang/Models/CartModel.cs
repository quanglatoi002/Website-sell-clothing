using ShopBanHang.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBanHang.Models
{
    public class CartModel
    {
        public Product product { get; set; }
        
        public int Quantity { get; set; }
    }
}