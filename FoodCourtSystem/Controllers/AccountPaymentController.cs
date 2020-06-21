using FoodCourtSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
        public ActionResult ViewFund(string userName)
        {
            if (userName == null)
                return View("Error");
            if(db.accountFunds.Count()==0)
            {
                AccountFundModel new_fund = new AccountFundModel
                {
                    ID = "",
                    AccountName = userName,
                    Balance = 0
                };
                db.Add(new_fund);
                return View(new_fund);
            }
            else
            {
                var f = db.accountFunds.Where(fund => fund.AccountName == userName).SingleOrDefault();
                if (f == null)
                {
                    f = new AccountFundModel
                    {
                        ID = "",
                        AccountName = userName,
                        Balance = 0
                    };
                    db.Add(f);
                }
                return View(f);
            }   
        }

        public ActionResult ChargeAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChargeAccount(AccountFundModel model, int amount)
        {
            if (amount > 0)
            {
                var f = db.accountFunds.Where(fund => fund.AccountName == userName).SingleOrDefault();
                if (f == null)
                {
                    return View("Error");
                }
                f.Balance = f.Balance + amount;
                db.accountFunds.AddOrUpdate(f);
                db.SaveChanges();
                return RedirectToAction("ViewFund", "AccountPayment");
            }
            else
                return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}