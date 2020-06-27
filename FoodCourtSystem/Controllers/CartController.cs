using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using FoodCourtSystem.Models;

namespace FoodCourtSystem.Controllers
{
    public class CartController : Controller
    {
        CartDbContext db = new CartDbContext();
        // GET: Cart
        public ActionResult ViewCart()
        {
            return View();
        }
        public ActionResult AddToCart(string username,CartItemModel item)
        {
            return View();
        }
    }
}