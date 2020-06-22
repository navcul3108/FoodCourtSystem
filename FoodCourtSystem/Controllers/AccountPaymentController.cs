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
    [Authorize]
    public class AccountPaymentController : Controller
    {
        FundDbContext db = new FundDbContext();
        
        // GET: AccountPayment
        public ActionResult ViewFund(string userName)
        {
            if (userName == null)
                return View("Error");
            var f = db.accountFunds.Where(fund => fund.UserName == userName).SingleOrDefault();
            if (f == null)
            {
                f = new AccountFundModel
                {
                    ID = DateTime.Now.Ticks.ToString(),
                    UserName = userName,
                    Balance = 0
                };
                db.accountFunds.Add(f);
                db.SaveChanges();
            }
            return View(f);
        }

        public ActionResult ChargeAccount(string userName)
        {
            ViewBag.UserName = userName;
            return View();
        }

        [HttpPost]
        public ActionResult ChargeAccount(ChargeAccountViewModel model)
        {
            if(!ModelState.IsValid)
                return View();
            else
            {
                var fund = db.accountFunds.Where(f => f.UserName == model.UserName).SingleOrDefault();
                if (fund == null)
                    return View("Error");
                fund.Balance += model.Amount;
                db.accountFunds.AddOrUpdate(fund);
                db.SaveChanges();
                return RedirectToAction("ViewFund", "AccountPayment",new { userName = model.UserName });
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}