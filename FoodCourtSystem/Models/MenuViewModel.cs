using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodCourtSystem.Models
{
    public class ProductModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public string ID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }
        [Range(5000, 100000, ErrorMessage ="Giá tiền trong khoảng 5000-100000")]
        public double UnitPrice { get; set; }
        [Required]
        [StringLength(40, MinimumLength =8)]
        public string ImageName { set; get; }
        [Required]
        [StringLength(200)]
        public string Desciption { get; set; }

    }

    public class CategoryModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public string ID { get; set; }
        public string Name { get; set; }
        [ForeignKey("ID")]
        public virtual ICollection<ProductModel> Products { get; set; }
    }

    public class MenuViewModel
    {
        public virtual ICollection<CategoryModel> Categories { get; set; }
    }

    public class ProductContext: DbContext
    {
        public ProductContext():base("ProductContext")
        {

        }
        public System.Data.Entity.DbSet<ProductModel> Products { get; set; }
        public System.Data.Entity.DbSet<CategoryModel> Categories { get; set; }
    }
    

}