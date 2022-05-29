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
    public class HomeController : Controller
    {
        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service

        public HomeController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Home()
        {
            string url = strBaseURL + "User/LoginCheck";
            client.BaseAddress = new Uri(url);
            UserRequest userRequest = new UserRequest();
            UserEntity userEntity = new UserEntity();
            userEntity.UserName = Request.Form["txtUserName"];
            userEntity.Password = EncodePasswordToBase64(Request.Form["txtPassword"]);
            userRequest.UserEntity = userEntity;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, userRequest);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var userResponse = JsonConvert.DeserializeObject<UserResponse>(responseData);
                if (userResponse.Message == string.Empty && userResponse.ErrorMessage == string.Empty)
                {
                    Common.AddSession("UserName", userResponse.userEntity.UserName);
                    Common.AddSession("UserID", userResponse.userEntity.UserId.ToString());
                    
                    client.Dispose();
                    url = strBaseURL + "Role/GetUserRollPermissions";
                    RoleRequest roleRequest = new RoleRequest();
                    roleRequest.UserId = userResponse.userEntity.UserId;
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(url);
                    HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync(url, roleRequest);
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        var roleResponseData = httpResponseMessage.Content.ReadAsStringAsync().Result;
                        var roleResponse = JsonConvert.DeserializeObject<RoleResponse>(roleResponseData);
                        if (roleResponse.Message == string.Empty)
                        {
                            int PageName = 3;
                            Session["UserRollPermissions"] =  roleResponse.LstRolePermissionEntities;
                            userResponse.rolePermissionEntity = Common.PagePermissions(PageName);
                            userResponse.adminAndReconciliation = AdminAndReconciliation.AdminPageOrReconciliation();
                            //return RedirectToAction("Home", "Home");
                            return View(userResponse);
                        }
                        else
                        {
                            TempData["LoginFailure"] = userResponse.Message;
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        TempData["LoginFailure"] = userResponse.Message;
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["LoginFailure"] = userResponse.Message;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["LoginFailure"] = responseMessage.ToString();
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
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
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
