using DHS.Reconcilation.Models;
using DHSEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;



namespace DHS.Reconcilation.Controllers
{
    public class AuditReportsController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service

        public AuditReportsController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: AuditReports
        public ActionResult ManageCRReports()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            string PageName = "CRReports";
            AuditResponse auditResponse = new AuditResponse();
            auditResponse.rolePermissionEntity = Common.PagePermissions(PageName);
            return View(auditResponse);
        }

        [HttpGet]
        public async Task<ActionResult> ManageCRSummaryReport()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");            
                       
            AuditResponse auditResponse = new AuditResponse();

            string url = strBaseURL + "AuditReport/GetCRSummaryReport";
            client.BaseAddress = new Uri(url);
            AuditRequest auditRequest = new AuditRequest();
            auditRequest.FacilityId = 0;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, auditRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                auditResponse = JsonConvert.DeserializeObject<AuditResponse>(responseData);
                if (auditResponse.Message == string.Empty && auditResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "CRReports";
                    auditResponse.rolePermissionEntity = Common.PagePermissions(PageName); bool PageHasPermissionsOrNot = CheckPagePermissionHeadders.PageHasPermission(PageName);
                    if (!PageHasPermissionsOrNot)
                        return RedirectToAction("Index", new { ErrorMsg = "You do not have access to this activity. Please contact your administrator." });
                    ViewData["Facility"] = "0";
                    return View(auditResponse);
                }
                else
                {
                    TempData["LoginFailure"] = auditResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ManageCRSummaryReport(AuditResponse auditResponse)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (Request.Form["Facility"] == "")
                ViewData["Facility"] = "0";
            else
                ViewData["Facility"] = Convert.ToInt32(Request.Form["Facility"]);
            if (Request.Form["FiscalPeriod"] == "")
                ViewData["Payment"] = "0";
            else
                ViewData["Payment"] = Convert.ToInt32(Request.Form["FiscalPeriod"]);

            AuditRequest auditRequest = new AuditRequest();
            auditRequest.FacilityId = Convert.ToInt32(ViewData["Facility"]);

            string url = strBaseURL + "AuditReport/GetCRSummaryReport";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, auditRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                auditResponse = JsonConvert.DeserializeObject<AuditResponse>(responseData);
                if (auditResponse.Message == string.Empty && auditResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "CRReports";
                    auditResponse.rolePermissionEntity = Common.PagePermissions(PageName); bool PageHasPermissionsOrNot = CheckPagePermissionHeadders.PageHasPermission(PageName);
                    if (!PageHasPermissionsOrNot)
                        return RedirectToAction("Index", new { ErrorMsg = "You do not have access to this activity. Please contact your administrator." });
                   
                    return View(auditResponse);
                }
                else
                {
                    TempData["LoginFailure"] = auditResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<ActionResult> ManageCRPaymentAnalysis()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            AuditResponse auditResponse = new AuditResponse();

            string url = strBaseURL + "AuditReport/GetCRSummaryReport";
            client.BaseAddress = new Uri(url);
            AuditRequest auditRequest = new AuditRequest();
            auditRequest.FacilityId = 0;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, auditRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                auditResponse = JsonConvert.DeserializeObject<AuditResponse>(responseData);
                if (auditResponse.Message == string.Empty && auditResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "CRReports";
                    auditResponse.rolePermissionEntity = Common.PagePermissions(PageName); bool PageHasPermissionsOrNot = CheckPagePermissionHeadders.PageHasPermission(PageName);
                    if (!PageHasPermissionsOrNot)
                        return RedirectToAction("Index", new { ErrorMsg = "You do not have access to this activity. Please contact your administrator." });
                    ViewData["Facility"] = "0";
                    return View(auditResponse);
                }
                else
                {
                    TempData["LoginFailure"] = auditResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ManageCRPaymentAnalysis(AuditResponse auditResponse)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (Request.Form["Facility"] == "")
                ViewData["Facility"] = "0";
            else
                ViewData["Facility"] = Convert.ToInt32(Request.Form["Facility"]);
            if (Request.Form["FiscalPeriod"] == "")
                ViewData["Payment"] = "0";
            else
                ViewData["Payment"] = Convert.ToInt32(Request.Form["FiscalPeriod"]);

            AuditRequest auditRequest = new AuditRequest();
            auditRequest.FacilityId = Convert.ToInt32(ViewData["Facility"]);

            string url = strBaseURL + "AuditReport/GetCRPaymentAnalysis";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, auditRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                auditResponse = JsonConvert.DeserializeObject<AuditResponse>(responseData);
                if (auditResponse.Message == string.Empty && auditResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "CRReports";
                    auditResponse.rolePermissionEntity = Common.PagePermissions(PageName); bool PageHasPermissionsOrNot = CheckPagePermissionHeadders.PageHasPermission(PageName);
                    if (!PageHasPermissionsOrNot)
                        return RedirectToAction("Index", new { ErrorMsg = "You do not have access to this activity. Please contact your administrator." });

                    return View(auditResponse);
                }
                else
                {
                    TempData["LoginFailure"] = auditResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }


        [HttpGet]
        public async Task<ActionResult> ManageSettlementPaymentReport()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            AuditResponse auditResponse = new AuditResponse();

            string url = strBaseURL + "AuditReport/GetCRSummaryReport";
            client.BaseAddress = new Uri(url);
            AuditRequest auditRequest = new AuditRequest();
            auditRequest.FacilityId = 0;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, auditRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                auditResponse = JsonConvert.DeserializeObject<AuditResponse>(responseData);
                if (auditResponse.Message == string.Empty && auditResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "CRReports";
                    auditResponse.rolePermissionEntity = Common.PagePermissions(PageName); bool PageHasPermissionsOrNot = CheckPagePermissionHeadders.PageHasPermission(PageName);
                    if (!PageHasPermissionsOrNot)
                        return RedirectToAction("Index", new { ErrorMsg = "You do not have access to this activity. Please contact your administrator." });
                    ViewData["Facility"] = "0";
                    return View(auditResponse);
                }
                else
                {
                    TempData["LoginFailure"] = auditResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ManageSettlementPaymentReport(AuditResponse auditResponse)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (Request.Form["Facility"] == "")
                ViewData["Facility"] = "0";
            else
                ViewData["Facility"] = Convert.ToInt32(Request.Form["Facility"]);
            if (Request.Form["FiscalYear"] == "")
                ViewData["FiscalYear"] = "0";
            else
                ViewData["FiscalYear"] = Convert.ToInt32(Request.Form["FiscalYear"]);

            AuditRequest auditRequest = new AuditRequest();
            auditRequest.FacilityId = Convert.ToInt32(ViewData["Facility"]);
            auditRequest.FiscalYearId = Convert.ToInt32(ViewData["FiscalYear"]);

            string url = strBaseURL + "AuditReport/GetSettlementPaymentReport";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, auditRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                auditResponse = JsonConvert.DeserializeObject<AuditResponse>(responseData);
                if (auditResponse.Message == string.Empty && auditResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "CRReports";
                    auditResponse.rolePermissionEntity = Common.PagePermissions(PageName); bool PageHasPermissionsOrNot = CheckPagePermissionHeadders.PageHasPermission(PageName);
                    if (!PageHasPermissionsOrNot)
                        return RedirectToAction("Index", new { ErrorMsg = "You do not have access to this activity. Please contact your administrator." });

                    return View(auditResponse);
                }
                else
                {
                    TempData["LoginFailure"] = auditResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }
    }
}