using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodCourtSystem.Models
{
    public class AccountFundModel
    {
        public string ID { get; set; }
        public string AccountName { get; set; }
        public int Balance { get; set; }
    }

    public class FundDbContext : DbContext 
    {
        public DbSet<AccountFundModel> accountFunds;
        public static Guid guid = new Guid();
        public void Add(AccountFundModel item)
        {
            string id = DateTime.Now.Ticks.ToString();
            item.ID = id;
            accountFunds.Add(item);
        }
    }

}