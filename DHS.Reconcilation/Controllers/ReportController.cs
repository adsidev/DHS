using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaid.Reconcilation.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult ManageGrantProjectReport()
        {
            return View();
        }
    }
}