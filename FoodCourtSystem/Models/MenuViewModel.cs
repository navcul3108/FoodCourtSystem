using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace FoodCourtSystem.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImageName { set; get; }

    }
    public class ProductDbContext: DbContext
    {
        public ProductDbContext():base("ProductContext")
        {

        }
        public System.Data.Entity.DbSet<Product> Products { get; set; }
    }
    public class ProductViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImageName { set; get; }
    }
}