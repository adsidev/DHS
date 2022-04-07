﻿using DHS.Reconcilation.Models;
using DHSEntities;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace DHS.Reconcilation.Controllers
{
    public class PayrollController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service
        PayrollResponse payrollResponse;
        public PayrollController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            payrollResponse = new PayrollResponse();
        }

        [HttpGet]
        public async Task<ActionResult> ManagePayroll(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            PayrollRequest PayrollRequest = new PayrollRequest();
            PayrollEntity payrollEntity = new PayrollEntity();
            payrollEntity.FiscalYearId = 0;
            PayrollRequest.payrollEntity = payrollEntity;
            string url = strBaseURL + "Payroll/GetPayrolls";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, PayrollRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                payrollResponse = JsonConvert.DeserializeObject<PayrollResponse>(responseData);
                if (payrollResponse.Message == string.Empty && payrollResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Payroll";
                    payrollResponse.payrollEntity = PayrollRequest.payrollEntity; 
                    payrollResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    payrollResponse.pagedPayrollEntities = payrollResponse.payrollEntities.ToPagedList(pageIndex, 15);
                    return View(payrollResponse);
                }
                else
                {
                    TempData["LoginFailure"] = payrollResponse.Message;
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
        public async Task<ActionResult> ManagePayroll(int? page, int? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            PayrollRequest PayrollRequest = new PayrollRequest();
            PayrollEntity payrollEntity = new PayrollEntity();
            if(Request["FiscalYearId"]!="")
            payrollEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);
            else
            payrollEntity.FiscalYearId = 0;
            PayrollRequest.payrollEntity = payrollEntity;
            string url = strBaseURL + "Payroll/GetPayrolls";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, PayrollRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                payrollResponse = JsonConvert.DeserializeObject<PayrollResponse>(responseData);
                if (payrollResponse.Message == string.Empty && payrollResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Payroll";
                    payrollResponse.payrollEntity = PayrollRequest.payrollEntity;
                    payrollResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    payrollResponse.pagedPayrollEntities = payrollResponse.payrollEntities.ToPagedList(pageIndex, 15);
                    return View(payrollResponse);
                }
                else
                {
                    TempData["LoginFailure"] = payrollResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> CreatePayroll(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            PayrollRequest payrollRequest = new PayrollRequest();
            PayrollEntity payrollEntity = new PayrollEntity();

            payrollEntity.PayrollId = Convert.ToInt32(id);
            payrollRequest.payrollEntity = payrollEntity;
            string url = strBaseURL + "Payroll/GetPayroll";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, payrollRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                payrollResponse = JsonConvert.DeserializeObject<PayrollResponse>(responseData);
                if (payrollResponse.Message == string.Empty && payrollResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Payroll";
                    payrollResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(payrollResponse);
                }
                else
                {
                    TempData["LoginFailure"] = payrollResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> ViewPayroll(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");

            PayrollRequest payrollRequest = new PayrollRequest();
            PayrollEntity payrollEntity = new PayrollEntity();

            payrollEntity.PayrollId = Convert.ToInt32(id);
            payrollRequest.payrollEntity = payrollEntity;
            string url = strBaseURL + "Payroll/GetPayroll";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, payrollRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                payrollResponse = JsonConvert.DeserializeObject<PayrollResponse>(responseData);
                if (payrollResponse.Message == string.Empty && payrollResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Payroll";
                    payrollResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewPayroll", payrollResponse);
                }
                else
                {
                    TempData["LoginFailure"] = payrollResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> EditPayroll(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");

            PayrollRequest payrollRequest = new PayrollRequest();
            PayrollEntity payrollEntity = new PayrollEntity();

            payrollEntity.PayrollId = Convert.ToInt32(id);
            payrollRequest.payrollEntity = payrollEntity;
            string url = strBaseURL + "Payroll/GetPayroll";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, payrollRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                payrollResponse = JsonConvert.DeserializeObject<PayrollResponse>(responseData);
                if (payrollResponse.Message == string.Empty && payrollResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Payroll";
                    payrollResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editPayroll", payrollResponse);
                }
                else
                {
                    TempData["LoginFailure"] = payrollResponse.Message;
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
        public async Task<ActionResult> ManagePayrollProject(long? id)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            
            PayrollRequest PayrollRequest = new PayrollRequest();
            PayrollProjectEntity payrollProjectEntity = new PayrollProjectEntity();
            payrollProjectEntity.PayrollId = Convert.ToInt64(id);
            PayrollRequest.payrollProjectEntity = payrollProjectEntity;
            
            string url = strBaseURL + "Payroll/GetPayrollProjects";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, PayrollRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                payrollResponse = JsonConvert.DeserializeObject<PayrollResponse>(responseData);
                if (payrollResponse.Message == string.Empty && payrollResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Payroll";
                    payrollResponse.payrollProjectEntity = payrollProjectEntity;
                    payrollResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(payrollResponse);
                }
                else
                {
                    TempData["LoginFailure"] = payrollResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> CreatePayrollProject(long? id)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (id == 0)
                return RedirectToAction("Index", "Home");

            PayrollRequest payrollRequest = new PayrollRequest();
            PayrollProjectEntity payrollProjectEntity = new PayrollProjectEntity();
            payrollProjectEntity.PayrollId = Convert.ToInt32(id);
            payrollProjectEntity.PayrollProjectId = 0;
            payrollRequest.payrollProjectEntity = payrollProjectEntity;
            string url = strBaseURL + "Payroll/GetPayrollProject";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, payrollRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                payrollResponse = JsonConvert.DeserializeObject<PayrollResponse>(responseData);
                if (payrollResponse.Message == string.Empty && payrollResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Payroll";
                    payrollResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(payrollResponse);
                }
                else
                {
                    TempData["LoginFailure"] = payrollResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> ViewPayrollProject(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");

            PayrollRequest payrollRequest = new PayrollRequest();
            PayrollProjectEntity payrollProjectEntity = new PayrollProjectEntity();
            payrollProjectEntity.PayrollProjectId = Convert.ToInt32(id);
            payrollRequest.payrollProjectEntity = payrollProjectEntity;
            string url = strBaseURL + "Payroll/GetPayrollProject";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, payrollRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                payrollResponse = JsonConvert.DeserializeObject<PayrollResponse>(responseData);
                if (payrollResponse.Message == string.Empty && payrollResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Payroll";
                    payrollResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewPayrollProject", payrollResponse);
                }
                else
                {
                    TempData["LoginFailure"] = payrollResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> EditPayrollProject(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            
            PayrollRequest payrollRequest = new PayrollRequest();
            PayrollProjectEntity payrollProjectEntity = new PayrollProjectEntity();
            payrollProjectEntity.PayrollProjectId = Convert.ToInt32(id);
            payrollRequest.payrollProjectEntity = payrollProjectEntity;
            string url = strBaseURL + "Payroll/GetPayrollProject";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, payrollRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                payrollResponse = JsonConvert.DeserializeObject<PayrollResponse>(responseData);
                if (payrollResponse.Message == string.Empty && payrollResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Payroll";
                    payrollResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editPayrollProject", payrollResponse);
                }
                else
                {
                    TempData["LoginFailure"] = payrollResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> PayrollDocuments(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            
            PayrollRequest payrollRequest = new PayrollRequest();
            PayrollProjectEntity payrollProjectEntity = new PayrollProjectEntity();
            payrollProjectEntity.PayrollProjectId = Convert.ToInt64(id);
            payrollRequest.payrollProjectEntity = payrollProjectEntity;

            string url = strBaseURL + "Payroll/GetPayrollDocuments";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, payrollRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                payrollResponse = JsonConvert.DeserializeObject<PayrollResponse>(responseData);
                if (payrollResponse.Message == string.Empty && payrollResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Payroll";
                    payrollResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(payrollResponse);
                }
                else
                {
                    TempData["LoginFailure"] = payrollResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> CreatePayrollDocument(int id = 0, int pid = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            PayrollRequest payrollRequest = new PayrollRequest();
            DocumentEntity documentEntity = new DocumentEntity();

            documentEntity.DocumentReferenceId = pid;
            documentEntity.DocumentId = id;
            payrollRequest.documentEntity = documentEntity;

            string url = strBaseURL + "Payroll/GetPayrollDocument";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, payrollRequest);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                payrollResponse = JsonConvert.DeserializeObject<PayrollResponse>(responseData);
                if (payrollResponse.Message == string.Empty && payrollResponse.ErrorMessage == string.Empty)
                {
                    if (id == 0)
                        payrollResponse.documentEntity = documentEntity;
                    string PageName = "Payroll";
                    payrollResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(payrollResponse);
                }
                else
                {
                    TempData["LoginFailure"] = payrollResponse.Message;
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
        public async Task<ActionResult> CreatePayrollDocument(HttpPostedFileBase fileUpload)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            PayrollRequest payrollRequest = new PayrollRequest();

            string fileName = string.Empty;
            if (fileUpload != null)
            {
                string destinationPath = string.Empty;
                fileName = System.IO.Path.GetFileName(fileUpload.FileName);
                fileName = fileName.Replace(" ", "");
                destinationPath = System.IO.Path.Combine(Server.MapPath("~/Documents/Payroll"), fileName);
                fileUpload.SaveAs(destinationPath);
            }
            else
            {
                fileName = Request["DocumentFile"];
            }
            DocumentEntity documentEntity = new DocumentEntity();
            documentEntity.DocumentId = Convert.ToInt32(Request["DocumentEntity.DocumentId"]);
            documentEntity.DocumentReferenceId = Convert.ToInt32(Request["PayrollProjectId"]);
            if (Request["DocumentCategoryId"] != "")
                documentEntity.DocumentCategoryId = Convert.ToInt32(Request["DocumentCategoryId"]);
            else
                documentEntity.DocumentCategoryId = 0;
            documentEntity.DisplayOrder = Convert.ToInt32(Request["DocumentEntity.DisplayOrder"]);
            documentEntity.DocumentDescription = Request["Description"];
            documentEntity.DocumentFile = fileName;
            documentEntity.DocumentTitle = Request["DocumentEntity.DocumentTitle"];
            documentEntity.ModifiedBy = Convert.ToInt32(Common.GetSession("UserID"));
            documentEntity.DocumentTypeId = 4;
            payrollRequest.documentEntity = documentEntity;

            string url = strBaseURL + "Payroll/SavePayrollDocument";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, payrollRequest);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                payrollResponse = JsonConvert.DeserializeObject<PayrollResponse>(responseData);
                if (payrollResponse.Message == string.Empty && payrollResponse.ErrorMessage == string.Empty)
                {
                    return RedirectToAction("PayrollDocuments", "Payroll", new { @id = documentEntity.DocumentReferenceId });
                }
                else
                {
                    TempData["LoginFailure"] = payrollResponse.Message;
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