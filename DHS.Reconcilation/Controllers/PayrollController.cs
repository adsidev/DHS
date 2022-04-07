using DHS.Reconcilation.Models;
using DHSEntities;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
            PayrollResponse payrollResponse = new PayrollResponse();
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
            PayrollResponse payrollResponse = new PayrollResponse();
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
            PayrollResponse payrollResponse = new PayrollResponse();
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
    }
}