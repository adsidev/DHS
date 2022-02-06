using DHS.Reconcilation.Models;
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
    public class ExpenseController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service

        public ExpenseController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> ManageExpenses(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ExpenseResponse expenseResponse = new ExpenseResponse();
            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.StatusId = 0;
            expenseEntity.AssignedTo = 0;
            expenseEntity.ProjectId = 0;
            expenseEntity.FiscalYearId = 0;
            expenseEntity.PeriodId = 0;
            expenseEntity.SourceId = 0;
            if (Request["AssignedTo"] != "")
                expenseEntity.AssignedTo = Convert.ToInt32(Request["AssignedTo"]);

            if (Request["StatusId"] != "")
                expenseEntity.StatusId = Convert.ToInt32(Request["StatusId"]);

            if (Request["ProjectId"] != "")
                expenseEntity.ProjectId = Convert.ToInt32(Request["ProjectId"]);
            
            if (Request["FiscalYearId"] != "")
                expenseEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);

            if (Request["PeriodId"] != "")
                expenseEntity.PeriodId = Convert.ToInt32(Request["PeriodId"]);

            if (Request["SourceId"] != "")
                expenseEntity.SourceId = Convert.ToInt32(Request["SourceId"]);
            expenseRequest.expenseEntity = expenseEntity;
            string url = strBaseURL + "Expense/GetExpenses";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.expenseEntity = expenseEntity;
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    expenseResponse.pagedExpenseEntities = expenseResponse.expenseEntities.ToPagedList(pageIndex, pageSize);
                    return View(expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
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
        public async Task<ActionResult> ManageExpenses(int? page, int?id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ExpenseResponse expenseResponse = new ExpenseResponse();
            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.StatusId = 0;
            expenseEntity.AssignedTo = 0;
            expenseEntity.ProjectId = 0;
            expenseEntity.FiscalYearId = 0;
            expenseEntity.PeriodId = 0;
            expenseEntity.SourceId = 0;
            if (Request["AssignedTo"] != "")
                expenseEntity.AssignedTo = Convert.ToInt32(Request["AssignedTo"]);

            if (Request["StatusId"] != "")
                expenseEntity.StatusId = Convert.ToInt32(Request["StatusId"]);

            if (Request["ProjectId"] != "")
                expenseEntity.ProjectId = Convert.ToInt32(Request["ProjectId"]);

            if (Request["FiscalYearId"] != "")
                expenseEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);

            if (Request["PeriodId"] != "")
                expenseEntity.PeriodId = Convert.ToInt32(Request["PeriodId"]);

            if (Request["SourceId"] != "")
                expenseEntity.SourceId = Convert.ToInt32(Request["SourceId"]);
            expenseRequest.expenseEntity = expenseEntity;
            string url = strBaseURL + "Expense/GetExpenses";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.expenseEntity = expenseEntity;
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    expenseResponse.pagedExpenseEntities = expenseResponse.expenseEntities.ToPagedList(pageIndex, pageSize);
                    return View(expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> CreateExpense()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            ExpenseResponse expenseResponse = new ExpenseResponse();

            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ExpenseId = 0;
            expenseRequest.expenseEntity = expenseEntity;
            string url = strBaseURL + "Expense/GetExpense";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return View(expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> ViewExpense(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            ExpenseResponse expenseResponse = new ExpenseResponse();

            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ExpenseId = Convert.ToInt64(id);
            expenseRequest.expenseEntity = expenseEntity;
            string url = strBaseURL + "Expense/GetTransactionDetails";
            client.BaseAddress = new Uri(url);
            Common.AddSession("ExpenseId", id.ToString());
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewExpense", expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> EditExpense(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            ExpenseResponse expenseResponse = new ExpenseResponse();

            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ExpenseId = Convert.ToInt32(id);
            expenseRequest.expenseEntity = expenseEntity;
            string url = strBaseURL + "Expense/GetTransactionDetails";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editExpense", expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> ManageTransactionDetails(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            ExpenseResponse expenseResponse = new ExpenseResponse();

            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ExpenseId = Convert.ToInt64(id);
            expenseRequest.expenseEntity = expenseEntity;
            string url = strBaseURL + "Expense/GetTransactionDetails";
            client.BaseAddress = new Uri(url);
            Common.AddSession("ExpenseId", id.ToString());
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    //expenseResponse.pagedTransactionDetailEntity = expenseResponse.transactionDetailEntities.ToPagedList(pageIndex, pageSize);
                    return View(expenseResponse);
                    //return PartialView("_viewExpense", expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> CreateTransactionDetail(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            ExpenseResponse expenseResponse = new ExpenseResponse();

            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.ExpenseId = Convert.ToInt64(id);
            transactionDetailEntity.TransactionDetailId = 0;
            expenseRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Expense/GetTransactionDetail";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }
        
        public async Task<ActionResult> ViewTransactionDetail(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            ExpenseResponse expenseResponse = new ExpenseResponse();

            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.TransactionDetailId = Convert.ToInt64(id);
            expenseRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Expense/GetTransactionDetail";
            client.BaseAddress = new Uri(url);
            Common.AddSession("ExpenseId", id.ToString());
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewTransactionDetail", expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> EditTransactionDetail(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            ExpenseResponse expenseResponse = new ExpenseResponse();

            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.TransactionDetailId = Convert.ToInt64(id);
            expenseRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Expense/GetTransactionDetail";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editTransactionDetail", expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> ExpenseRevenue(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            ExpenseResponse expenseResponse = new ExpenseResponse();

            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ExpenseId = Convert.ToInt32(((long)id).ToString());
            expenseRequest.expenseEntity = expenseEntity;
            string url = strBaseURL + "Expense/GetExpenseRevenue";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    if(expenseResponse.revenueEntities.Count==0)
                    {
                        return RedirectToAction("LinkToRevenue", "Expense", new {@id = expenseEntity.ExpenseId });
                    }
                    return View(expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> LinkToRevenue(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if(id == 0)
                return RedirectToAction("Index", "Home");
            ExpenseResponse expenseResponse = new ExpenseResponse();

            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ExpenseId = Convert.ToInt32(((long)id).ToString());
            expenseRequest.expenseEntity = expenseEntity;
            string url = strBaseURL + "Expense/GetLinkToRevenue";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
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
        public async Task<ActionResult> LinkToRevenue()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            ExpenseResponse expenseResponse = new ExpenseResponse();

            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ExpenseId = Convert.ToInt64(Request["ExpenseId"].ToString());
            expenseEntity.ProjectId = Convert.ToInt64(Request["ProjectId"]);
            expenseRequest.expenseEntity = expenseEntity;
            string url = strBaseURL + "Expense/GetLinkToRevenue";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> ExpenseDocuments(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            ExpenseResponse expenseResponse = new ExpenseResponse();

            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ExpenseId = Convert.ToInt32(((long)id).ToString());
            expenseRequest.expenseEntity = expenseEntity;
            string url = strBaseURL + "Expense/GetExpenseDocuments";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> CreateExpenseDocument(int id = 0, int pid = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            ExpenseResponse expenseResponse = new ExpenseResponse();
            ExpenseRequest expenseRequest = new ExpenseRequest();
            DocumentEntity documentEntity = new DocumentEntity();

            documentEntity.DocumentReferenceId = pid;
            documentEntity.DocumentId = id;
            expenseRequest.documentEntity = documentEntity;

            string url = strBaseURL + "Expense/GetExpenseDocument";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    if (id == 0)
                        expenseResponse.documentEntity = documentEntity; 
                    string PageName = "Expesnses";
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
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
        public async Task<ActionResult> CreateExpenseDocument(HttpPostedFileBase fileUpload)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            ExpenseResponse expenseResponse = new ExpenseResponse();
            ExpenseRequest expenseRequest = new ExpenseRequest();

            string fileName = string.Empty;
            if (fileUpload != null)
            {
                string destinationPath = string.Empty;
                fileName = System.IO.Path.GetFileName(fileUpload.FileName);
                fileName = fileName.Replace(" ", "");
                destinationPath = System.IO.Path.Combine(Server.MapPath("~/Documents/Expense"), fileName);
                fileUpload.SaveAs(destinationPath);
            }
            else
            {
                fileName = Request["DocumentFile"];
            }
            DocumentEntity documentEntity = new DocumentEntity();
            documentEntity.DocumentId = Convert.ToInt32(Request["DocumentEntity.DocumentId"]);
            documentEntity.DocumentReferenceId = Convert.ToInt32(Request["ExpenseId"]);
            documentEntity.DocumentCategoryId = Convert.ToInt32(Request["DocumentCategoryId"]);
            documentEntity.DisplayOrder = Convert.ToInt32(Request["DocumentEntity.DisplayOrder"]);
            documentEntity.DocumentDescription = Request["Description"];
            documentEntity.DocumentFile = fileName;
            documentEntity.DocumentTitle = Request["DocumentEntity.DocumentTitle"];
            documentEntity.ModifiedBy = Convert.ToInt32(Common.GetSession("UserID"));
            documentEntity.DocumentTypeId = 1;
            expenseRequest.documentEntity = documentEntity;

            string url = strBaseURL + "Expense/SaveExpenseDocument";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    return RedirectToAction("ExpenseDocuments", "Expense", new { @id = documentEntity.DocumentReferenceId});
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }


        public async Task<ActionResult> ViewRevenue(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.RevenueId = Convert.ToInt64(id);
            Common.AddSession("RevenueId", id.ToString());
            revenueRequest.revenueEntity = revenueEntity;
            string url = strBaseURL + "Revenue/GetRevenue";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Revenues";
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewRevenue", revenueResponse);
                }
                else
                {
                    TempData["LoginFailure"] = revenueResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }


        public async Task<ActionResult> RevenueTransaction(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            ExpenseResponse expenseResponse = new ExpenseResponse();

            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.ExpenseId = Convert.ToInt64(id);
            expenseRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Expense/GetRevenueTransactionByExpenseId";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return PartialView("_revenueTransaction", expenseResponse);
                }
                else
                {
                    TempData["LoginFailure"] = expenseResponse.Message;
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