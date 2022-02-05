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
    public class RevenueController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service

        public RevenueController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> ManageRevenues(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.StatusId = 0;
            revenueEntity.AssignedTo = 0;
            revenueEntity.ProjectId = 0;
            revenueEntity.FiscalYearId = 0;
            revenueEntity.PeriodId = 0;
            revenueEntity.SourceId = 0;
            if (Request["AssignedTo"] != "")
                revenueEntity.AssignedTo = Convert.ToInt32(Request["AssignedTo"]);

            if (Request["StatusId"] != "")
                revenueEntity.StatusId = Convert.ToInt32(Request["StatusId"]);

            if (Request["ProjectId"] != "")
                revenueEntity.ProjectId = Convert.ToInt32(Request["ProjectId"]);

            if (Request["FiscalYearId"] != "")
                revenueEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);

            if (Request["PeriodId"] != "")
                revenueEntity.PeriodId = Convert.ToInt32(Request["PeriodId"]);

            if (Request["SourceId"] != "")
                revenueEntity.SourceId = Convert.ToInt32(Request["SourceId"]);
            revenueRequest.revenueEntity= revenueEntity;

            string url = strBaseURL + "Revenue/GetRevenues";
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
                    revenueResponse.revenueEntity = revenueEntity;
                    revenueResponse.pagedRevenueEntities = revenueResponse.revenueEntities.ToPagedList(pageIndex, pageSize);
                    return View(revenueResponse);
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

        [HttpPost]
        public async Task<ActionResult> ManageRevenues(int? page, int? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.StatusId = 0;
            revenueEntity.AssignedTo = 0;
            revenueEntity.ProjectId = 0;
            revenueEntity.FiscalYearId = 0;
            revenueEntity.PeriodId = 0;
            revenueEntity.SourceId = 0;
            if (Request["AssignedTo"] != "")
                revenueEntity.AssignedTo = Convert.ToInt32(Request["AssignedTo"]);

            if (Request["StatusId"] != "")
                revenueEntity.StatusId = Convert.ToInt32(Request["StatusId"]);

            if (Request["ProjectId"] != "")
                revenueEntity.ProjectId = Convert.ToInt32(Request["ProjectId"]);

            if (Request["FiscalYearId"] != "")
                revenueEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);

            if (Request["PeriodId"] != "")
                revenueEntity.PeriodId = Convert.ToInt32(Request["PeriodId"]);

            if (Request["SourceId"] != "")
                revenueEntity.SourceId = Convert.ToInt32(Request["SourceId"]);
            revenueRequest.revenueEntity = revenueEntity;
            string url = strBaseURL + "Revenue/GetRevenues";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Revenues";
                    revenueResponse.revenueEntity = revenueEntity;
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    revenueResponse.pagedRevenueEntities = revenueResponse.revenueEntities.ToPagedList(pageIndex, pageSize);
                    return View(revenueResponse);
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

        public async Task<ActionResult> CreateRevenue()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.RevenueId = 0;
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

                    return View(revenueResponse);
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

        public async Task<ActionResult> EditRevenue(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.RevenueId = Convert.ToInt32(id);
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

                    return PartialView("_editRevenue", revenueResponse);
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

        public async Task<ActionResult> RevenueExpense(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.RevenueId = Convert.ToInt32(((long)id).ToString());
            revenueRequest.revenueEntity = revenueEntity;
            string url = strBaseURL + "Revenue/GetRevenueExpense";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    revenueResponse.revenueEntity = revenueEntity;
                    return View(revenueResponse);
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

        public async Task<ActionResult> RevenueDocuments(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.RevenueId = Convert.ToInt32(((long)id).ToString());
            revenueRequest.revenueEntity = revenueEntity;
            string url = strBaseURL + "Revenue/GetRevenueDocuments";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(revenueResponse);
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

        public async Task<ActionResult> CreateRevenueDocument(int id = 0, int pid = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();
            DocumentEntity documentEntity = new DocumentEntity();

            documentEntity.DocumentReferenceId = pid;
            documentEntity.DocumentId = id;
            revenueRequest.documentEntity = documentEntity;

            string url = strBaseURL + "Revenue/GetRevenueDocument";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    if (id == 0)
                        revenueResponse.documentEntity = documentEntity;
                    string PageName = "Expesnses";
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(revenueResponse);
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

        [HttpPost]
        public async Task<ActionResult> CreateRevenueDocument(HttpPostedFileBase fileUpload)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();

            string fileName = string.Empty;
            if (fileUpload != null)
            {
                string destinationPath = string.Empty;
                fileName = System.IO.Path.GetFileName(fileUpload.FileName);
                fileName = fileName.Replace(" ", "");
                destinationPath = System.IO.Path.Combine(Server.MapPath("~/Documents/Revenue"), fileName);
                fileUpload.SaveAs(destinationPath);
            }
            else
            {
                fileName = Request["DocumentFile"];
            }
            DocumentEntity documentEntity = new DocumentEntity();
            documentEntity.DocumentId = Convert.ToInt32(Request["DocumentEntity.DocumentId"]);
            documentEntity.DocumentReferenceId = Convert.ToInt32(Request["RevenueId"]);
            documentEntity.DocumentCategoryId = Convert.ToInt32(Request["DocumentCategoryId"]);
            documentEntity.DisplayOrder = Convert.ToInt32(Request["DocumentEntity.DisplayOrder"]);
            documentEntity.DocumentDescription = Request["Description"];
            documentEntity.DocumentFile = fileName;
            documentEntity.DocumentTitle = Request["DocumentEntity.DocumentTitle"];
            documentEntity.ModifiedBy = Convert.ToInt32(Common.GetSession("UserID"));
            documentEntity.DocumentTypeId = 2;
            revenueRequest.documentEntity = documentEntity;

            string url = strBaseURL + "Revenue/SaveRevenueDocument";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    return RedirectToAction("RevenueDocuments", "Revenue", new { @id = documentEntity.DocumentReferenceId });
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

        public async Task<ActionResult> RevenueInfo(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id==0)
            {
                Session.RemoveAll();
                return RedirectToAction("Index", "Home");
            }
                

            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.RevenueId = Convert.ToInt32(((long)id).ToString());
            revenueRequest.revenueEntity = revenueEntity;
            string url = strBaseURL + "Revenue/GetRevenueInformation";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(revenueResponse);
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

        public async Task<ActionResult> ViewDraw(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            DrawResponse drawResponse = new DrawResponse();

            DrawRequest drawRequest = new DrawRequest();
            DrawEntity drawEntity = new DrawEntity();
            drawEntity.DrawId = Convert.ToInt64(id);
            drawRequest.drawEntity = drawEntity;
            string url = strBaseURL + "Draw/GetDraw";
            client.BaseAddress = new Uri(url);
            Common.AddSession("DrawId", id.ToString());
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, drawRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                drawResponse = JsonConvert.DeserializeObject<DrawResponse>(responseData);
                if (drawResponse.Message == string.Empty && drawResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Draws";
                    drawResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewDraw", drawResponse);
                }
                else
                {
                    TempData["LoginFailure"] = drawResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> LinkToDraw(long? id)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (id==0)
                return RedirectToAction("Index", "Home");
            
            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.RevenueId = Convert.ToInt32(((long)id).ToString());
            revenueRequest.revenueEntity = revenueEntity;
            string url = strBaseURL + "Revenue/LinkToDraw";
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
                    return View(revenueResponse);
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

        public async Task<ActionResult> DrawRevenue(long? id)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (id == 0)
                return RedirectToAction("Index", "Home");

            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.RevenueId = Convert.ToInt32(((long)id).ToString());
            revenueRequest.revenueEntity = revenueEntity;
            string url = strBaseURL + "Revenue/DrawRevenue";
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
                    return View(revenueResponse);
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

        public async Task<ActionResult> CreateRevenueTransaction(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (id==0)
                return RedirectToAction("Index", "Home");
            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
            revenueTransactionEntity.RevenueId = Convert.ToInt64(id);
            revenueTransactionEntity.RevenueTransactionId= 0;
            revenueRequest.revenueTransactionEntity = revenueTransactionEntity;
            string url = strBaseURL + "Revenue/GetRevenueTransaction";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(revenueResponse);
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

        public async Task<ActionResult> ViewRevenueTransaction(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if(id== 0)
                return RedirectToAction("Index", "Home");

            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
            revenueTransactionEntity.RevenueTransactionId = Convert.ToInt64(id);
            revenueRequest.revenueTransactionEntity = revenueTransactionEntity;
            string url = strBaseURL + "Revenue/GetRevenueTransaction";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewRevenueTransaction", revenueResponse);
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

        public async Task<ActionResult> EditRevenueTransaction(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if(id==0)
                return RedirectToAction("Index", "Home");
            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();

            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();

            revenueTransactionEntity.RevenueTransactionId= Convert.ToInt64(id);
            revenueRequest.revenueTransactionEntity = revenueTransactionEntity;

            string url = strBaseURL + "Revenue/GetRevenueTransaction";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editRevenueTransaction", revenueResponse);
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
    
    }
}