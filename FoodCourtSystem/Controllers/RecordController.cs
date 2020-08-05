using System;
using System.Collections.Generic;
using System.EnterpriseServices;
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
        [SecurityRole("VendorOwner")]
        public ActionResult ViewVendorRecord(string vendorID)
        {
            var recordItems = db.Records.Where(record => record.Vendor.ID == vendorID);
            ViewBag.VendorName = db.Records.First(record => record.Vendor.ID == vendorID).Vendor.Name;
            return View(recordItems);
        }

        [SecurityRole("Admin")]
        public ActionResult ViewEntireRecord()
        {
            return View();
        }
    }
}