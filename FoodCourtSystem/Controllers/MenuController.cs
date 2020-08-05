using FoodCourtSystem.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.EnterpriseServices;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FoodCourtSystem.Controllers
{
    public class MenuController : Controller
    {
        MenuContext db = new Models.MenuContext();
        public MenuController() { }
        // GET: Menu
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
                return View(db.Products.ToList());
            else
                return RedirectToAction("Login", "Account");
        }

        public async Task<ActionResult> Search(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var items = db.Products.Where(item => item.Name.Contains(searchString));
                return RedirectToAction("Index",await items.ToListAsync());
            }
            return RedirectToAction("Index");
        }
        


        [HttpPost]
        public ActionResult Create([Bind(Include ="ID,Name,UnitPrice,ImageName,Description")] ProductModel p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(p);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                    }
                }
            }
            return View();
        }

        public ActionResult Edit()
        {
            if (User.IsInRole("VendorOwner"))
            {
                return View();
            }
            else
                return View("NotPermissionToAccess");
        }

        [HttpPost]
        [SecurityRole("VendorOwner")]
        public ActionResult Edit([Bind(Include ="ID,Name,UnitPrice,ImageName,Description")] ProductModel product)
        {
            if (User.IsInRole("VendorOwner"))
            {
                if (ModelState.IsValid)
                {
                    db.Products.AddOrUpdate(product);
                    db.SaveChanges();
                    return new EmptyResult();
                }
                return View("Error");
            }
            else
                return View("NotPermissionToAccess");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}