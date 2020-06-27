using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodCourtSystem.Models
{
    enum OrderStatus { 
        WAITING=0,
        READY,
        DELIVERED,
        CANCELED
    }

    public class OrderModel
    {
        [Required]
        public string ID { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedTime { get; set; }
        [Required]
        public ICollection<ProductModel> products { get; set; }
        public bool IsDelivered { get; set; }
        public int Money { get; set }
    }

    public class OrderViewModel
    {

    }
}