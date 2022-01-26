using DataAccessLayer;
using DHSEntities;
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
    }
}
