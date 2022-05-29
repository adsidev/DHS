using DHS.Reconcilation.Models;
using DHSEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DHS.Reconcilation.Controllers
{
    public class StatusController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service
        StatusResponse statusResponse;

        public StatusController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            statusResponse = new StatusResponse();
        }

        // GET: Status
        public async Task<ActionResult> ManageStatus()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            string url = strBaseURL + "Status/GetStatuses";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                statusResponse = JsonConvert.DeserializeObject<StatusResponse>(responseData);
                if (statusResponse.Message == string.Empty && statusResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 14;
                    statusResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(statusResponse);
                }
                else
                {
                    TempData["LoginFailure"] = statusResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> CreateStatus(int? id =0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            string url = strBaseURL + "Status/GetStatus";
            client.BaseAddress = new Uri(url);
            StatusRequest statusRequest = new StatusRequest();
            StatusEntity statusEntity = new StatusEntity();
            statusEntity.StatusId= Convert.ToInt32(id);
            statusRequest.statusEntity = statusEntity;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, statusRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                statusResponse = JsonConvert.DeserializeObject<StatusResponse>(responseData);
                if (statusResponse.Message == string.Empty && statusResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 14;
                    statusResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(statusResponse);
                }
                else
                {
                    TempData["LoginFailure"] = statusResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }


        public async Task<ActionResult> ViewStatus(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            StatusRequest statusRequest = new StatusRequest();
            StatusEntity statusEntity = new StatusEntity();

            statusEntity.StatusId = Convert.ToInt32(id);
            statusRequest.statusEntity = statusEntity;
            string url = strBaseURL + "Status/GetStatus";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, statusRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                statusResponse = JsonConvert.DeserializeObject<StatusResponse>(responseData);
                if (statusResponse.Message == string.Empty && statusResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 14;
                    statusResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewStatus", statusResponse);
                }
                else
                {
                    TempData["LoginFailure"] = statusResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> EditStatus(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            StatusRequest statusRequest = new StatusRequest();
            StatusEntity statusEntity = new StatusEntity();

            statusEntity.StatusId = Convert.ToInt32(id);
            statusRequest.statusEntity = statusEntity;
            string url = strBaseURL + "Status/GetStatus";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, statusRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                statusResponse = JsonConvert.DeserializeObject<StatusResponse>(responseData);
                if (statusResponse.Message == string.Empty && statusResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 14;
                    statusResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editStatus", statusResponse);
                }
                else
                {
                    TempData["LoginFailure"] = statusResponse.Message;
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