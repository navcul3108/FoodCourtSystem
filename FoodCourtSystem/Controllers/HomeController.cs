using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodCourtSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [SecurityRole("Cook")]
        public ActionResult ViewOrder()
        {
            return View();
        }

        [SecurityRole("VendorOwner")]
        public ActionResult ViewVendorOwnerHomePage(string vendorID)
        {
            return View();
        }

        [SecurityRole("Admin")]
        public ActionResult ViewAdminHomePage()
        {
            return View();
        }
    }
}