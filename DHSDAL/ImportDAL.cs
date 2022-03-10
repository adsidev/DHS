using DataAccessLayer;
using DHSEntities;
using System;
using System.Data;

namespace DHSDAL
{
    public class ImportDAL : IImportDALRepository
    {
        private readonly string _connectionString;

        public ImportDAL()
        {
            _connectionString = Constant.MRAConnectionString;
        }

        public ErrorMessages ImportExpense(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            errorMessages.Message = string.Empty;
            var expenseDataSet = importRequest.dataset;
            try
            {
                foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
                {
                    if(expenseDataRow["PER"].ToString()!="")
                    {
                        SqlObject.Parameters = new object[] {
                        expenseDataRow["PER"].ToString(),
                        expenseDataRow["JNL"].ToString(),
                        expenseDataRow["SRC"].ToString(),
                        expenseDataRow["FUND"].ToString(),
                        expenseDataRow["ORG"].ToString(),
                        expenseDataRow["FUNC"].ToString(),
                        expenseDataRow["FUNCTION DESCRIPTION"].ToString(),
                        expenseDataRow["DEPT"].ToString(),
                        expenseDataRow["DEPARTMENT DESCRIPTION"].ToString(),
                        expenseDataRow["ACTIVITY"].ToString(),
                        expenseDataRow["ACTIVITY  DESCRIPTION"].ToString(),
                        expenseDataRow["OBJECT"].ToString(),
                        expenseDataRow["OBJECT DESCRIPTION"].ToString(),
                        expenseDataRow["PROJ"].ToString(),
                        expenseDataRow["PROJECT TITLE"].ToString(),
                        expenseDataRow["CFDA"].ToString(),
                        expenseDataRow["REF-1"].ToString(),
                        expenseDataRow["REF-2"].ToString(),
                        expenseDataRow["REF-3"].ToString(),
                        expenseDataRow["REF-4"].ToString(),
                        expenseDataRow["NET"].ToString(),
                        importRequest.FiscalYear
                        };
                        var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Import.USPIMPORTEXPENSE, SqlObject.Parameters);
                    }                    
                }
            }
            catch (System.Exception ex)
            {
                errorMessages.Message = ex.Message;
                errorMessages.Exception = ex;
            }            
            return errorMessages;
        }

        public ErrorMessages ImportRevenue(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            errorMessages.Message = string.Empty;
            var expenseDataSet = importRequest.dataset;
            try
            {
                foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
                {
                    if (expenseDataRow["PER"].ToString() != "")
                    {
                        SqlObject.Parameters = new object[] {
                        expenseDataRow["PER"].ToString(),
                        expenseDataRow["JNL"].ToString(),
                        expenseDataRow["SRC"].ToString(),
                        expenseDataRow["FUND"].ToString(),
                        expenseDataRow["ORG"].ToString(),
                        expenseDataRow["FUNC"].ToString(),
                        expenseDataRow["FUNCTION DESCRIPTION"].ToString(),
                        expenseDataRow["DEPT"].ToString(),
                        expenseDataRow["DEPARTMENT DESCRIPTION"].ToString(),
                        expenseDataRow["ACTIVITY"].ToString(),
                        expenseDataRow["ACTIVITY  DESCRIPTION"].ToString(),
                        expenseDataRow["OBJECT"].ToString(),
                        expenseDataRow["OBJECT DESCRIPTION"].ToString(),
                        expenseDataRow["PROJ"].ToString(),
                        expenseDataRow["PROJECT TITLE"].ToString(),
                        expenseDataRow["CFDA"].ToString(),
                        expenseDataRow["REF-1"].ToString(),
                        expenseDataRow["REF-2"].ToString(),
                        expenseDataRow["REF-3"].ToString(),
                        expenseDataRow["REF-4"].ToString(),
                        expenseDataRow["NET"].ToString(),
                        importRequest.FiscalYear
                        };
                        var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Import.USPIMPORTREVENUE, SqlObject.Parameters);
                    }
                }
            }
            catch (System.Exception ex)
            {
                errorMessages.Message = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }
                
