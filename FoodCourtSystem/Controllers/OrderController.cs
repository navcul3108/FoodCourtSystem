using FoodCourtSystem.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FoodCourtSystem.Controllers
{
    public class OrderController : Controller
    {
        public OrderContext db = new OrderContext();
        // GET: Order
        [Authorize(Roles = "Regular, Guest")]
        public ActionResult ViewOrder()
        {
            return View();
        }
        [SecurityRole("Cook")]
        public ActionResult ViewOrderQueue()
        {
            return View();
        }
    }
}