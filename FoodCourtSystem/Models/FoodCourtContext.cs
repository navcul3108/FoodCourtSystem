using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FoodCourt.Models;

namespace FoodCourt.Models
{
    public class FoodCourtContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<InternalAccount> Accounts { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}