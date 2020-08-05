using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodCourtSystem.Models
{
    public class RecordItemModel
    {
        [Key]
        public string ID { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
        public int Revenue { get; set; }
        public RecordModel Record;
    }
    public class RecordModel
    { 
        [Key]
        public string ID { get; set; }
        public VendorModel Vendor { get; set; }
        public virtual ICollection<RecordItemModel> Items { get; set; }
        [DisplayName("Doanh thu")]
        public int TotalRevenue { get; set; }
        [DisplayName("Thời gian")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDay { get; set; }
    }

    public class RecordContext : DbContext
    {
        public DbSet<RecordModel> Records { get; set; }

        public RecordContext(): base("RecordContext")
        {
            
        }
    }

}