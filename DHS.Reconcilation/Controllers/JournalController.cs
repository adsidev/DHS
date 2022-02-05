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
    public class JournalController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service

        public JournalController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> ManageJournals(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            JournalResponse journalResponse = new JournalResponse();
            JournalRequest journalRequest = new JournalRequest();
            string url = strBaseURL + "Journal/GetJournals";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, journalRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                journalResponse = JsonConvert.DeserializeObject<JournalResponse>(responseData);
                if (journalResponse.Message == string.Empty && journalResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Journals";
                    journalResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    journalResponse.pagedJournalEntities = journalResponse.journalEntities.ToPagedList(pageIndex, pageSize);
                    return View(journalResponse);
                }
                else
                {
                    TempData["LoginFailure"] = journalResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> CreateJournal()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            JournalResponse journalResponse = new JournalResponse();

            JournalRequest journalRequest = new JournalRequest();
            JournalEntity journalEntity = new JournalEntity();
            journalEntity.JournalId = 0;
            journalRequest.journalEntity = journalEntity;
            string url = strBaseURL + "Journal/GetJournal";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, journalRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                journalResponse = JsonConvert.DeserializeObject<JournalResponse>(responseData);
                if (journalResponse.Message == string.Empty && journalResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Journals";
                    journalResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return View(journalResponse);
                }
                else
                {
                    TempData["LoginFailure"] = journalResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> ViewJournal(long id)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            JournalResponse journalResponse = new JournalResponse();

            JournalRequest journalRequest = new JournalRequest();
            JournalEntity journalEntity = new JournalEntity();
            journalEntity.JournalId = Convert.ToInt64(id);
            journalRequest.journalEntity = journalEntity;
            string url = strBaseURL + "Journal/GetJournal";
            client.BaseAddress = new Uri(url);
            Common.AddSession("JournalId", id.ToString());
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, journalRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                journalResponse = JsonConvert.DeserializeObject<JournalResponse>(responseData);
                if (journalResponse.Message == string.Empty && journalResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Journals";
                    journalResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewJournal", journalResponse);
                }
                else
                {
                    TempData["LoginFailure"] = journalResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> EditJournal()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            JournalResponse journalResponse = new JournalResponse();

            JournalRequest journalRequest = new JournalRequest();
            JournalEntity journalEntity = new JournalEntity();
            journalEntity.JournalId = Convert.ToInt32(Common.GetSession("JournalId"));
            journalRequest.journalEntity = journalEntity;
            string url = strBaseURL + "Journal/GetJournal";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, journalRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                journalResponse = JsonConvert.DeserializeObject<JournalResponse>(responseData);
                if (journalResponse.Message == string.Empty && journalResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Journals";
                    journalResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editJournal", journalResponse);
                }
                else
                {
                    TempData["LoginFailure"] = journalResponse.Message;
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