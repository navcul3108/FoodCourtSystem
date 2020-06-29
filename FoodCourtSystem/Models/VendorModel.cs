using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodCourtSystem.Models
{
    public class VendorModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class VendorContext: DbContext
    {
        public DbSet<VendorModel> vendors;

        public VendorContext(): base("VendorContext")
        {

        }
    }
}