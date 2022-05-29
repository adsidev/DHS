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
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ManageGrantProjectReport()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            Session["FGReport"] = "0";
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
                    int PageName = 10;
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
            Session["FGReport"] = "0";
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
                    int PageName = 10;
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
                    int PageName = 10;
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
                    int PageName = 10;
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
                    int PageName = 10;
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
                    int PageName = 10;
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
        public async Task<ActionResult> ManageGrantReport()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            Session["FGReport"] = "0";
            ReportResponse reportResponse = new ReportResponse();
            string url = strBaseURL + "Report/GetGrantReport";
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
                    reportResponse.ProjectId = reportRequest.ProjectId;
                    reportResponse.FiscalYearId = reportRequest.FiscalYearId;
                    int PageName = 10;
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

        [HttpPost]
        public async Task<ActionResult> ManageGrantReport(ReportResponse reportResponse)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            ReportRequest reportRequest = new ReportRequest();
            if (Request.Form["ProjectId"] == "")
                reportRequest.ProjectId = 0;
            else
                reportRequest.ProjectId = Convert.ToInt32(Request.Form["ProjectId"]);
            if (Request.Form["FiscalYearId"] == "")
                reportRequest.FiscalYearId = 0;
            else
                reportRequest.FiscalYearId = Convert.ToInt32(Request.Form["FiscalYearId"]);
            
            string url = strBaseURL + "Report/GetGrantReport";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, reportRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                reportResponse = JsonConvert.DeserializeObject<ReportResponse>(responseData);
                if (reportResponse.Message == string.Empty && reportResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 10;
                    reportResponse.ProjectId = reportRequest.ProjectId;
                    reportResponse.FiscalYearId = reportRequest.FiscalYearId;
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
        public async Task<ActionResult> ManageReceivablesReport()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            Session["FGReport"] = "0";
            ReportResponse reportResponse = new ReportResponse();
            string url = strBaseURL + "Report/GetGrantProjectReport";
            // string url = strBaseURL + "AuditReport/GetCRSummaryReport";
            client.BaseAddress = new Uri(url);
            ReportRequest reportRequest = new ReportRequest();
            reportRequest.FiscalYearId = 0;
            reportRequest.ProjectStatusId = 0;
            reportRequest.ProjectId = 0;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, reportRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                reportResponse = JsonConvert.DeserializeObject<ReportResponse>(responseData);
                if (reportResponse.Message == string.Empty && reportResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 10;
                    reportResponse.rolePermissionEntity = Common.PagePermissions(PageName); bool PageHasPermissionsOrNot = CheckPagePermissionHeadders.PageHasPermission(PageName);
                    if (!PageHasPermissionsOrNot)
                        return RedirectToAction("Index", new { ErrorMsg = "You do not have access to this activity. Please contact your administrator." });
                    ViewData["StatusName"] = "0";
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
        public async Task<ActionResult> ManageReceivablesReport(ReportResponse reportResponse)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (Request.Form["StatusId"] == "")
                ViewData["StatusName"] = "0";
            else
                ViewData["StatusName"] = Convert.ToInt32(Request.Form["StatusId"]);

            if (Request.Form["ProjectId"] == "")
                ViewData["ProjectName"] = "0";
            else
                ViewData["ProjectName"] = Convert.ToInt32(Request.Form["ProjectId"]);


            if (Request.Form["FiscalYearId"] == "")
                ViewData["FiscalYear"] = "0";
            else
                ViewData["FiscalYear"] = Convert.ToInt32(Request.Form["FiscalYearId"]);
            Session["FGReport"] = "0";
            ReportRequest reportRequest = new ReportRequest();
            reportRequest.ProjectStatusId = Convert.ToInt32(ViewData["StatusName"]);
            reportRequest.ProjectId = Convert.ToInt32(ViewData["ProjectName"]);
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
                    int PageName = 10;
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
        public async Task<ActionResult> ManageProjectSummaries()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            Session["FGReport"] = "0";
            ReportResponse reportResponse = new ReportResponse();
            string url = strBaseURL + "Report/GetGrantProjectReport";
            // string url = strBaseURL + "AuditReport/GetCRSummaryReport";
            client.BaseAddress = new Uri(url);
            ReportRequest reportRequest = new ReportRequest();
            reportRequest.FiscalYearId = 0;
            reportRequest.ProjectStatusId = 0;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, reportRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                reportResponse = JsonConvert.DeserializeObject<ReportResponse>(responseData);
                if (reportResponse.Message == string.Empty && reportResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 10;
                    reportResponse.rolePermissionEntity = Common.PagePermissions(PageName); bool PageHasPermissionsOrNot = CheckPagePermissionHeadders.PageHasPermission(PageName);
                    if (!PageHasPermissionsOrNot)
                        return RedirectToAction("Index", new { ErrorMsg = "You do not have access to this activity. Please contact your administrator." });
                    ViewData["ProjectStatus"] = "0";
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
        public async Task<ActionResult> ManageProjectSummaries(ReportResponse reportResponse)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (Request.Form["ProjectStatusId"] == "")
                ViewData["ProjectStatus"] = "0";
            else
                ViewData["ProjectStatus"] = Convert.ToInt32(Request.Form["ProjectStatusId"]);
            if (Request.Form["FiscalYearId"] == "")
                ViewData["FiscalYear"] = "0";
            else
                ViewData["FiscalYear"] = Convert.ToInt32(Request.Form["FiscalYearId"]);
            Session["FGReport"] = "0";
            ReportRequest reportRequest = new ReportRequest();
            reportRequest.ProjectStatusId = Convert.ToInt32(ViewData["ProjectStatus"]);
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
                    int PageName = 10;
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
        public async Task<ActionResult> ManageProjectReceivableReport()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            Session["FGReport"] = "0";
            ReportResponse reportResponse = new ReportResponse();
            string url = strBaseURL + "Report/GetProjectReceivablesReport";
            // string url = strBaseURL + "AuditReport/GetCRSummaryReport";
            client.BaseAddress = new Uri(url);
            ReportRequest reportRequest = new ReportRequest();
            reportRequest.FiscalYearId = 0;
            reportRequest.ProjectStatusId = 0;
            reportRequest.ProjectId = 0;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, reportRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                reportResponse = JsonConvert.DeserializeObject<ReportResponse>(responseData);
                if (reportResponse.Message == string.Empty && reportResponse.ErrorMessage == string.Empty)
                {
                    reportResponse.ProjectStatusId = reportRequest.ProjectStatusId;
                    reportResponse.ProjectId = reportRequest.ProjectId;
                    reportResponse.FiscalYearId = reportRequest.FiscalYearId;
                    int PageName = 10;
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

        [HttpPost]
        public async Task<ActionResult> ManageProjectReceivableReport(ReportResponse reportResponse)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            ReportRequest reportRequest = new ReportRequest();
            if (Request.Form["FiscalYearId"] == "")
                reportRequest.FiscalYearId = 0;
            else
                reportRequest.FiscalYearId = Convert.ToInt32(Request.Form["FiscalYearId"]);


            if (Request.Form["StatusId"] == "")
                reportRequest.ProjectStatusId = 0;
            else
                reportRequest.ProjectStatusId = Convert.ToInt32(Request.Form["StatusId"]);

            if (Request.Form["ProjectId"] == "")
                reportRequest.ProjectId = 0;
            else
                reportRequest.ProjectId = Convert.ToInt32(Request.Form["ProjectId"]);

            string url = strBaseURL + "Report/GetProjectReceivablesReport";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, reportRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                reportResponse = JsonConvert.DeserializeObject<ReportResponse>(responseData);
                if (reportResponse.Message == string.Empty && reportResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 10;
                    reportResponse.FiscalYearId = reportRequest.FiscalYearId;
                    reportResponse.ProjectStatusId = reportRequest.ProjectStatusId;
                    reportResponse.ProjectId = reportRequest.ProjectId;
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
        public async Task<ActionResult> ManageDifferenceRevenueExpenseTransactions()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            Session["FGReport"] = "0";
            ReportResponse reportResponse = new ReportResponse();
            string url = strBaseURL + "Report/GetGrantProjectReport";
            client.BaseAddress = new Uri(url);
            ReportRequest reportRequest = new ReportRequest();
            reportRequest.FiscalYearId = 0;
            reportRequest.ProjectStatusId = 0;
            reportRequest.ProjectId = 0;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, reportRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                reportResponse = JsonConvert.DeserializeObject<ReportResponse>(responseData);
                if (reportResponse.Message == string.Empty && reportResponse.ErrorMessage == string.Empty)
                {
                    reportResponse.ProjectStatusId = reportRequest.ProjectStatusId;
                    reportResponse.ProjectId = reportRequest.ProjectId;
                    reportResponse.FiscalYearId = reportRequest.FiscalYearId;
                    int PageName = 10;
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