using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace FoodCourtSystem.Models
{
    public class CartItemModel
    {
        public string ID { get; set; }
        public int Quantity { get; set; }
        public int TotalMoney { get; set;}
        public string ProductID { get; set; }
        [ForeignKey("ProductID")]
        public ProductModel Product { get; set; }
        public CartModel Cart { get; set; }
        public string CartID { get; set; }
        public void UpdateTotalMoney()
        {
            TotalMoney = Product.UnitPrice * Quantity;
        }


    }
    public class CartModel
    {
        [Key]
        public string ID { get; set; }
        public string OwnerName { get; set; }
        public virtual ICollection<CartItemModel> Items { get; set; }
        public double VAT { get;}
        public int TotalMoney { get; set; }
        public CartModel()
        {
            VAT = 0.1;
            TotalMoney = 0;
            Items = new HashSet<CartItemModel>();
        }
        public void UpdateTotalMoney()
        {
            double price = 0;
            foreach (CartItemModel item in Items)
                price += item.TotalMoney;
            TotalMoney = (int)((double)price * (1 + VAT));
        }
    }

}