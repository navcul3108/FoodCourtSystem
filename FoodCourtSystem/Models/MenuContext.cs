using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodCourtSystem.Models
{
    public class MenuContext: DbContext
    {
        public System.Data.Entity.DbSet<ProductModel> Products { get; set; }
        public System.Data.Entity.DbSet<CategoryModel> Categories { get; set; }
        public DbSet<CartModel> Carts { get; set; }
        public DbSet<CartItemModel> CartItems { get; set; }

        public MenuContext(): base("MenuContext")
        { }

    }
}