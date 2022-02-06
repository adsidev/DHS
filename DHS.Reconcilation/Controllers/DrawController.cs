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
    public class DrawController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service

        public DrawController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> ManageDraws(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            DrawResponse drawResponse = new DrawResponse();
            DrawRequest drawRequest = new DrawRequest();
            DrawEntity drawEntity = new DrawEntity();
            drawEntity.StatusId = 0;
            drawEntity.AssignedTo = 0;
            drawEntity.FiscalYearId = 0;
            drawEntity.ProjectName = string.Empty;
            if (Common.GetSession("DAssignedTo") != "")
                drawEntity.AssignedTo = Convert.ToInt32(Common.GetSession("DAssignedTo"));

            if (Common.GetSession("DStatusId") != "")
                drawEntity.StatusId = Convert.ToInt32(Common.GetSession("DStatusId"));
            
            if (Common.GetSession("DFiscalYearId") != "")
                drawEntity.FiscalYearId = Convert.ToInt32(Common.GetSession("DFiscalYearId"));

            if (Common.GetSession("DProjectName") != "")
                drawEntity.ProjectName = Common.GetSession("DProjectName");

            drawRequest.drawEntity = drawEntity;
            string url = strBaseURL + "Draw/GetDraws";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, drawRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                drawResponse = JsonConvert.DeserializeObject<DrawResponse>(responseData);
                if (drawResponse.Message == string.Empty && drawResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Draws";
                    drawResponse.drawEntity = drawEntity;
                    drawResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    drawResponse.pagedDrawEntities = drawResponse.drawEntities.ToPagedList(pageIndex, pageSize);
                    return View(drawResponse);
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

        [HttpPost]
        public async Task<ActionResult> ManageDraws(int? page, int? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            DrawResponse drawResponse = new DrawResponse();
            DrawRequest drawRequest = new DrawRequest();
            DrawEntity drawEntity   = new DrawEntity();
            drawEntity.StatusId = 0;
            drawEntity.AssignedTo = 0;
            drawEntity.FiscalYearId = 0;
            drawEntity.ProjectName = string.Empty;

            if (Request["AssignedTo"] != "")
                drawEntity.AssignedTo = Convert.ToInt32(Request["AssignedTo"]);

            if (Request["StatusId"] != "")
                drawEntity.StatusId = Convert.ToInt32(Request["StatusId"]);

            if (Request["FiscalYearId"] != "")
                drawEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);
            
            if (Request["drawEntity.ProjectName"] != "")
                drawEntity.ProjectName = Request["drawEntity.ProjectName"];

            Common.AddSession("DAssignedTo", drawEntity.AssignedTo.ToString());
            Common.AddSession("DStatusId", drawEntity.StatusId.ToString());
            Common.AddSession("DProjectName", drawEntity.ProjectName.ToString());
            Common.AddSession("DFiscalYearId", drawEntity.FiscalYearId.ToString());

            drawRequest.drawEntity = drawEntity;
            string url = strBaseURL + "Draw/GetDraws";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, drawRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                drawResponse = JsonConvert.DeserializeObject<DrawResponse>(responseData);
                if (drawResponse.Message == string.Empty && drawResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Draws";
                    drawResponse.drawEntity = drawEntity;
                    drawResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    drawResponse.pagedDrawEntities = drawResponse.drawEntities.ToPagedList(pageIndex, pageSize);
                    return View(drawResponse);
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

        public async Task<ActionResult> CreateDraw()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            DrawResponse drawResponse = new DrawResponse();

            DrawRequest drawRequest = new DrawRequest();
            DrawEntity drawEntity = new DrawEntity();
            drawEntity.DrawId = 0;
            drawRequest.drawEntity = drawEntity;
            string url = strBaseURL + "Draw/GetDraw";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, drawRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                drawResponse = JsonConvert.DeserializeObject<DrawResponse>(responseData);
                if (drawResponse.Message == string.Empty && drawResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Draws";
                    drawResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return View(drawResponse);
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

        public async Task<ActionResult> ViewDraw(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (id==0)
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

        public async Task<ActionResult> EditDraw(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if(id==0)
                return RedirectToAction("Index", "Home");
            DrawResponse drawResponse = new DrawResponse();

            DrawRequest drawRequest = new DrawRequest();
            DrawEntity drawEntity = new DrawEntity();
            drawEntity.DrawId = Convert.ToInt32(id);
            drawRequest.drawEntity = drawEntity;
            string url = strBaseURL + "Draw/GetDraw";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, drawRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                drawResponse = JsonConvert.DeserializeObject<DrawResponse>(responseData);
                if (drawResponse.Message == string.Empty && drawResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Draws";
                    drawResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editDraw", drawResponse);
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

        public async Task<ActionResult> DrawDocuments(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if(id==0)
                return RedirectToAction("Index", "Home");
            DrawResponse drawResponse = new DrawResponse();

            DrawRequest drawRequest = new DrawRequest();
            DrawEntity drawEntity = new DrawEntity();
            drawEntity.DrawId = Convert.ToInt32(((long)id).ToString());
            drawRequest.drawEntity = drawEntity;
            string url = strBaseURL + "Draw/GetDrawDocuments";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, drawRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                drawResponse = JsonConvert.DeserializeObject<DrawResponse>(responseData);
                if (drawResponse.Message == string.Empty && drawResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Expesnses";
                    drawResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(drawResponse);
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

        public async Task<ActionResult> CreateDrawDocument(int id = 0, int pid = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            DrawResponse drawResponse = new DrawResponse();
            DrawRequest drawRequest = new DrawRequest();
            DocumentEntity documentEntity = new DocumentEntity();

            documentEntity.DocumentReferenceId = pid;
            documentEntity.DocumentId = id;
            drawRequest.documentEntity = documentEntity;

            string url = strBaseURL + "Draw/GetDrawDocument";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, drawRequest);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                drawResponse = JsonConvert.DeserializeObject<DrawResponse>(responseData);
                if (drawResponse.Message == string.Empty && drawResponse.ErrorMessage == string.Empty)
                {
                    if (id == 0)
                        drawResponse.documentEntity = documentEntity;
                    string PageName = "Expesnses";
                    drawResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(drawResponse);
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

        [HttpPost]
        public async Task<ActionResult> CreateDrawDocument(HttpPostedFileBase fileUpload)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            DrawResponse drawResponse = new DrawResponse();
            DrawRequest drawRequest = new DrawRequest();

            string fileName = string.Empty;
            if (fileUpload != null)
            {
                string destinationPath = string.Empty;
                fileName = System.IO.Path.GetFileName(fileUpload.FileName);
                fileName = fileName.Replace(" ", "");
                destinationPath = System.IO.Path.Combine(Server.MapPath("~/Documents/Draw"), fileName);
                fileUpload.SaveAs(destinationPath);
            }
            else
            {
                fileName = Request["DocumentFile"];
            }
            DocumentEntity documentEntity = new DocumentEntity();
            documentEntity.DocumentId = Convert.ToInt32(Request["DocumentEntity.DocumentId"]);
            documentEntity.DocumentReferenceId = Convert.ToInt32(Request["DrawId"]);
            documentEntity.DocumentCategoryId = Convert.ToInt32(Request["DocumentCategoryId"]);
            documentEntity.DisplayOrder = Convert.ToInt32(Request["DocumentEntity.DisplayOrder"]);
            documentEntity.DocumentDescription = Request["Description"];
            documentEntity.DocumentFile = fileName;
            documentEntity.DocumentTitle = Request["DocumentEntity.DocumentTitle"];
            documentEntity.ModifiedBy = Convert.ToInt32(Common.GetSession("UserID"));
            documentEntity.DocumentTypeId = 3;
            drawRequest.documentEntity = documentEntity;

            string url = strBaseURL + "Draw/SaveDrawDocument";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, drawRequest);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                drawResponse = JsonConvert.DeserializeObject<DrawResponse>(responseData);
                if (drawResponse.Message == string.Empty && drawResponse.ErrorMessage == string.Empty)
                {
                    return RedirectToAction("DrawDocuments", "Draw", new { @id = documentEntity.DocumentReferenceId });
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

        public async Task<ActionResult> DrawInfo(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (id==0)
                return RedirectToAction("Index", "Home"); 
            DrawResponse drawResponse = new DrawResponse();

            DrawRequest drawRequest = new DrawRequest();
            DrawEntity drawEntity = new DrawEntity();
            drawEntity.DrawId = Convert.ToInt32(((long)id).ToString());
            drawRequest.drawEntity = drawEntity;
            string url = strBaseURL + "Draw/GetTransactionByDrawId";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, drawRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                drawResponse = JsonConvert.DeserializeObject<DrawResponse>(responseData);
                if (drawResponse.Message == string.Empty && drawResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Draws";
                    drawResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(drawResponse);
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

        public async Task<ActionResult> TransactionDetails(long? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            DrawResponse drawResponse = new DrawResponse();

            DrawRequest drawRequest = new DrawRequest();
            DrawEntity drawEntity = new DrawEntity();
            drawEntity.DrawId = Convert.ToInt32(((long)id).ToString());
            drawRequest.drawEntity = drawEntity;
            string url = strBaseURL + "Draw/GetTransactionByDrawId";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, drawRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                drawResponse = JsonConvert.DeserializeObject<DrawResponse>(responseData);
                if (drawResponse.Message == string.Empty && drawResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Draws";
                    drawResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(drawResponse);
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
    }
}