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
    public class FiscalYearController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service
        FiscalYearResponse fiscalYearResponse;

        public FiscalYearController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            fiscalYearResponse = new FiscalYearResponse();
        }

        // GET: FiscalYear
        public async Task<ActionResult> ManageFiscalYears()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            string url = strBaseURL + "FiscalYear/GetFiscalYears";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                fiscalYearResponse = JsonConvert.DeserializeObject<FiscalYearResponse>(responseData);
                if (fiscalYearResponse.Message == string.Empty && fiscalYearResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 13;
                    fiscalYearResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(fiscalYearResponse);
                }
                else
                {
                    TempData["LoginFailure"] = fiscalYearResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> CreateFiscalYear(int? id =0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            string url = strBaseURL + "FiscalYear/GetFiscalYear";
            client.BaseAddress = new Uri(url);
            FiscalYearRequest fiscalYearRequest = new FiscalYearRequest();
            FiscalYearEntity fiscalYearEntity = new FiscalYearEntity();
            fiscalYearEntity.FiscalYearId= Convert.ToInt32(id);
            fiscalYearRequest.fiscalYearEntity = fiscalYearEntity;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, fiscalYearRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                fiscalYearResponse = JsonConvert.DeserializeObject<FiscalYearResponse>(responseData);
                if (fiscalYearResponse.Message == string.Empty && fiscalYearResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 13;
                    fiscalYearResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(fiscalYearResponse);
                }
                else
                {
                    TempData["LoginFailure"] = fiscalYearResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }


        public async Task<ActionResult> ViewFiscalYear(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            FiscalYearRequest fiscalYearRequest = new FiscalYearRequest();
            FiscalYearEntity fiscalYearEntity = new FiscalYearEntity();

            fiscalYearEntity.FiscalYearId = Convert.ToInt32(id);
            fiscalYearRequest.fiscalYearEntity = fiscalYearEntity;
            string url = strBaseURL + "FiscalYear/GetFiscalYear";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, fiscalYearRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                fiscalYearResponse = JsonConvert.DeserializeObject<FiscalYearResponse>(responseData);
                if (fiscalYearResponse.Message == string.Empty && fiscalYearResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 13;
                    fiscalYearResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewFiscalYear", fiscalYearResponse);
                }
                else
                {
                    TempData["LoginFailure"] = fiscalYearResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> EditFiscalYear(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            FiscalYearRequest fiscalYearRequest = new FiscalYearRequest();
            FiscalYearEntity fiscalYearEntity = new FiscalYearEntity();

            fiscalYearEntity.FiscalYearId = Convert.ToInt32(id);
            fiscalYearRequest.fiscalYearEntity = fiscalYearEntity;
            string url = strBaseURL + "FiscalYear/GetFiscalYear";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, fiscalYearRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                fiscalYearResponse = JsonConvert.DeserializeObject<FiscalYearResponse>(responseData);
                if (fiscalYearResponse.Message == string.Empty && fiscalYearResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 13;
                    fiscalYearResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editFiscalYear", fiscalYearResponse);
                }
                else
                {
                    TempData["LoginFailure"] = fiscalYearResponse.Message;
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