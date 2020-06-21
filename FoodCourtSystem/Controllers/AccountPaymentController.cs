using FoodCourtSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser;

namespace FoodCourtSystem.Controllers
{
    public class AccountPaymentController : Controller
    {
        FundDbContext db = new FundDbContext();
        
        // GET: AccountPayment
        public ActionResult ViewFund(string accountName)
        {
            if (accountName == null)
                return View("Error");
            var f = db.accountFunds.Where(fund => fund.AccountName == accountName).SingleOrDefault();
            if(f==null)
            {
                f = new AccountFundModel
                {
                    ID = "",
                    AccountName = accountName,
                    Balance = 0
                };
                db.Add(f);
            }
            return View(f);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}