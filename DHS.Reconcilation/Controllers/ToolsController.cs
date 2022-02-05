using DHS.Reconcilation.Models;
using DHSEntities;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DHS.Reconcilation.Controllers
{
    public class ToolsController : Controller
    {

        HttpClient client;
        readonly string strBaseURL;
        //The URL of the WEB API Service

        public ToolsController()
        {
            strBaseURL = ConfigurationManager.AppSettings["BaseURL"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        public ActionResult ImportExpense()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ImportExpense(HttpPostedFileBase fileUpload)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            string fileName = string.Empty;
            if (fileUpload != null)
            {
                string destinationPath = string.Empty;
                fileName = System.IO.Path.GetFileName(fileUpload.FileName);
                fileName = fileName.Replace(" ", "");
                destinationPath = Path.Combine(Server.MapPath("~/Documents/Import"), fileName);
                fileUpload.SaveAs(destinationPath);

                string read = Path.GetFullPath(Path.Combine(Server.MapPath("~/Documents/Import"), fileName));
                OleDbConnection oleDbConnection = null;
                if (Path.GetExtension(read) == ".xls")
                {
                    oleDbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + read + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
                }
                else if (Path.GetExtension(read) == ".xlsx")
                {
                    oleDbConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + read + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                }
                oleDbConnection.Open();
                OleDbCommand y = new OleDbCommand();
                OleDbDataAdapter z = new OleDbDataAdapter();
                DataSet dset = new DataSet();
                y.Connection = oleDbConnection;
                y.CommandType = CommandType.Text;
                y.CommandText = "SELECT * FROM [EXPENSES$]";
                z = new OleDbDataAdapter(y);
                z.Fill(dset);

                ImportRequest importRequest = new ImportRequest();
                importRequest.dataset = dset;
                importRequest.FiscalYear = Request["FiscalYear"];
                oleDbConnection.Close();

                string url = strBaseURL + "Import/ImportExpense";
                client.BaseAddress = new Uri(url);
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, importRequest);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    ErrorMessages errorMessages = JsonConvert.DeserializeObject<ErrorMessages>(responseData);
                    if (errorMessages.Message == string.Empty)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
                else
                {
                    TempData["LoginFailure"] = responseMessage.ToString();
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                return View();
            }

        }        // GET: Tools

        [HttpGet]
        public ActionResult ImportRevenue()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ImportRevenue(HttpPostedFileBase fileUpload)
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");

            string fileName = string.Empty;
            if (fileUpload != null)
            {
                string destinationPath = string.Empty;
                fileName = System.IO.Path.GetFileName(fileUpload.FileName);
                fileName = fileName.Replace(" ", "");
                destinationPath = Path.Combine(Server.MapPath("~/Documents/Import"), fileName);
                fileUpload.SaveAs(destinationPath);

                string read = Path.GetFullPath(Path.Combine(Server.MapPath("~/Documents/Import"), fileName));
                OleDbConnection oleDbConnection = null;
                if (Path.GetExtension(read) == ".xls")
                {
                    oleDbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + read + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
                }
                else if (Path.GetExtension(read) == ".xlsx")
                {
                    oleDbConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + read + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                }
                oleDbConnection.Open();
                OleDbCommand y = new OleDbCommand();
                OleDbDataAdapter z = new OleDbDataAdapter();
                DataSet dset = new DataSet();
                y.Connection = oleDbConnection;
                y.CommandType = CommandType.Text;
                y.CommandText = "SELECT * FROM [REVENUES$]";
                z = new OleDbDataAdapter(y);
                z.Fill(dset);

                ImportRequest importRequest = new ImportRequest();
                importRequest.dataset = dset;
                importRequest.FiscalYear = Request["FiscalYear"];
                oleDbConnection.Close();

                string url = strBaseURL + "Import/ImportRevenue";
                client.BaseAddress = new Uri(url);
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, importRequest);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    ErrorMessages errorMessages = JsonConvert.DeserializeObject<ErrorMessages>(responseData);
                    if (errorMessages.Message == string.Empty)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
                else
                {
                    TempData["LoginFailure"] = responseMessage.ToString();
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                return View();
            }

        }        // GET: Tools

    }
}