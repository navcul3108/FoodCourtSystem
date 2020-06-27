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
        FoodCourtSystem.Models.ProductContext db = new Models.ProductContext();
        public MenuController() { }
        // GET: Menu
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        
        //
        // GET: 
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include ="Name,Price,ImageName")] ProductModel p)
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