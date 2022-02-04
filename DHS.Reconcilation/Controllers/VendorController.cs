using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaid.Reconcilation.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult ManageVendors()
        {
            return View();
        }

        public ActionResult CreateVendor()
        {
            return View();
        }
    }
}