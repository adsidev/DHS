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
    public class RevenueTypeController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service

        public RevenueTypeController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> ManageRevenueTypes(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            RevenueTypeResponse revenueTypeResponse = new RevenueTypeResponse();
            RevenueTypeRequest revenueTypeRequest = new RevenueTypeRequest();
            string url = strBaseURL + "RevenueType/GetRevenueTypes";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueTypeRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueTypeResponse = JsonConvert.DeserializeObject<RevenueTypeResponse>(responseData);
                if (revenueTypeResponse.Message == string.Empty && revenueTypeResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "RevenueType";
                    revenueTypeResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    revenueTypeResponse.pagedRevenueTypeEntities = revenueTypeResponse.RevenueTypeEntities.ToPagedList(pageIndex, pageSize);
                    return View(revenueTypeResponse);
                }
                else
                {
                    TempData["LoginFailure"] = revenueTypeResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> CreateRevenueType()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            RevenueTypeResponse revenueTypeResponse = new RevenueTypeResponse();

            RevenueTypeRequest revenueTypeRequest = new RevenueTypeRequest();
            RevenueTypeEntity revenueTypeEntity = new RevenueTypeEntity();
            revenueTypeEntity.RevenueTypeId = 0;
            revenueTypeRequest.RevenueTypeEntity = revenueTypeEntity;
            string url = strBaseURL + "RevenueType/GetRevenueType";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueTypeRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueTypeResponse = JsonConvert.DeserializeObject<RevenueTypeResponse>(responseData);
                if (revenueTypeResponse.Message == string.Empty && revenueTypeResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "RevenueType";
                    revenueTypeResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return View(revenueTypeResponse);
                }
                else
                {
                    TempData["LoginFailure"] = revenueTypeResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> ViewRevenueType(long id)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            RevenueTypeResponse revenueTypeResponse = new RevenueTypeResponse();

            RevenueTypeRequest revenueTypeRequest = new RevenueTypeRequest();
            RevenueTypeEntity revenueTypeEntity = new RevenueTypeEntity();
            revenueTypeEntity.RevenueTypeId = Convert.ToInt32(id);
            revenueTypeRequest.RevenueTypeEntity = revenueTypeEntity;
            string url = strBaseURL + "RevenueType/GetRevenueType";
            client.BaseAddress = new Uri(url);
            Common.AddSession("RevenueTypeId", id.ToString());
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueTypeRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueTypeResponse = JsonConvert.DeserializeObject<RevenueTypeResponse>(responseData);
                if (revenueTypeResponse.Message == string.Empty && revenueTypeResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "RevenueType";
                    revenueTypeResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewRevenueType", revenueTypeResponse);
                }
                else
                {
                    TempData["LoginFailure"] = revenueTypeResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> EditRevenueType(long id)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            RevenueTypeResponse revenueTypeResponse = new RevenueTypeResponse();

            RevenueTypeRequest revenueTypeRequest = new RevenueTypeRequest();
            RevenueTypeEntity revenueTypeEntity = new RevenueTypeEntity();
            revenueTypeEntity.RevenueTypeId = Convert.ToInt32(id);
            revenueTypeRequest.RevenueTypeEntity = revenueTypeEntity;
            string url = strBaseURL + "RevenueType/GetRevenueType";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, revenueTypeRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                revenueTypeResponse = JsonConvert.DeserializeObject<RevenueTypeResponse>(responseData);
                if (revenueTypeResponse.Message == string.Empty && revenueTypeResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "RevenueType";
                    revenueTypeResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editRevenueType", revenueTypeResponse);
                }
                else
                {
                    TempData["LoginFailure"] = revenueTypeResponse.Message;
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