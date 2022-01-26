﻿using DHS.Reconcilation.Models;
using DHSEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DHS.Reconcilation.Controllers
{
    public class RoleController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service

        public RoleController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<ActionResult> Roles()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            RoleResponse roleResponse = new RoleResponse();
            HttpResponseMessage responseMessage = await GetRoles();

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                roleResponse = JsonConvert.DeserializeObject<RoleResponse>(responseData);
                if (roleResponse.Message == string.Empty && roleResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Roles";
                    roleResponse.RolePermissionEntity = Common.PagePermissions(PageName);
                    return View(roleResponse);
                }
                else
                {
                    TempData["LoginFailure"] = roleResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        private async Task<HttpResponseMessage> GetRoles()
        {
            RoleResponse roleResponse = new RoleResponse();
            string url = strBaseURL + "Role/GetRoles";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            return responseMessage;
        }

        [HttpPost]
        public ActionResult Roles(RoleResponse roleResponse)
        {
            Common.RemoveSession("RoleId");
            TempData["ID"] = Request["hdnRoleId"];
            return RedirectToAction("CreateRole");
        }

        public async Task<ActionResult> CreateRole()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            RoleResponse roleResponse = new RoleResponse();
            RoleRequest roleRequest = new RoleRequest();
            if (TempData["ID"] != null)
                roleRequest.RoleId = Convert.ToInt32(TempData["ID"]);
            else
                roleRequest.RoleId = 0;
            string url = strBaseURL + "Role/GetRolePermission";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url,roleRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                roleResponse = JsonConvert.DeserializeObject<RoleResponse>(responseData);
                if (roleResponse.Message == string.Empty && roleResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Roles";
                    roleResponse.RolePermissionEntity = Common.PagePermissions(PageName);
                    return View(roleResponse);
                }
                else
                {
                    TempData["LoginFailure"] = roleResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        private string SplitString(string checkbool)
        {
            string returnVal = string.Empty;
            string[] viewarray = checkbool.Split(',');
            for (int i = 0; i < viewarray.Length; i++)
            {
                if (viewarray[i] == "true")
                {
                    returnVal += viewarray[i] + ",";
                    i = i + 1;
                }
                else
                    returnVal += viewarray[i] + ",";

            }
            return returnVal.TrimEnd(',');
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole(RoleResponse roleResponse)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            HttpResponseMessage httpResponseMessage = await GetRoles();
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var responseData = httpResponseMessage.Content.ReadAsStringAsync().Result;
                roleResponse = JsonConvert.DeserializeObject<RoleResponse>(responseData);
                if (roleResponse.Message == string.Empty && roleResponse.ErrorMessage == string.Empty)
                {
                    List<Roles> userAccountList = roleResponse.LstRoles;
                    // check user name already present
                    if (Request.Form[0] == "0")
                    {
                        var userWithSameName = from c in userAccountList where c.RoleName == Request.Form[1] select c;
                        if (userWithSameName.Count() > 0)
                        {
                            TempData["FailureMesage"] = "Please enter another role name.";
                            return RedirectToAction("CreateRole"); 
                        }
                    }
                    if (Request.Form[0] != "0")
                    {
                        var userWithSameName = from c in userAccountList where c.RoleName == Request.Form[1] && c.RoleId != Convert.ToInt32(Request.Form[0]) select c;
                        if (userWithSameName.Count() > 0)
                        {
                            TempData["FailureMesage"] = "Please enter another role name.";
                            TempData["ID"] = Request.Form[0];
                            return RedirectToAction("CreateRole");
                        }
                    }
                }
                else
                {
                    TempData["LoginFailure"] = roleResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = httpResponseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
            

            RoleRequest roleRequest = new RoleRequest();
            RolePermissionEntity rolePermissionEntity = new RolePermissionEntity();
            roleRequest.RoleId = Convert.ToInt32(Request.Form[0]);

            rolePermissionEntity.RoleName = Request.Form[1];
            rolePermissionEntity.RoleDescription = Request.Form[2];
            rolePermissionEntity.PermissionId = Request.Form[3];
            rolePermissionEntity.View = SplitString(Request.Form[4]);
            rolePermissionEntity.Edit = SplitString(Request.Form[5]);
            rolePermissionEntity.Create = SplitString(Request.Form[6]);
            rolePermissionEntity.Delete = SplitString(Request.Form[7]);

            roleRequest.RolePermissionEntity = rolePermissionEntity;

            string url = strBaseURL + "Role/SaveRolePermission";
            client.Dispose();
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, roleRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                roleResponse = JsonConvert.DeserializeObject<RoleResponse>(responseData);
                if (roleResponse.Message == string.Empty && roleResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Roles";
                    roleResponse.RolePermissionEntity = Common.PagePermissions(PageName);
                    return View(roleResponse);
                }
                else
                {
                    TempData["LoginFailure"] = roleResponse.Message;
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