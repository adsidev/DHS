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
    public class ReportController : Controller
    {
         HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service

        public ReportController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        public ActionResult ManageReports()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ManageGrantProjectReport()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            ReportResponse reportResponse = new ReportResponse();
            string url = strBaseURL + "Report/GetGrantProjectReport";
            // string url = strBaseURL + "AuditReport/GetCRSummaryReport";
            client.BaseAddress = new Uri(url);
            ReportRequest reportRequest = new ReportRequest();
            reportRequest.FiscalYearId = 0;
            reportRequest.ProjectId = 0;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, reportRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                reportResponse = JsonConvert.DeserializeObject<ReportResponse>(responseData);
                if (reportResponse.Message == string.Empty && reportResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Reports";
                    reportResponse.rolePermissionEntity = Common.PagePermissions(PageName); bool PageHasPermissionsOrNot = CheckPagePermissionHeadders.PageHasPermission(PageName);
                    if (!PageHasPermissionsOrNot)
                        return RedirectToAction("Index", new { ErrorMsg = "You do not have access to this activity. Please contact your administrator." });
                    ViewData["Project"] = "0";
                    ViewData["FiscalYear"] = "0";
                    return View(reportResponse);
                }
                else
                {
                    TempData["LoginFailure"] = reportResponse.Message;
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
        public async Task<ActionResult> ManageGrantProjectReport(ReportResponse reportResponse)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (Request.Form["ProjectId"] == "")
                ViewData["Project"] = "0";
            else
                ViewData["Project"] = Convert.ToInt32(Request.Form["ProjectId"]);
            if (Request.Form["FiscalYearId"] == "")
                ViewData["FiscalYear"] = "0";
            else
                ViewData["FiscalYear"] = Convert.ToInt32(Request.Form["FiscalYearId"]);

           ReportRequest reportRequest = new ReportRequest();
            reportRequest.ProjectId = Convert.ToInt32(ViewData["Project"]);
            reportRequest.FiscalYearId = Convert.ToInt32(ViewData["FiscalYear"]);

            string url = strBaseURL + "Report/GetGrantProjectReport";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, reportRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                reportResponse = JsonConvert.DeserializeObject<ReportResponse>(responseData);
                if (reportResponse.Message == string.Empty && reportResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Reports";
                    reportResponse.rolePermissionEntity = Common.PagePermissions(PageName); bool PageHasPermissionsOrNot = CheckPagePermissionHeadders.PageHasPermission(PageName);
                    if (!PageHasPermissionsOrNot)
                        return RedirectToAction("Index", new { ErrorMsg = "You do not have access to this activity. Please contact your administrator." });

                    return View(reportResponse);
                }
                else
                {
                    TempData["LoginFailure"] = reportResponse.Message;
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
        public async Task<ActionResult> ManageDHSSummaryReport()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            ReportResponse reportResponse = new ReportResponse();
            string url = strBaseURL + "Report/GetGrantProjectReport";
            // string url = strBaseURL + "AuditReport/GetCRSummaryReport";
            client.BaseAddress = new Uri(url);
            ReportRequest reportRequest = new ReportRequest();
            reportRequest.FiscalYearId = 0;
            reportRequest.ProjectId = 0;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, reportRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                reportResponse = JsonConvert.DeserializeObject<ReportResponse>(responseData);
                if (reportResponse.Message == string.Empty && reportResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Reports";
                    reportResponse.rolePermissionEntity = Common.PagePermissions(PageName); bool PageHasPermissionsOrNot = CheckPagePermissionHeadders.PageHasPermission(PageName);
                    if (!PageHasPermissionsOrNot)
                        return RedirectToAction("Index", new { ErrorMsg = "You do not have access to this activity. Please contact your administrator." });
                    ViewData["FiscalYear"] = "0";
                    return View(reportResponse);
                }
                else
                {
                    TempData["LoginFailure"] = reportResponse.Message;
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
        public async Task<ActionResult> ManageDHSSummaryReport(ReportResponse reportResponse)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (Request.Form["FiscalYearId"] == "")
                ViewData["FiscalYear"] = "0";
            else
                ViewData["FiscalYear"] = Convert.ToInt32(Request.Form["FiscalYearId"]);

            ReportRequest reportRequest = new ReportRequest();
            reportRequest.FiscalYearId = Convert.ToInt32(ViewData["FiscalYear"]);

            string url = strBaseURL + "Report/GetGrantProjectReport";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, reportRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                reportResponse = JsonConvert.DeserializeObject<ReportResponse>(responseData);
                if (reportResponse.Message == string.Empty && reportResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Reports";
                    reportResponse.rolePermissionEntity = Common.PagePermissions(PageName); bool PageHasPermissionsOrNot = CheckPagePermissionHeadders.PageHasPermission(PageName);
                    if (!PageHasPermissionsOrNot)
                        return RedirectToAction("Index", new { ErrorMsg = "You do not have access to this activity. Please contact your administrator." });

                    return View(reportResponse);
                }
                else
                {
                    TempData["LoginFailure"] = reportResponse.Message;
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
        public async Task<ActionResult> ManageDERSummaryReport()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            ReportResponse reportResponse = new ReportResponse();
            string url = strBaseURL + "Report/GetGrantProjectReport";
            // string url = strBaseURL + "AuditReport/GetCRSummaryReport";
            client.BaseAddress = new Uri(url);
            ReportRequest reportRequest = new ReportRequest();
            reportRequest.FiscalYearId = 0;
            reportRequest.ProjectId = 0;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, reportRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                reportResponse = JsonConvert.DeserializeObject<ReportResponse>(responseData);
                if (reportResponse.Message == string.Empty && reportResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Reports";
                    reportResponse.rolePermissionEntity = Common.PagePermissions(PageName); bool PageHasPermissionsOrNot = CheckPagePermissionHeadders.PageHasPermission(PageName);
                    if (!PageHasPermissionsOrNot)
                        return RedirectToAction("Index", new { ErrorMsg = "You do not have access to this activity. Please contact your administrator." });
                    ViewData["FiscalYear"] = "0";
                    return View(reportResponse);
                }
                else
                {
                    TempData["LoginFailure"] = reportResponse.Message;
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
        public async Task<ActionResult> ManageDERSummaryReport(ReportResponse reportResponse)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (Request.Form["FiscalYearId"] == "")
                ViewData["FiscalYear"] = "0";
            else
                ViewData["FiscalYear"] = Convert.ToInt32(Request.Form["FiscalYearId"]);

            ReportRequest reportRequest = new ReportRequest();
            reportRequest.FiscalYearId = Convert.ToInt32(ViewData["FiscalYear"]);

            string url = strBaseURL + "Report/GetGrantProjectReport";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, reportRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                reportResponse = JsonConvert.DeserializeObject<ReportResponse>(responseData);
                if (reportResponse.Message == string.Empty && reportResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Reports";
                    reportResponse.rolePermissionEntity = Common.PagePermissions(PageName); bool PageHasPermissionsOrNot = CheckPagePermissionHeadders.PageHasPermission(PageName);
                    if (!PageHasPermissionsOrNot)
                        return RedirectToAction("Index", new { ErrorMsg = "You do not have access to this activity. Please contact your administrator." });

                    return View(reportResponse);
                }
                else
                {
                    TempData["LoginFailure"] = reportResponse.Message;
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