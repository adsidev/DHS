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
    public class ProjectGroupController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service
        ProjectResponse projectResponse;

        public ProjectGroupController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            projectResponse = new ProjectResponse();
        }

        // GET: Status
        public async Task<ActionResult> ManageProjectGroup()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            string url = strBaseURL + "ProjectGroup/GetProjectGroups";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(responseData);
                if (projectResponse.Message == string.Empty && projectResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 19;
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

        public async Task<ActionResult> CreateProjectGroup(int? id =0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            string url = strBaseURL + "ProjectGroup/GetProjectGroup";
            client.BaseAddress = new Uri(url);
            ProjectRequest projectRequest = new ProjectRequest();
            ProjectGroupEntity projectGroupEntity = new ProjectGroupEntity();
            projectGroupEntity.ProjectGroupId= Convert.ToInt32(id);
            projectRequest.projectGroupEntity = projectGroupEntity;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, projectRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(responseData);
                if (projectResponse.Message == string.Empty && projectResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 19;
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


        public async Task<ActionResult> ViewProjectGroup(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            ProjectRequest projectRequest = new ProjectRequest();
            ProjectGroupEntity projectGroupEntity = new ProjectGroupEntity();

            projectGroupEntity.ProjectGroupId = Convert.ToInt32(id);
            projectRequest.projectGroupEntity = projectGroupEntity;
            string url = strBaseURL + "ProjectGroup/GetProjectGroup";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, projectRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(responseData);
                if (projectResponse.Message == string.Empty && projectResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 19;
                    projectResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewProjectGroup", projectResponse);
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

        public async Task<ActionResult> EditProjectGroup(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            ProjectRequest projectRequest = new ProjectRequest();
            ProjectGroupEntity projectGroupEntity = new ProjectGroupEntity();

            projectGroupEntity.ProjectGroupId = Convert.ToInt32(id);
            projectRequest.projectGroupEntity = projectGroupEntity;
            string url = strBaseURL + "ProjectGroup/GetProjectGroup";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, projectRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(responseData);
                if (projectResponse.Message == string.Empty && projectResponse.ErrorMessage == string.Empty)
                {
                    int PageName = 19;
                    projectResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_editProjectGroup", projectResponse);
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