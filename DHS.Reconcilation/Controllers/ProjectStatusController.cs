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
    public class ProjectStatusController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service
        ProjectResponse projectResponse;

        public ProjectStatusController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            projectResponse = new ProjectResponse();
        }

        // GET: Status
        public async Task<ActionResult> ManageProjectStatus()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            string url = strBaseURL + "ProjectStatus/GetProjectStatuses";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(responseData);
                if (projectResponse.Message == string.Empty && projectResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 18;
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

        public async Task<ActionResult> CreateProjectStatus(int? id =0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            string url = strBaseURL + "ProjectStatus/GetProjectStatus";
            client.BaseAddress = new Uri(url);
            ProjectRequest projectRequest = new ProjectRequest();
            ProjectStatusEntity projectStatusEntity = new ProjectStatusEntity();
            projectStatusEntity.ProjectStatusId= Convert.ToInt32(id);
            projectRequest.projectStatusEntity = projectStatusEntity;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, projectRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(responseData);
                if (projectResponse.Message == string.Empty && projectResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 18;
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


        public async Task<ActionResult> ViewProjectStatus(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            ProjectRequest projectRequest = new ProjectRequest();
            ProjectStatusEntity projectStatusEntity = new ProjectStatusEntity();

            projectStatusEntity.ProjectStatusId = Convert.ToInt32(id);
            projectRequest.projectStatusEntity = projectStatusEntity;
            string url = strBaseURL + "ProjectStatus/GetProjectStatus";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, projectRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(responseData);
                if (projectResponse.Message == string.Empty && projectResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 18;
                    projectResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewProjectStatus", projectResponse);
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

        public async Task<ActionResult> EditProjectStatus(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            ProjectRequest projectRequest = new ProjectRequest();
            ProjectStatusEntity projectStatusEntity = new ProjectStatusEntity();

            projectStatusEntity.ProjectStatusId = Convert.ToInt32(id);
            projectRequest.projectStatusEntity = projectStatusEntity;
            string url = strBaseURL + "ProjectStatus/GetProjectStatus";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, projectRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(responseData);
                if (projectResponse.Message == string.Empty && projectResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 18;
                    projectResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editProjectStatus", projectResponse);
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