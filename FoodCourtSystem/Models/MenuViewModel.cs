using System.Collections.Generic;
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
        public int UnitPrice { get; set; }
        [Required]
        [StringLength(40)]
        public string ImageName { set; get; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        public CategoryModel Category { get; set; }
    }

    public class CategoryModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public string ID { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

    }

    public class MenuViewModel
    {
        public virtual ICollection<CategoryModel> Categories { get; set; }
    }
    

}