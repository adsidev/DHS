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
    public class VendorController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service

        public VendorController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> ManageVendors(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            VendorResponse vendorResponse = new VendorResponse();
            VendorRequest vendorRequest = new VendorRequest();
            string url = strBaseURL + "Vendor/GetVendors";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, vendorRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                vendorResponse = JsonConvert.DeserializeObject<VendorResponse>(responseData);
                if (vendorResponse.Message == string.Empty && vendorResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Vendor";
                    vendorResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    vendorResponse.pagedVendorEntities = vendorResponse.vendorEntities.ToPagedList(pageIndex, pageSize);
                    return View(vendorResponse);
                }
                else
                {
                    TempData["LoginFailure"] = vendorResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> CreateVendor()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            VendorResponse vendorResponse = new VendorResponse();

            VendorRequest vendorRequest = new VendorRequest();
            VendorEntity vendorEntity = new VendorEntity();
            vendorEntity.VendorId = 0;
            vendorRequest.vendorEntity = vendorEntity;
            string url = strBaseURL + "Vendor/GetVendor";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, vendorRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                vendorResponse = JsonConvert.DeserializeObject<VendorResponse>(responseData);
                if (vendorResponse.Message == string.Empty && vendorResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Vendor";
                    vendorResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return View(vendorResponse);
                }
                else
                {
                    TempData["LoginFailure"] = vendorResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> ViewVendor(long id)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            VendorResponse vendorResponse = new VendorResponse();

            VendorRequest vendorRequest = new VendorRequest();
            VendorEntity vendorEntity = new VendorEntity();
            vendorEntity.VendorId = Convert.ToInt32(id);
            vendorRequest.vendorEntity = vendorEntity;
            string url = strBaseURL + "Vendor/GetVendor";
            client.BaseAddress = new Uri(url);
            Common.AddSession("VendorId", id.ToString());
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, vendorRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                vendorResponse = JsonConvert.DeserializeObject<VendorResponse>(responseData);
                if (vendorResponse.Message == string.Empty && vendorResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Vendor";
                    vendorResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewVendor", vendorResponse);
                }
                else
                {
                    TempData["LoginFailure"] = vendorResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> EditVendor(long id)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            VendorResponse vendorResponse = new VendorResponse();

            VendorRequest vendorRequest = new VendorRequest();
            VendorEntity vendorEntity = new VendorEntity();
            vendorEntity.VendorId = Convert.ToInt32(id);
            vendorRequest.vendorEntity = vendorEntity;
            string url = strBaseURL + "Vendor/GetVendor";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, vendorRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                vendorResponse = JsonConvert.DeserializeObject<VendorResponse>(responseData);
                if (vendorResponse.Message == string.Empty && vendorResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Vendor";
                    vendorResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editVendor", vendorResponse);
                }
                else
                {
                    TempData["LoginFailure"] = vendorResponse.Message;
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