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
    public class FGTCategoryController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service

        public FGTCategoryController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<ActionResult> ManageFGTCategories(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            FGTCategoryResponse FGTCategoryResponse = new FGTCategoryResponse();
            FGTCategoryRequest FGTCategoryRequest = new FGTCategoryRequest();
            string url = strBaseURL + "FGTCategory/GetFGTCategories";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, FGTCategoryRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                FGTCategoryResponse = JsonConvert.DeserializeObject<FGTCategoryResponse>(responseData);
                if (FGTCategoryResponse.Message == string.Empty && FGTCategoryResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "FGTCategories";
                    FGTCategoryResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    FGTCategoryResponse.pagedFGTCategoryEntities = FGTCategoryResponse.fGTCategoryEntities.ToPagedList(pageIndex, pageSize);
                    return View(FGTCategoryResponse);
                }
                else
                {
                    TempData["LoginFailure"] = FGTCategoryResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> CreateFGTCategory()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            FGTCategoryResponse FGTCategoryResponse = new FGTCategoryResponse();

            FGTCategoryRequest FGTCategoryRequest = new FGTCategoryRequest();
            FGTCategoryEntity fGTCategoryEntity = new FGTCategoryEntity();
            fGTCategoryEntity.FGTCategoryId = 0;
            FGTCategoryRequest.fGTCategoryEntity = fGTCategoryEntity;
            string url = strBaseURL + "FGTCategory/GetFGTCategory";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, FGTCategoryRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                FGTCategoryResponse = JsonConvert.DeserializeObject<FGTCategoryResponse>(responseData);
                if (FGTCategoryResponse.Message == string.Empty && FGTCategoryResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "FGTCategories";
                    FGTCategoryResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return View(FGTCategoryResponse);
                }
                else
                {
                    TempData["LoginFailure"] = FGTCategoryResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> ViewFGTCategory(long id)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            FGTCategoryResponse FGTCategoryResponse = new FGTCategoryResponse();

            FGTCategoryRequest FGTCategoryRequest = new FGTCategoryRequest();
            FGTCategoryEntity fGTCategoryEntity = new FGTCategoryEntity();
            fGTCategoryEntity.FGTCategoryId = Convert.ToInt64(id);
            FGTCategoryRequest.fGTCategoryEntity = fGTCategoryEntity;
            string url = strBaseURL + "FGTCategory/GetFGTCategory";
            client.BaseAddress = new Uri(url);
            Common.AddSession("FGTCategoryId", id.ToString());
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, FGTCategoryRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                FGTCategoryResponse = JsonConvert.DeserializeObject<FGTCategoryResponse>(responseData);
                if (FGTCategoryResponse.Message == string.Empty && FGTCategoryResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "FGTCategories";
                    FGTCategoryResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewFGTCategory", FGTCategoryResponse);
                }
                else
                {
                    TempData["LoginFailure"] = FGTCategoryResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> EditFGTCategory()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            FGTCategoryResponse FGTCategoryResponse = new FGTCategoryResponse();

            FGTCategoryRequest FGTCategoryRequest = new FGTCategoryRequest();
            FGTCategoryEntity fGTCategoryEntity = new FGTCategoryEntity();
            fGTCategoryEntity.FGTCategoryId = Convert.ToInt32(Common.GetSession("FGTCategoryId"));
            FGTCategoryRequest.fGTCategoryEntity = fGTCategoryEntity;
            string url = strBaseURL + "FGTCategory/GetFGTCategory";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, FGTCategoryRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                FGTCategoryResponse = JsonConvert.DeserializeObject<FGTCategoryResponse>(responseData);
                if (FGTCategoryResponse.Message == string.Empty && FGTCategoryResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "FGTCategories";
                    FGTCategoryResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editFGTCategory", FGTCategoryResponse);
                }
                else
                {
                    TempData["LoginFailure"] = FGTCategoryResponse.Message;
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