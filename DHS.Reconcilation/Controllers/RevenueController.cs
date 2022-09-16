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
            //if (pageIndex == 1)
            //{
            //    if (Common.GetSession("RPageIndex") != "")
            //        pageIndex = Convert.ToInt32(Common.GetSession("RPageIndex"));
            //}
            //if (pageIndex > 1)
            //    Common.AddSession("RPageIndex", pageIndex.ToString());
            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.StatusId = 0;
            revenueEntity.AssignedTo = 0;
            revenueEntity.ProjectId = 0;
            revenueEntity.FiscalYearId = 0;
            revenueEntity.PeriodId = 0;
            revenueEntity.SourceId = 0;
            if (Common.GetSession("RAssignedTo") != "")
                revenueEntity.AssignedTo = Convert.ToInt32(Common.GetSession("RAssignedTo"));

            if (Common.GetSession("RStatusId") != "")
                revenueEntity.StatusId = Convert.ToInt32(Common.GetSession("RStatusId"));

            if (Common.GetSession("RProjectId") != "")
                revenueEntity.ProjectId = Convert.ToInt32(Common.GetSession("RProjectId"));

            if (Common.GetSession("RFiscalYearId") != "")
                revenueEntity.FiscalYearId = Convert.ToInt32(Common.GetSession("RFiscalYearId"));

            if (Common.GetSession("RPeriodId") != "")
                revenueEntity.PeriodId = Convert.ToInt32(Common.GetSession("RPeriodId"));

            if (Common.GetSession("RSourceId") != "")
                revenueEntity.SourceId = Convert.ToInt32(Common.GetSession("RSourceId"));

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
                    int PageName = 2;
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

            Common.AddSession("RAssignedTo", revenueEntity.AssignedTo.ToString());
            Common.AddSession("RStatusId", revenueEntity.StatusId.ToString());
            Common.AddSession("RProjectId", revenueEntity.ProjectId.ToString());
            Common.AddSession("RFiscalYearId", revenueEntity.FiscalYearId.ToString());
            Common.AddSession("RPeriodId", revenueEntity.PeriodId.ToString());
            Common.AddSession("RSourceId", revenueEntity.SourceId.ToString());
            //if (pageIndex == 1)
            //{
            //    if (Common.GetSession("RPageIndex") != "")
            //        pageIndex = Convert.ToInt32(Common.GetSession("RPageIndex"));
            //}
            //if (pageIndex > 1)
            //    Common.AddSession("RPageIndex", pageIndex.ToString());

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
                    int PageName = 2;
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
                    int PageName = 2;
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
                    int PageName = 2;
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
                    int PageName = 2;
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
                    int PageName = 2;
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
                    int PageName = 2;
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
                    int PageName = 2;
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
                    int PageName = 2;
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
                    int PageName = 2;
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
                    int PageName = 2;
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
                    int PageName = 2;
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
                    int PageName = 2;
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
                    int PageName = 2;
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
                    int PageName = 2;
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

        public async Task<ActionResult> ManageRevenueTransactions(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();

            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
            revenueTransactionEntity.ProjectName = string.Empty;
            revenueTransactionEntity.RevenueTransactionNumber = string.Empty;
            if (Common.GetSession("RTProjectName") != "")
                revenueTransactionEntity.ProjectName = Common.GetSession("RTProjectName");

            if (Common.GetSession("RTRevenueTransactionNumber") != "")
                revenueTransactionEntity.RevenueTransactionNumber = Common.GetSession("RTRevenueTransactionNumber");

            if (Common.GetSession("RTRevenueTypeId") != "")
                revenueTransactionEntity.RevenueTypeId = Convert.ToInt32(Common.GetSession("RTRevenueTypeId"));


            if (Common.GetSession("RTFiscalYearId") != "")
                revenueTransactionEntity.FiscalYearId = Convert.ToInt32(Common.GetSession("RTFiscalYearId"));

            revenueRequest.revenueTransactionEntity = revenueTransactionEntity;

            string url = strBaseURL + "Revenue/GetRevenueTransactionDetails";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 2;
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    revenueResponse.revenueTransactionEntity = revenueTransactionEntity;
                    revenueResponse.pagedrevenueTransactionEntities = revenueResponse.revenueTransactionEntities.ToPagedList(pageIndex, pageSize);
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
        public async Task<ActionResult> ManageRevenueTransactions(int? page, int? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
            revenueTransactionEntity.ProjectName = string.Empty;
            revenueTransactionEntity.RevenueTransactionNumber = string.Empty;
            if (Request["revenueTransactionEntity.ProjectName"] != "")
                revenueTransactionEntity.ProjectName = Request["revenueTransactionEntity.ProjectName"];

            if (Request["revenueTransactionEntity.RevenueTransactionNumber"] != "")
                revenueTransactionEntity.RevenueTransactionNumber = Request["revenueTransactionEntity.RevenueTransactionNumber"];

            if (Request["RevenueTypeId"] != "")
                revenueTransactionEntity.RevenueTypeId = Convert.ToInt32(Request["RevenueTypeId"]);

            if (Request["FiscalYearId"] != "")
                revenueTransactionEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);

            Common.AddSession("RTProjectName", revenueTransactionEntity.ProjectName);
            Common.AddSession("RTRevenueTransactionNumber", revenueTransactionEntity.RevenueTransactionNumber.ToString());
            Common.AddSession("RTRevenueTypeId", revenueTransactionEntity.RevenueTypeId.ToString());
            Common.AddSession("RTFiscalYearId", revenueTransactionEntity.FiscalYearId.ToString());

            revenueRequest.revenueTransactionEntity = revenueTransactionEntity;
            string url = strBaseURL + "Revenue/GetRevenueTransactionDetails";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 2;
                    revenueResponse.revenueTransactionEntity = revenueTransactionEntity;
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    revenueResponse.pagedrevenueTransactionEntities = revenueResponse.revenueTransactionEntities.ToPagedList(pageIndex, pageSize);
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

        public async Task<ActionResult> ManageReceivables(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.FiscalYearId = 0;
            transactionDetailEntity.TransProject = String.Empty;
            transactionDetailEntity.TransOrg = String.Empty;
            transactionDetailEntity.TransactionNumber = String.Empty;

            if (Common.GetSession("RMRTransProject") != "")
                transactionDetailEntity.TransProject = Common.GetSession("RMRTransProject");


            if (Common.GetSession("RMRTransOrg") != "")
                transactionDetailEntity.TransOrg = Common.GetSession("RMRTransOrg");

            if (Common.GetSession("RMRTransactionNumber") != "")
                transactionDetailEntity.TransactionNumber = Common.GetSession("RMRTransactionNumber");

            if (Common.GetSession("RMRFiscalYearId") != "")
                transactionDetailEntity.FiscalYearId = Convert.ToInt32(Common.GetSession("RMRFiscalYearId"));

            revenueRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Revenue/GetManageReceivables";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 2;
                    revenueResponse.transactionDetailEntity = transactionDetailEntity;
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    revenueResponse.pagedTransactionDetailEntities = revenueResponse.transactionDetailEntities.ToPagedList(pageIndex, pageSize);
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
        public async Task<ActionResult> ManageReceivables(int? page, int? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.TransProject = string.Empty;
            transactionDetailEntity.TransOrg = String.Empty;
            transactionDetailEntity.TransactionNumber = String.Empty;

            if (Request["transactionDetailEntity.TransProject"] != "")
                transactionDetailEntity.TransProject =Request["transactionDetailEntity.TransProject"];

            if (Request["transactionDetailEntity.TransOrg"] != "")
                transactionDetailEntity.TransOrg = Request["transactionDetailEntity.TransOrg"];

            if (Request["transactionDetailEntity.TransactionNumber"] != "")
                transactionDetailEntity.TransactionNumber = Request["transactionDetailEntity.TransactionNumber"];

            if (Request["FiscalYearId"] != "")
                transactionDetailEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);


            Common.AddSession("RMRTransProject", transactionDetailEntity.TransProject.ToString());
            Common.AddSession("RMRTransactionNumber", transactionDetailEntity.TransactionNumber.ToString());
            Common.AddSession("RMRTransOrg", transactionDetailEntity.TransOrg.ToString());
            Common.AddSession("RMRFiscalYearId", transactionDetailEntity.FiscalYearId.ToString());

            revenueRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Revenue/GetManageReceivables";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 2;
                    revenueResponse.transactionDetailEntity = transactionDetailEntity;
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    revenueResponse.pagedTransactionDetailEntities = revenueResponse.transactionDetailEntities.ToPagedList(pageIndex, pageSize);
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

        public async Task<ActionResult> ViewTransactionDetail(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.TransactionDetailId = Convert.ToInt64(id);
            revenueRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Revenue/GetTransactionDetail";
            client.BaseAddress = new Uri(url);
            Common.AddSession("ExpenseId", id.ToString());
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 2;
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewTransactionDetail", revenueResponse);
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

        public async Task<ActionResult> EditTransactionDetail(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            transactionDetailEntity.TransactionDetailId = Convert.ToInt64(id);
            revenueRequest.transactionDetailEntity = transactionDetailEntity;
            string url = strBaseURL + "Revenue/GetTransactionDetail";
            client.BaseAddress = new Uri(url);
            Common.AddSession("ExpenseId", id.ToString());
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 2;
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editTransactionDetail", revenueResponse);
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

        public async Task<ActionResult> CreateMissingRevenueTransaction()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
            revenueTransactionEntity.RevenueTransactionId = 0;
            revenueRequest.revenueTransactionEntity = revenueTransactionEntity;
            string url = strBaseURL + "Revenue/GetMissingRevenueTransaction";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 2;
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

        public async Task<ActionResult> ViewMissingRevenueTransaction(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");

            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
            revenueTransactionEntity.RevenueTransactionId = Convert.ToInt64(id);
            revenueRequest.revenueTransactionEntity = revenueTransactionEntity;
            string url = strBaseURL + "Revenue/GetMissingRevenueTransaction";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 2;
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewMissingRevenueTransaction", revenueResponse);
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

        public async Task<ActionResult> EditMissingRevenueTransaction(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (id == 0)
                return RedirectToAction("Index", "Home");
            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();

            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();

            revenueTransactionEntity.RevenueTransactionId = Convert.ToInt64(id);
            revenueRequest.revenueTransactionEntity = revenueTransactionEntity;

            string url = strBaseURL + "Revenue/GetMissingRevenueTransaction";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 2;
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editMissingRevenueTransaction", revenueResponse);
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

        public async Task<ActionResult> ManageMissingRevenueTransactions(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();

            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
            revenueTransactionEntity.ProjectName = string.Empty;
            revenueTransactionEntity.RevenueTransactionNumber = string.Empty;
            if (Common.GetSession("RTMProjectName") != "")
                revenueTransactionEntity.ProjectName = Common.GetSession("RTMProjectName");

            if (Common.GetSession("RTMRevenueTransactionNumber") != "")
                revenueTransactionEntity.RevenueTransactionNumber = Common.GetSession("RTMRevenueTransactionNumber");

            if (Common.GetSession("RTMRevenueTypeId") != "")
                revenueTransactionEntity.RevenueTypeId = Convert.ToInt32(Common.GetSession("RTMRevenueTypeId"));

            if (Common.GetSession("RTMFiscalYearId") != "")
                revenueTransactionEntity.FiscalYearId = Convert.ToInt32(Common.GetSession("RTMFiscalYearId"));

            revenueRequest.revenueTransactionEntity = revenueTransactionEntity;

            string url = strBaseURL + "Revenue/GetMissingRevenueTransactions";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 2;
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    revenueResponse.revenueTransactionEntity = revenueTransactionEntity;
                    revenueResponse.pagedrevenueTransactionEntities = revenueResponse.revenueTransactionEntities.ToPagedList(pageIndex, pageSize);
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
        public async Task<ActionResult> ManageMissingRevenueTransactions(int? page, int? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            RevenueResponse revenueResponse = new RevenueResponse();
            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
            revenueTransactionEntity.ProjectName = string.Empty;
            revenueTransactionEntity.RevenueTransactionNumber = string.Empty;
            if (Request["revenueTransactionEntity.ProjectName"] != "")
                revenueTransactionEntity.ProjectName = Request["revenueTransactionEntity.ProjectName"];

            if (Request["revenueTransactionEntity.RevenueTransactionNumber"] != "")
                revenueTransactionEntity.RevenueTransactionNumber = Request["revenueTransactionEntity.RevenueTransactionNumber"];

            if (Request["RevenueTypeId"] != "")
                revenueTransactionEntity.RevenueTypeId = Convert.ToInt32(Request["RevenueTypeId"]);

            if (Request["FiscalYearId"] != "")
                revenueTransactionEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);


            Common.AddSession("RTMProjectName", revenueTransactionEntity.ProjectName);
            Common.AddSession("RTMRevenueTransactionNumber", revenueTransactionEntity.RevenueTransactionNumber.ToString());
            Common.AddSession("RTMRevenueTypeId", revenueTransactionEntity.RevenueTypeId.ToString());
            Common.AddSession("RTMFiscalYearId", revenueTransactionEntity.FiscalYearId.ToString());

            revenueRequest.revenueTransactionEntity = revenueTransactionEntity;
            string url = strBaseURL + "Revenue/GetMissingRevenueTransactions";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 2;
                    revenueResponse.revenueTransactionEntity = revenueTransactionEntity;
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    revenueResponse.pagedrevenueTransactionEntities = revenueResponse.revenueTransactionEntities.ToPagedList(pageIndex, pageSize);
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

        public async Task<ActionResult> ViewRevenueTransactions(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");

            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
            revenueTransactionEntity.RevenueTransactionId = Convert.ToInt64(id);
            revenueRequest.revenueTransactionEntity = revenueTransactionEntity;
            string url = strBaseURL + "Revenue/GetExpenseRevenueTransactions";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 2;
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

        public async Task<ActionResult> ManageRevenueExpenseCompare()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
            revenueTransactionEntity.ProjectName = string.Empty;
            revenueTransactionEntity.BatchNumber = string.Empty;
            revenueTransactionEntity.FiscalYearId = 0;
            revenueTransactionEntity.Difference = 0;
            revenueRequest.revenueTransactionEntity = revenueTransactionEntity;
            string url = strBaseURL + "Revenue/GetRevenueExpenseCompare";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 2;
                    revenueResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    revenueResponse.revenueTransactionEntity = revenueTransactionEntity;
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
        public async Task<ActionResult> ManageRevenueExpenseCompare(int? page, int? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            RevenueResponse revenueResponse = new RevenueResponse();

            RevenueRequest revenueRequest = new RevenueRequest();
            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
            revenueTransactionEntity.ProjectName = string.Empty;
            revenueTransactionEntity.BatchNumber = string.Empty;
            revenueTransactionEntity.FiscalYearId = 0;
            revenueTransactionEntity.Difference = 0;

            if (Request["FiscalYearId"] != "")
                revenueTransactionEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);

            if (Request["Difference"] != "")
                revenueTransactionEntity.Difference = Convert.ToInt32(Request["Difference"]);

            revenueTransactionEntity.ProjectName = Request["revenueTransactionEntity.ProjectName"];
            revenueTransactionEntity.BatchNumber = Request["revenueTransactionEntity.BatchNumber"];
            revenueRequest.revenueTransactionEntity = revenueTransactionEntity;
            string url = strBaseURL + "Revenue/GetRevenueExpenseCompare";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueResponse = JsonConvert.DeserializeObject<RevenueResponse>(responseData);
                if (revenueResponse.Message == string.Empty && revenueResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 2;
                    revenueResponse.revenueTransactionEntity = revenueTransactionEntity;
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

    }
}