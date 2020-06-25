using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;   

namespace FoodCourt.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Blank Name")]
        public string nameProduct { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Blank Category")]
        public string category { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Please choose image to upload")]
        public string imagePath { get; set; }

        [Display(Name = "FoodID")]
        public string number { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Blank Price")]
        public int price { get; set; }

        [Display(Name = "Vendor")]
        public string vendorName { get; set; }
    }
}