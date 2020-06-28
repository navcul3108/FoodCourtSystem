using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodCourtSystem.Models
{
    public class CartItemModel
    {

        public int Quantity { get; set; }
        public int TotalMoney { get; set;}
        public ProductModel Product { get; set; }
        private int getTotalMoney()
        {
            return Product.UnitPrice * Quantity;
        }
    }
    public class CartModel
    {
        public string OwnerName { get; set; }
        public virtual ICollection<CartItemModel> items { get; set; }
        public double VAT { get;}
        public int TotalMoney { get; set; }
        public CartModel()
        {
            VAT = 0.1;
            TotalMoney = 0;
        }
        public void CalculatePrice()
        {
            double price = 0;
            foreach (CartItemModel item in items)
                price += item.TotalMoney;
            TotalMoney = (int)((double)price * (1 + VAT));
        }
    }

    public class CartDbContext: DbContext
    {
        public CartDbContext(): base("CartContext")
        { }
        public DbSet<CartModel> carts;
        
    }
}