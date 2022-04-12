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
        UserResponse userResponse;
        public UserController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            userResponse = new UserResponse();
        }

        // GET: User
        public async Task<ActionResult> Users()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

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

        public async Task<ActionResult> CreateUser()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            string url = strBaseURL + "User/GetUser";
            client.BaseAddress = new Uri(url);
            UserRequest userRequest = new UserRequest();
            UserEntity userEntity = new UserEntity();
            userEntity.UserId= Convert.ToInt32(0);
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
            UserEntity userEntity = new UserEntity();
            userEntity.UserId = Convert.ToInt32(Request["hdnUserId"]);
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

        private string DecodeFrom64(string encodedData)
        {
            string result = string.Empty;
            if (encodedData != "")
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encodedData);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                result = new String(decoded_char);
            }
            return result;

        }


        public async Task<ActionResult> ViewUser(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            UserRequest userRequest = new UserRequest();
            UserEntity userEntity = new UserEntity();

            userEntity.UserId = Convert.ToInt32(id);
            userRequest.UserEntity = userEntity;
            string url = strBaseURL + "User/GetUser";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, userRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                userResponse = JsonConvert.DeserializeObject<UserResponse>(responseData);
                if (userResponse.Message == string.Empty && userResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "User";
                    userResponse.rolePermissionEntity = Common.PagePermissions(PageName);

                    return PartialView("_viewUser", userResponse);
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

        public async Task<ActionResult> EditUser(long? id = 0)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            if (id == 0)
                return RedirectToAction("Index", "Home");
            UserRequest userRequest = new UserRequest();
            UserEntity userEntity = new UserEntity();

            userEntity.UserId = Convert.ToInt32(id);
            userRequest.UserEntity = userEntity;
            string url = strBaseURL + "User/GetUser";
            client.BaseAddress = new Uri(url);

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, userRequest);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                userResponse = JsonConvert.DeserializeObject<UserResponse>(responseData);
                if (userResponse.Message == string.Empty && userResponse.ErrorMessage == string.Empty)
                {
                    string PageName = "User";
                    userResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                    userResponse.userEntity.Password = DecodeFrom64(userResponse.userEntity.Password);
                    return PartialView("_editUser", userResponse);
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
    }
}