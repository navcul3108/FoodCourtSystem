using FoodCourtSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FoodCourtSystem.Controllers
{
    [SecurityRole("Cook")]
    public class OrderController : Controller
    {
        public OrderContext db = new OrderContext();
        // GET: Order

        public ActionResult ViewOrderList()
        {
            return View(db.orders.ToList());
        }

        public ActionResult ConformOrder(string orderID)
        {
            try
            {
                var conformedOrder = db.orders.First(order => order.ID == orderID);
                if (conformedOrder.Status != OrderStatus.READY)
                    return View("Error");
                else
                {
                    conformedOrder.Status = OrderStatus.READY;
                    db.orders.AddOrUpdate(conformedOrder);
                    db.SaveChanges();
                    return new EmptyResult();
                }
            }
            catch (ArgumentNullException)
            {
                return View("Error");
            }
        }
    }
}