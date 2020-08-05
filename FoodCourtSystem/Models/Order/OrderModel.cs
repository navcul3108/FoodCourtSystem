using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodCourtSystem.Models
{
     public enum OrderStatus { 
        WAITING=0,
        READY,
        DELIVERED,
        CANCELED
    }
    public class OrderItem
    {
        [Key]
        public string ID { get; set;}
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderModel
    {
        [Required]
        public string ID { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedTime { get; set; }
        [Required]
        public ICollection<OrderItem> Items { get; set; }
        public OrderStatus Status { get; set; }
        public int Money { get; set; }
        public string Owner { get; set; }
    }

    public class OrderContext: DbContext
    {
        public DbSet<OrderModel> orders { get; set; }

        public OrderContext(): base("OrderContext")
        {

        }
    }
}