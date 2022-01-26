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
    public class UserController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service

        public UserController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: User
        public async Task<ActionResult> Users()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            UserResponse userResponse = new UserResponse();
            string url = strBaseURL + "User/GetUsers";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                userResponse = JsonConvert.DeserializeObject<UserResponse>(responseData);
                if (userResponse.Message == string.Empty && userResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Users";
                    userResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(userResponse);
                }
                else
                {
                    TempData["LoginFailure"] = userResponse.Message;
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
        public ActionResult Users(UserResponse userResponse)
        {
            TempData["ID"] = Request["hdnUserId"];
            Common.RemoveSession("UserId");
            return RedirectToAction("CreateUser");
        }

        public async Task<ActionResult> CreateUser()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            UserResponse userResponse = new UserResponse();
            string url = strBaseURL + "User/GetUser";
            client.BaseAddress = new Uri(url);
            UserRequest userRequest = new UserRequest();
            userRequest.UserId= Convert.ToInt32(TempData["ID"]);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, userRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                userResponse = JsonConvert.DeserializeObject<UserResponse>(responseData);
                if (userResponse.Message == string.Empty && userResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Users";
                    userResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    return View(userResponse);
                }
                else
                {
                    TempData["LoginFailure"] = userResponse.Message;
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
        public async Task<ActionResult> CreateUser(UserResponse userResponse)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            string url = strBaseURL + "User/SaveUser";
            client.BaseAddress = new Uri(url);
            UserRequest userRequest = new UserRequest();
            userRequest.UserId = Convert.ToInt32(Request["hdnUserId"]);
            UserEntity userEntity = new UserEntity();
            userEntity.UserName = Request["UserName"];
            userEntity.Password = EncodePasswordToBase64(Request["Password"]);
            userEntity.Email = Request["Email"];
            userEntity.RoleId = Convert.ToInt32(Request["RoleId"]);
            userRequest.UserEntity = userEntity;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, userRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                userResponse = JsonConvert.DeserializeObject<UserResponse>(responseData);
                if (userResponse.Message == string.Empty && userResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "Users";
                    userResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    Common.RemoveSession("UserId");
                    return RedirectToAction("Users");
                }
                else
                {
                    TempData["LoginFailure"] = userResponse.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        private string EncodePasswordToBase64(string password)
        {
            string encodedData = string.Empty;
            if (password == null)
                password = string.Empty;
            if (password != "")
            {
                try
                {
                    byte[] encData_byte = new byte[password.Length];
                    encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                    encodedData = Convert.ToBase64String(encData_byte);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in base64Encode" + ex.Message);
                }
            }
            return encodedData;
        } //this function Convert to Decord your Password
    }
}