using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodCourtSystem.Models;

namespace FoodCourtSystem.Controllers
{
  
    public class RecordController : Controller
    {
        public RecordContext db = new RecordContext();
        // GET: Record
        public ActionResult ViewVendorRecord()
        {
            return View();
        }

        public ActionResult ViewEntireRecord()
        {
            return View();
        }
    }
}