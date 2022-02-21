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
    public class ProjectController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service
        ProjectResponse projectResponse;
        public ProjectController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            projectResponse = new ProjectResponse();
        }

        public async Task<ActionResult> ManageProjects(int? page)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            int pageSize = Common.pageNumbers;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ProjectRequest ProjectRequest = new ProjectRequest();
            string url = strBaseURL + "Project/GetProjects";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, ProjectRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(responseData);
                if (projectResponse.Message == string.Empty && projectResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Project";
                    projectResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    projectResponse.pagedProjectEntities = projectResponse.projectEntities.ToPagedList(pageIndex, pageSize);
                    return View(projectResponse);
                }
                else
                {
                    TempData["LoginFailure"] = projectResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<ActionResult> ManageProjectSummary()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            ProjectEntity projectEntity = new ProjectEntity();
            projectEntity.FiscalYearId =0;
            ProjectRequest projectRequest = new ProjectRequest();
            projectRequest.projectEntity = projectEntity;
            string url = strBaseURL + "Project/GetProjectResponse";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, projectRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(responseData);
                if (projectResponse.Message == string.Empty && projectResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Project";
                    projectResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    projectResponse.projectEntity = projectEntity;
                    return View(projectResponse);
                }
                else
                {
                    TempData["LoginFailure"] = projectResponse.Message;
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
        public async Task<ActionResult> ManageProjectSummary(int? id=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            ProjectEntity projectEntity = new ProjectEntity();
            projectEntity.FiscalYearId = Convert.ToInt32(Request["FiscalYearId"]);
            ProjectRequest projectRequest = new ProjectRequest();
            projectRequest.projectEntity = projectEntity;
            string url = strBaseURL + "Project/GetProjectResponse";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, projectRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(responseData);
                if (projectResponse.Message == string.Empty && projectResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Project";
                    projectResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    projectResponse.projectEntity = projectEntity;
                    return View(projectResponse);
                }
                else
                {
                    TempData["LoginFailure"] = projectResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> ManageProjectDetails(long? id=0, long? pid=0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            if (id==0)
                return RedirectToAction("Index", "Home");

            ProjectEntity projectEntity = new ProjectEntity();
            projectEntity.ProjectId = Convert.ToInt64(id);
            projectEntity.FiscalYearId = Convert.ToInt64(pid);
            ProjectRequest projectRequest = new ProjectRequest();
            projectRequest.projectEntity = projectEntity;
            string url = strBaseURL + "Project/GetProjectDetails";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, projectRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(responseData);
                if (projectResponse.Message == string.Empty && projectResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Project";
                    projectResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(projectResponse);
                }
                else
                {
                    TempData["LoginFailure"] = projectResponse.Message;
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