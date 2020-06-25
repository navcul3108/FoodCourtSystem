using FoodCourtSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodCourtSystem.Controllers
{
    public class MenuController : Controller
    {
        FoodCourtSystem.Models.ProductDbContext db = new Models.ProductDbContext();
        public MenuController() { }
        // GET: Menu
        public ActionResult Index()
        {
            var products = from p in db.Products
                           orderby p.ID
                           select p;
            return View(products);
        }
        
        //
        // GET: 
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include ="Name,Price,ImageName")] Product p)
        {
            if(ModelState.IsValid)
            {
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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