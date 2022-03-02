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
        }


        [HttpGet]
        public ActionResult ImportExpenseTransaction()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ImportExpenseTransaction(HttpPostedFileBase fileUpload)
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
                string ConnectionString = string.Empty;
                string read = Path.GetFullPath(Path.Combine(Server.MapPath("~/Documents/Import"), fileName));
                OleDbConnection oleDbConnection = null;
                if (Path.GetExtension(read) == ".xls")
                {
                    ConnectionString =@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + read + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=2;'";
                }
                else if (Path.GetExtension(read) == ".xlsx")
                {
                    ConnectionString =  @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + read + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
                }
                string[] SheetName = GetExcelSheetNames(ConnectionString);

                foreach(var item in SheetName)
                {
                    oleDbConnection = new OleDbConnection(ConnectionString);
                    oleDbConnection.Open();
                    OleDbCommand y = new OleDbCommand();
                    OleDbDataAdapter z = new OleDbDataAdapter();
                    DataSet dset = new DataSet();
                    y.Connection = oleDbConnection;
                    y.CommandType = CommandType.Text;
                    y.CommandText = "SELECT * FROM [" + item + "]";
                    z = new OleDbDataAdapter(y);
                    z.Fill(dset);

                    ImportRequest importRequest = new ImportRequest();
                    importRequest.dataset = dset;
                    importRequest.FiscalYear = item.Replace("'","").Replace("$","");
                    importRequest.CreatedBy = Convert.ToInt32(Common.GetSession("UserID"));
                    oleDbConnection.Close();

                    string url = strBaseURL + "Import/ImportExpenseTransaction";
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(url);
                    HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, importRequest);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        ErrorMessages errorMessages = JsonConvert.DeserializeObject<ErrorMessages>(responseData);
                        if (errorMessages.Message == string.Empty)
                        {
                            
                        }
                        else
                        {
                            
                        }
                    }
                    else
                    {
                        
                    }
                }
                return View();
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult ImportDrawRevenueTransaction()
        {
            if (!Common.SessionExists())
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ImportDrawRevenueTransaction(HttpPostedFileBase fileUpload)
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
                string ConnectionString = string.Empty;
                string read = Path.GetFullPath(Path.Combine(Server.MapPath("~/Documents/Import"), fileName));
                OleDbConnection oleDbConnection = null;
                if (Path.GetExtension(read) == ".xls")
                {
                    ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + read + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=2;'";
                }
                else if (Path.GetExtension(read) == ".xlsx")
                {
                    ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + read + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
                }
                //string[] SheetName = GetExcelSheetNames(ConnectionString);

                oleDbConnection = new OleDbConnection(ConnectionString);
                oleDbConnection.Open();
                OleDbCommand y = new OleDbCommand();
                OleDbDataAdapter z = new OleDbDataAdapter();
                DataSet dset = new DataSet();
                y.Connection = oleDbConnection;
                y.CommandType = CommandType.Text;
                y.CommandText = "SELECT * FROM [Sheet1$]";
                z = new OleDbDataAdapter(y);
                z.Fill(dset);

                ImportRequest importRequest = new ImportRequest();
                importRequest.dataset = dset;
                importRequest.CreatedBy = Convert.ToInt32(Common.GetSession("UserID"));
                oleDbConnection.Close();

                string url = strBaseURL + "Import/ImportDrawRevenueTransaction";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(url);
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, importRequest);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    ErrorMessages errorMessages = JsonConvert.DeserializeObject<ErrorMessages>(responseData);
                    if (errorMessages.Message == string.Empty)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {

                }
                return View();
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// This method retrieves the excel sheet names from 
        /// an excel workbook.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <returns>String[]</returns>
        private String[] GetExcelSheetNames(string connString)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            try
            {
                // Create connection object by using the preceding connection string.
                objConn = new OleDbConnection(connString);
                // Open connection with the database.
                objConn.Open();
                // Get the data table containg the schema guid.
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }

                // Loop through all of the sheets if you want too...
                for (int j = 0; j < excelSheets.Length; j++)
                {
                    // Query each excel sheet.
                }

                return excelSheets;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

    }
}