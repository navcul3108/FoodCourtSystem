using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodCourtSystem.Models
{
    public class CartItemViewModel
    {
        public ProductModel product { get; set; }
        public int Quantity { get; set; }
    } 
}