        public ErrorMessages CheckImportExpense(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            errorMessages.Message = string.Empty;
            SqlObject.Parameters = new object[] {
                    importRequest.FiscalYear
            };
            var drawDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Import.USPCHECKIMPORTEXPENSE, SqlObject.Parameters);
            if(drawDataSet.Tables[0].Rows.Count > 0)
            {
                errorMessages.Message = "Records already exists.";
            }
            return errorMessages;
        }

        public ErrorMessages CheckImportRevenue(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            errorMessages.Message = string.Empty;
            SqlObject.Parameters = new object[] {
                    importRequest.FiscalYear
            };
            var drawDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Import.USPCHECKIMPORTREVENUE, SqlObject.Parameters);
            if (drawDataSet.Tables[0].Rows.Count > 0)
            {
                errorMessages.Message = "Records already exists.";
            }
            return errorMessages;
        }

        public ErrorMessages ImportExpenseTransaction(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            errorMessages.Message = string.Empty;
            var expenseDataSet = importRequest.dataset;
            try
            {
                decimal TotalPrice = Convert.ToDecimal(expenseDataSet.Tables[0].Compute("SUM(Amount)", string.Empty));
                string ImportNumber = DateTime.Now.ToString();
                foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
                {
                    SqlObject.Parameters = new object[] {
                        expenseDataRow["Effective Date"].ToString(),
                        expenseDataRow["Amount"].ToString(),
                        expenseDataRow["Vendor"].ToString(),
                        expenseDataRow["Org"].ToString(),
                        expenseDataRow["Category ID"].ToString(),
                        expenseDataRow["Batch #"].ToString(),
                        expenseDataRow["Org Amount from Draw"].ToString(),
                        expenseDataRow["Notes"].ToString(),
                        importRequest.FiscalYear,
                        importRequest.CreatedBy,
                        expenseDataRow["Project"].ToString(),
                        TotalPrice,
                        ImportNumber
                        };
                    var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Import.USPIMPORTEXPENSETRANSACTION, SqlObject.Parameters);
                }
            }
            catch (System.Exception ex)
            {
                errorMessages.Message = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }

        public ErrorMessages ImportDrawRevenueTransaction(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            errorMessages.Message = string.Empty;
            var expenseDataSet = importRequest.dataset;
            try
            {
                foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
                {
                    var PostDate = expenseDataRow["POST DATE"].ToString();
                    string[] PostD = PostDate.Split(' ')[0].Split('-');
                    if (PostD.Length > 1)
                        PostDate = PostD[1] + "/" + PostD[0] + "/" + PostD[2];
                    else
                        PostDate = PostD[0];
                    var EntryDate = expenseDataRow["ENTRY DATE"].ToString();
                    string[] EntD = EntryDate.Split(' ')[0].Split('-');
                    if(EntD.Length > 1)
                    EntryDate = EntD[1] + "/" + EntD[0] + "/" + EntD[2];
                    else
                        EntryDate = EntD[0];
                    var BatchTotal = expenseDataRow["BATCH TOTAL"].ToString();
                    if (BatchTotal == "")
                        BatchTotal = "0";
                    SqlObject.Parameters = new object[] {
                        expenseDataRow["YEAR"].ToString(),
                        expenseDataRow["PERIOD"].ToString(),
                        expenseDataRow["JOURNAL"].ToString(),
                        expenseDataRow["LINE"].ToString(),
                        expenseDataRow["FUND"].ToString(),
                        expenseDataRow["ORG"].ToString(),
                        expenseDataRow["OBJECT"].ToString(),
                        expenseDataRow["PROJECT"].ToString(),
                        expenseDataRow["CLERK"].ToString(),
                        expenseDataRow["REF 1"].ToString(),
                        expenseDataRow["REF 2"].ToString(),
                        expenseDataRow["REF 3"].ToString(),
                        expenseDataRow["REF 4"].ToString(),
                        expenseDataRow["SOURCE"].ToString(),
                        expenseDataRow["COMMENT"].ToString(),
                        expenseDataRow["D/c"].ToString(),
                        expenseDataRow["EFFECTIVE DATE"].ToString(),
                        EntryDate,
                        expenseDataRow["POST CLERK"].ToString(),
                        PostDate,
                        expenseDataRow["GROSS AMOUNT"].ToString(),
                        expenseDataRow["RECEIPT"].ToString(),
                        expenseDataRow["BATCH"].ToString().Trim(),
                        BatchTotal,
                        importRequest.CreatedBy,
                        };
                    var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Import.USPIMPORTDRAWREVENUETRANSACTION, SqlObject.Parameters);
                }
            }
            catch (System.Exception ex)
            {
                errorMessages.Message = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }

        public ErrorMessages ImportExpensesTransaction(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            errorMessages.Message = string.Empty;
            var expenseDataSet = importRequest.dataset;
            try
            {
                foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
                {
                    var EffectiveDate = expenseDataRow["EFFECTIVE DATE"].ToString();
                    string[] Effd = EffectiveDate.Split(' ')[0].Split('-');
                    if (Effd.Length > 1)
                        EffectiveDate = Effd[1] + "/" + Effd[0] + "/" + Effd[2];
                    else
                        EffectiveDate = Effd[0];
                    var PostDate = expenseDataRow["POST DATE"].ToString();
                    string[] PostD = PostDate.Split(' ')[0].Split('-');
                    if (PostD.Length > 1)
                        PostDate = PostD[1] + "/" + PostD[0] + "/" + PostD[2];
                    else
                        PostDate = PostD[0];
                    var EntryDate = expenseDataRow["ENTRY DATE"].ToString();
                    string[] EntD = EntryDate.Split(' ')[0].Split('-');
                    if (EntD.Length > 1)
                        EntryDate = EntD[1] + "/" + EntD[0] + "/" + EntD[2];
                    else
                        EntryDate = EntD[0];
                    var BatchTotal = expenseDataRow["GROSS AMOUNT"].ToString();
                    if (BatchTotal == "")
                        BatchTotal = "0";
                    SqlObject.Parameters = new object[] {
                        expenseDataRow["YEAR"].ToString(),
                        expenseDataRow["PERIOD"].ToString(),
                        expenseDataRow["JOURNAL"].ToString(),
                        expenseDataRow["LINE"].ToString(),
                        expenseDataRow["FUND"].ToString(),
                        expenseDataRow["ORG"].ToString(),
                        expenseDataRow["OBJECT"].ToString(),
                        expenseDataRow["PROJECT"].ToString(),
                        expenseDataRow["CLERK"].ToString(),
                        expenseDataRow["REF 1/VENDOR"].ToString(),
                        expenseDataRow["REF 2/PO"].ToString(),
                        expenseDataRow["REF 3/DOCUMENT"].ToString(),
                        expenseDataRow["REF 4"].ToString(),
                        expenseDataRow["SOURCE"].ToString(),
                        expenseDataRow["COMMENT"].ToString(),
                        expenseDataRow["D/c"].ToString(),
                        EffectiveDate,
                        EntryDate,
                        expenseDataRow["POST CLERK"].ToString(),
                        PostDate,
                        BatchTotal,
                        expenseDataRow["CHECK"].ToString(),
                        expenseDataRow["VOUCHER"].ToString(),
                        expenseDataRow["WARRANT"].ToString().Trim(),
                        expenseDataRow["VENDOR"].ToString().Trim(),
                        importRequest.CreatedBy,
                        };
                    var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Import.USPIMPORTEXPENSESTRANSACTION, SqlObject.Parameters);
                }
            }
            catch (System.Exception ex)
            {
                errorMessages.Message = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }
    }
}
