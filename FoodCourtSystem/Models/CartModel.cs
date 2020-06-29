using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodCourtSystem.Models
{
    public class CartItemModel
    {
        public string ID { get; set; }
        public int Quantity { get; set; }
        public int TotalMoney { get; set;}
        [ForeignKey("ID")]
        public virtual ProductModel Product { get; set; }
        public void UpdateTotalMoney()
        {
            TotalMoney = Product.UnitPrice * Quantity;
        }
        [ForeignKey("ID")]
        public CartModel Cart { get; set;}
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
            Items = new List<CartItemModel>();
            VAT = 0.1;
            TotalMoney = 0;
        }
        public void UpdateTotalMoney()
        {
            double price = 0;
            foreach (CartItemModel item in Items)
                price += item.TotalMoney;
            TotalMoney = (int)((double)price * (1 + VAT));
        }
    }

    public class CartContext: DbContext
    {
        public CartContext(): base("CartContext")
        { }
        public DbSet<CartModel> Carts { get; set; }

    }
}