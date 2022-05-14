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
        ExpenseResponse expenseResponse;
        //The URL of the WEB API Service

        public ExpenseController()
        {
            expenseResponse = new ExpenseResponse();
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
            
            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.StatusId = 0;
            expenseEntity.AssignedTo = 0;
            expenseEntity.ProjectId = 0;
            expenseEntity.FiscalYearId = 0;
            expenseEntity.PeriodId = 0;
            expenseEntity.SourceId = 0;
            if (Common.GetSession("EAssignedTo") != "")
                expenseEntity.AssignedTo = Convert.ToInt32(Common.GetSession("EAssignedTo"));

            if (Common.GetSession("EStatusId") != "")
                expenseEntity.StatusId = Convert.ToInt32(Common.GetSession("EStatusId"));

            if (Common.GetSession("EProjectId") != "")
                expenseEntity.ProjectId = Convert.ToInt32(Common.GetSession("EProjectId"));

            if (Common.GetSession("EFiscalYearId") != "")
                expenseEntity.FiscalYearId = Convert.ToInt32(Common.GetSession("EFiscalYearId"));

            if (Common.GetSession("EPeriodId") != "")
                expenseEntity.PeriodId = Convert.ToInt32(Common.GetSession("EPeriodId"));

            if (Common.GetSession("ESourceId") != "")
                expenseEntity.SourceId = Convert.ToInt32(Common.GetSession("ESourceId"));
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

            Common.AddSession("EAssignedTo", expenseEntity.AssignedTo.ToString());
            Common.AddSession("EStatusId", expenseEntity.StatusId.ToString());
            Common.AddSession("EProjectId", expenseEntity.ProjectId.ToString());
            Common.AddSession("EFiscalYearId", expenseEntity.FiscalYearId.ToString());
            Common.AddSession("EPeriodId", expenseEntity.PeriodId.ToString());
            Common.AddSession("ESourceId", expenseEntity.SourceId.ToString());
            //if (pageIndex == 1)
            //{
            //    if (Common.GetSession("EPageIndex") != "")
            //        pageIndex = Convert.ToInt32(Common.GetSession("EPageIndex"));
            //}
            //if (pageIndex > 1)
            //    Common.AddSession("EPageIndex", pageIndex.ToString());
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
           

            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ExpenseId = Convert.ToInt32(id);
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

        //public async Task<ActionResult> CreateTransactionDetail(long? id=0)
        //{
        //    if (!Common.SessionExists())
        //        return RedirectToAction("Index", "Home");
        //    if (id == 0)
        //        return RedirectToAction("Index", "Home");
        //   

        //    ExpenseRequest expenseRequest = new ExpenseRequest();
        //    TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
        //    transactionDetailEntity.ExpenseId = Convert.ToInt64(id);
        //    transactionDetailEntity.TransactionDetailId = 0;
        //    expenseRequest.transactionDetailEntity = transactionDetailEntity;
        //    string url = strBaseURL + "Expense/GetTransactionDetail";
        //    client.BaseAddress = new Uri(url);
        //    HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //        expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
        //        if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
        //        {
        //            string PageName = "Expesnses";
        //            expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
        //            return View(expenseResponse);
        //        }
        //        else
        //        {
        //            TempData["LoginFailure"] = expenseResponse.Message;
        //            return RedirectToAction("Error", "Home");
        //        }
        //    }
        //    else
        //    {
        //        TempData["LoginFailure"] = responseMessage.ToString();
        //        return RedirectToAction("Error", "Home");
        //    }
        //}

        public async Task<ActionResult> CreateTransactionDetail(long? id = 0, long? pid = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");

           
            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.ExpenseId = Convert.ToInt64(id);
            transactionDetailEntity.TransactionDetailId = Convert.ToInt64(pid);
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
           

            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.ExpenseId = Convert.ToInt64(id);
            transactionDetailEntity.ProjectId = 0;
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

        public async Task<ActionResult> ManageExpenseTransactions(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
           
            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.TransactionNumber = String.Empty;
            transactionDetailEntity.ProjectId = 0;
            transactionDetailEntity.FiscalYearId = 0;
            transactionDetailEntity.StatusId = 0;
            transactionDetailEntity.FGTCategoryId2 = 0;
            transactionDetailEntity.RevenueTransactionNumber = String.Empty;

            if (Common.GetSession("ETProjectId") != "")
                transactionDetailEntity.ProjectId = Convert.ToInt32(Common.GetSession("ETProjectId"));

            if (Common.GetSession("ETFiscalYearId") != "")
                transactionDetailEntity.FiscalYearId = Convert.ToInt32(Common.GetSession("ETFiscalYearId"));

            if (Common.GetSession("ETStatusId") != "")
                transactionDetailEntity.StatusId = Convert.ToInt32(Common.GetSession("ETStatusId"));

            if (Common.GetSession("ETTransactionNumber") != "")
                transactionDetailEntity.TransactionNumber = Common.GetSession("ETTransactionNumber");

            if (Common.GetSession("ETRevenueTransactionNumber") != "")
                transactionDetailEntity.RevenueTransactionNumber = Common.GetSession("ETRevenueTransactionNumber");

            if (Common.GetSession("ETFGTCategoryId2") != "")
                transactionDetailEntity.FGTCategoryId2= Convert.ToUInt32(Common.GetSession("ETFGTCategoryId2"));

            expenseRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Expense/GetAllTransactionDetails";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.transactionDetailEntity = transactionDetailEntity;
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    expenseResponse.pagedTransactionDetailEntity = expenseResponse.transactionDetailEntities.ToPagedList(pageIndex, pageSize);
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
        public async Task<ActionResult> ManageExpenseTransactions(int? page, int? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
           
            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.TransactionNumber = String.Empty;
            transactionDetailEntity.ProjectId = 0;
            transactionDetailEntity.StatusId = 0;
            transactionDetailEntity.FiscalYearId = 0;
            transactionDetailEntity.RevenueTransactionNumber = String.Empty;

            if (Request["ProjectId"] != "")
                transactionDetailEntity.ProjectId = Convert.ToInt32(Request["ProjectId"]);

            if (Request["FiscalYearId"] != "")
                transactionDetailEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);

            if (Request["StatusId"] != "")
                transactionDetailEntity.StatusId = Convert.ToInt32(Request["StatusId"]);

            if (Request["transactionDetailEntity.TransactionNumber"] != "")
                transactionDetailEntity.TransactionNumber = Request["transactionDetailEntity.TransactionNumber"];

            if (Request["transactionDetailEntity.RevenueTransactionNumber"] != "")
                transactionDetailEntity.RevenueTransactionNumber = Request["transactionDetailEntity.RevenueTransactionNumber"];

            if (Request["CategoryId"] != "")
                transactionDetailEntity.FGTCategoryId2 = Convert.ToInt32(Request["CategoryId"]);

            Common.AddSession("ETProjectId", transactionDetailEntity.ProjectId.ToString());
            Common.AddSession("ETFiscalYearId", transactionDetailEntity.FiscalYearId.ToString());
            Common.AddSession("ETStatusId", transactionDetailEntity.StatusId.ToString());
            Common.AddSession("ETRevenueTransactionNumber", transactionDetailEntity.RevenueTransactionNumber.ToString());
            Common.AddSession("ETTransactionNumber", transactionDetailEntity.TransactionNumber.ToString());
            Common.AddSession("ETFGTCategoryId2", transactionDetailEntity.FGTCategoryId2.ToString());

            expenseRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Expense/GetAllTransactionDetails";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.transactionDetailEntity = transactionDetailEntity;
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    expenseResponse.pagedTransactionDetailEntity = expenseResponse.transactionDetailEntities.ToPagedList(pageIndex, pageSize);
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

        public async Task<ActionResult> ViewExpenseTransactionDetail(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
           

            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ExpenseId = Convert.ToInt64(id);
            expenseRequest.expenseEntity = expenseEntity;
            string url = strBaseURL + "Expense/GetExpenseTransactions";
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
                    expenseResponse.expenseEntity = expenseEntity;
                    return PartialView("_viewExpenseTransactionDetail", expenseResponse);
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

        public async Task<ActionResult> ManageExpExpCompare(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
           
            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ProjectId = 0;
            expenseEntity.StatusId = 0;
            expenseEntity.FiscalYearId = 0;
            expenseEntity.Difference = 0;
            if (Common.GetSession("ETCCProjectId") != "")
                expenseEntity.ProjectId = Convert.ToInt32(Common.GetSession("ETCCProjectId"));

            if (Common.GetSession("ETCCStatusId") != "")
                expenseEntity.StatusId = Convert.ToInt32(Common.GetSession("ETCCStatusId"));
            
            if (Common.GetSession("ETCCFiscalYearId") != "")
                expenseEntity.FiscalYearId = Convert.ToInt32(Common.GetSession("ETCCFiscalYearId"));

            if (Common.GetSession("ETCCDifference") != "")
                expenseEntity.Difference = Convert.ToInt32(Common.GetSession("ETCCDifference"));

            expenseRequest.expenseEntity = expenseEntity;
            string url = strBaseURL + "Expense/GetExpExpTransCompare";
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
        public async Task<ActionResult> ManageExpExpCompare(int? page, int? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
           
            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ProjectId = 0;
            expenseEntity.StatusId = 0;
            expenseEntity.FiscalYearId = 0;

            if (Request["ProjectId"] != "")
                expenseEntity.ProjectId = Convert.ToInt32(Request["ProjectId"]);

            if (Request["StatusId"] != "")
                expenseEntity.StatusId = Convert.ToInt32(Request["StatusId"]);

            if (Request["FiscalYearId"] != "")
                expenseEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);

            if (Request["Difference"] != "")
                expenseEntity.Difference = Convert.ToInt32(Request["Difference"]);

            Common.AddSession("ETCCProjectId", expenseEntity.ProjectId.ToString());
            Common.AddSession("ETCCFiscalYearId", expenseEntity.FiscalYearId.ToString());
            Common.AddSession("ETCCStatusId", expenseEntity.StatusId.ToString());
            Common.AddSession("ETCCDifference", expenseEntity.Difference.ToString());
            
            expenseRequest.expenseEntity = expenseEntity;
            string url = strBaseURL + "Expense/GetExpExpTransCompare";
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

        public async Task<ActionResult> ManageMissingExpenseTransactions(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
           
            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.TransactionNumber = String.Empty;
            transactionDetailEntity.TransProject = string.Empty;
            transactionDetailEntity.FiscalYearId = 0;
            transactionDetailEntity.StatusId = 0;
            transactionDetailEntity.FGTCategoryId2 = 0;
            transactionDetailEntity.RevenueTransactionNumber = String.Empty;

            if (Common.GetSession("ETMTransProject") != "")
                transactionDetailEntity.TransProject = Common.GetSession("ETMTransProject");

            if (Common.GetSession("ETMFiscalYearId") != "")
                transactionDetailEntity.FiscalYearId = Convert.ToInt32(Common.GetSession("ETMFiscalYearId"));

            if (Common.GetSession("ETMStatusId") != "")
                transactionDetailEntity.StatusId = Convert.ToInt32(Common.GetSession("ETMStatusId"));

            if (Common.GetSession("ETMTransactionNumber") != "")
                transactionDetailEntity.TransactionNumber = Common.GetSession("ETMTransactionNumber");

            if (Common.GetSession("ETMRevenueTransactionNumber") != "")
                transactionDetailEntity.RevenueTransactionNumber = Common.GetSession("ETMRevenueTransactionNumber");

            if (Common.GetSession("ETMFGTCategoryId2") != "")
                transactionDetailEntity.FGTCategoryId2= Convert.ToUInt32(Common.GetSession("ETMFGTCategoryId2"));

            expenseRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Expense/GetMissingExpenseTransactions";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.transactionDetailEntity = transactionDetailEntity;
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    expenseResponse.pagedTransactionDetailEntity = expenseResponse.transactionDetailEntities.ToPagedList(pageIndex, pageSize);
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
        public async Task<ActionResult> ManageMissingExpenseTransactions(int? page, int? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
           
            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.TransactionNumber = String.Empty;
            transactionDetailEntity.TransProject = string.Empty;
            transactionDetailEntity.StatusId = 0;
            transactionDetailEntity.FiscalYearId = 0;
            transactionDetailEntity.RevenueTransactionNumber = String.Empty;

            if (Request["transactionDetailEntity.TransProject"] != "")
                transactionDetailEntity.TransProject = Request["transactionDetailEntity.TransProject"];

            if (Request["FiscalYearId"] != "")
                transactionDetailEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);

            if (Request["StatusId"] != "")
                transactionDetailEntity.StatusId = Convert.ToInt32(Request["StatusId"]);

            if (Request["transactionDetailEntity.TransactionNumber"] != "")
                transactionDetailEntity.TransactionNumber = Request["transactionDetailEntity.TransactionNumber"];

            if (Request["transactionDetailEntity.RevenueTransactionNumber"] != "")
                transactionDetailEntity.RevenueTransactionNumber = Request["transactionDetailEntity.RevenueTransactionNumber"];

            if (Request["CategoryId"] != "")
                transactionDetailEntity.FGTCategoryId2 = Convert.ToInt32(Request["CategoryId"]);

            Common.AddSession("ETMTransProject", transactionDetailEntity.TransProject.ToString());
            Common.AddSession("ETMFiscalYearId", transactionDetailEntity.FiscalYearId.ToString());
            Common.AddSession("ETMStatusId", transactionDetailEntity.StatusId.ToString());
            Common.AddSession("ETMRevenueTransactionNumber", transactionDetailEntity.RevenueTransactionNumber.ToString());
            Common.AddSession("ETMTransactionNumber", transactionDetailEntity.TransactionNumber.ToString());
            Common.AddSession("ETMFGTCategoryId2", transactionDetailEntity.FGTCategoryId2.ToString());

            expenseRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Expense/GetMissingExpenseTransactions";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.transactionDetailEntity = transactionDetailEntity;
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    expenseResponse.pagedTransactionDetailEntity = expenseResponse.transactionDetailEntities.ToPagedList(pageIndex, pageSize);
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

        public async Task<ActionResult> CreateMissingExpenseTransaction(int? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.TransactionDetailId = Convert.ToInt64(id);
            expenseRequest.transactionDetailEntity = transactionDetailEntity;

            string url = strBaseURL + "Expense/GetMissingExpenseTransaction";
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

        public async Task<ActionResult> ViewMissingExpenseTransaction(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.TransactionDetailId = Convert.ToInt64(id);
            expenseRequest.transactionDetailEntity = transactionDetailEntity;

            string url = strBaseURL + "Expense/GetMissingExpenseTransaction";
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
                    return PartialView("_viewMissingExpenseTransaction", expenseResponse);                    
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

        public async Task<ActionResult> EditMissingExpenseTransaction(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.TransactionDetailId = Convert.ToInt64(id);
            expenseRequest.transactionDetailEntity = transactionDetailEntity;

            string url = strBaseURL + "Expense/GetMissingExpenseTransaction";
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
                    return PartialView("_editMissingExpenseTransaction", expenseResponse);
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

        public async Task<ActionResult> ViewMissingRevneueTransaction(long id, string projectCode)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.FiscalYearId = Convert.ToInt64(id);
            transactionDetailEntity.TransProject = projectCode;
            expenseRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Expense/GetMissingRevenueTransaction";
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
                    return PartialView("_revenueMissingTransaction", expenseResponse);
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

        public async Task<ActionResult> ManagePriorYearExpenseTransactions(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.TransactionNumber = String.Empty;
            transactionDetailEntity.ProjectId = 0;
            transactionDetailEntity.FiscalYearId = 5;
            transactionDetailEntity.StatusId = 0;
            transactionDetailEntity.FGTCategoryId2 = 0;
            transactionDetailEntity.RevenueTransactionNumber = String.Empty;

            if (Common.GetSession("ETProjectId") != "")
                transactionDetailEntity.ProjectId = Convert.ToInt32(Common.GetSession("ETProjectId"));

            if (Common.GetSession("ETFiscalYearId") != "")
                transactionDetailEntity.FiscalYearId = Convert.ToInt32(Common.GetSession("ETFiscalYearId"));

            if (Common.GetSession("ETStatusId") != "")
                transactionDetailEntity.StatusId = Convert.ToInt32(Common.GetSession("ETStatusId"));

            if (Common.GetSession("ETTransactionNumber") != "")
                transactionDetailEntity.TransactionNumber = Common.GetSession("ETTransactionNumber");

            if (Common.GetSession("ETRevenueTransactionNumber") != "")
                transactionDetailEntity.RevenueTransactionNumber = Common.GetSession("ETRevenueTransactionNumber");

            if (Common.GetSession("ETFGTCategoryId2") != "")
                transactionDetailEntity.FGTCategoryId2 = Convert.ToUInt32(Common.GetSession("ETFGTCategoryId2"));

            expenseRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Expense/GetPriorYearExpenseTransactions";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.transactionDetailEntity = transactionDetailEntity;
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    expenseResponse.pagedTransactionDetailEntity = expenseResponse.transactionDetailEntities.ToPagedList(pageIndex, pageSize);
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
        public async Task<ActionResult> ManagePriorYearExpenseTransactions(int? page, int? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            ExpenseRequest expenseRequest = new ExpenseRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.TransactionNumber = String.Empty;
            transactionDetailEntity.ProjectId = 0;
            transactionDetailEntity.StatusId = 0;
            transactionDetailEntity.FiscalYearId = 0;
            transactionDetailEntity.RevenueTransactionNumber = String.Empty;

            if (Request["ProjectId"] != "")
                transactionDetailEntity.ProjectId = Convert.ToInt32(Request["ProjectId"]);

            if (Request["FiscalYearId"] != "")
                transactionDetailEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);

            if (Request["StatusId"] != "")
                transactionDetailEntity.StatusId = Convert.ToInt32(Request["StatusId"]);

            if (Request["transactionDetailEntity.TransactionNumber"] != "")
                transactionDetailEntity.TransactionNumber = Request["transactionDetailEntity.TransactionNumber"];

            if (Request["transactionDetailEntity.RevenueTransactionNumber"] != "")
                transactionDetailEntity.RevenueTransactionNumber = Request["transactionDetailEntity.RevenueTransactionNumber"];

            if (Request["CategoryId"] != "")
                transactionDetailEntity.FGTCategoryId2 = Convert.ToInt32(Request["CategoryId"]);

            Common.AddSession("ETProjectId", transactionDetailEntity.ProjectId.ToString());
            Common.AddSession("ETFiscalYearId", transactionDetailEntity.FiscalYearId.ToString());
            Common.AddSession("ETStatusId", transactionDetailEntity.StatusId.ToString());
            Common.AddSession("ETRevenueTransactionNumber", transactionDetailEntity.RevenueTransactionNumber.ToString());
            Common.AddSession("ETTransactionNumber", transactionDetailEntity.TransactionNumber.ToString());
            Common.AddSession("ETFGTCategoryId2", transactionDetailEntity.FGTCategoryId2.ToString());

            expenseRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Expense/GetPriorYearExpenseTransactions";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, expenseRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                expenseResponse = JsonConvert.DeserializeObject<ExpenseResponse>(responseData);
                if (expenseResponse.Message == string.Empty && expenseResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    expenseResponse.transactionDetailEntity = transactionDetailEntity;
                    expenseResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    expenseResponse.pagedTransactionDetailEntity = expenseResponse.transactionDetailEntities.ToPagedList(pageIndex, pageSize);
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
    }
}