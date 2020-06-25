using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Globalization;
using System.Security.Claims;
using FoodCourt.Models;

namespace FoodCourt.Controllers
{
    public class InternalAccountsController : Controller
    {
        private FoodCourtContext db = new FoodCourtContext();

        // GET: InternalAccounts
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        // GET: InternalAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InternalAccount internalAccount = db.Accounts.Find(id);
            if (internalAccount == null)
            {
                return HttpNotFound();
            }
            return View(internalAccount);
        }

        // GET: InternalAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InternalAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,userName,password,emailAddress,accountRole")] InternalAccount internalAccount)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(internalAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(internalAccount);
        }

        // GET: InternalAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InternalAccount internalAccount = db.Accounts.Find(id);
            if (internalAccount == null)
            {
                return HttpNotFound();
            }
            return View(internalAccount);
        }

        // POST: InternalAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,userName,password,emailAddress,accountRole")] InternalAccount internalAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(internalAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(internalAccount);
        }

        // GET: InternalAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InternalAccount internalAccount = db.Accounts.Find(id);
            if (internalAccount == null)
            {
                return HttpNotFound();
            }
            return View(internalAccount);
        }

        // POST: InternalAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InternalAccount internalAccount = db.Accounts.Find(id);
            db.Accounts.Remove(internalAccount);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(InternalAccount model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Product", 0);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
