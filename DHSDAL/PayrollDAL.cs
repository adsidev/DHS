using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class PayrollDAL : IPayrollDALRepository
    {

        private readonly string _connectionString;
        PayrollResponse payrollResponse;
        public PayrollDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            payrollResponse = new PayrollResponse();
            payrollResponse.ErrorMessage = string.Empty;
            payrollResponse.Message = string.Empty;
        }

        public PayrollResponse GetPayrolls(PayrollRequest payrollRequest)
        {
            SqlObject.Parameters = new object[] {
                payrollRequest.payrollEntity.FiscalYearId
            };
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Payroll.USPGETPAYROLLS, SqlObject.Parameters);
            List<PayrollEntity> payrollEntities = new List<PayrollEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                PayrollEntity payrollEntity = new PayrollEntity();
                try
                {
                    payrollEntity.PayrollId = Convert.ToInt32(expenseDataRow["PayrollId"]);
                    payrollEntity.FiscalYear = expenseDataRow["FiscalYear"].ToString();
                    payrollEntity.PayrollNumber = expenseDataRow["PayrollNumber"].ToString();
                    payrollEntity.PayrollDate = Convert.ToDateTime(expenseDataRow["PayrollDate"].ToString()).ToShortDateString();                    
                }
                catch (Exception exception)
                {
                    payrollEntity.PayrollDate = expenseDataRow["PayrollDate"].ToString();
                    payrollResponse.ErrorMessage = exception.Message;
                    payrollResponse.Message = exception.Message;
                    payrollResponse.Exception = exception;
                }
                finally
                {
                    payrollEntities.Add(payrollEntity);
                }
            }
            payrollResponse.payrollEntities = payrollEntities;
            CommonDAL commonDAL = new CommonDAL();
            payrollResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            return payrollResponse;
        }

        public PayrollResponse GetPayroll(PayrollRequest payrollRequest)
        {
            SqlObject.Parameters = new object[] {
                payrollRequest.payrollEntity.PayrollId
            };
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Payroll.USPGETPAYROLL, SqlObject.Parameters);
            PayrollEntity payrollEntity = new PayrollEntity();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                try
                {
                    payrollEntity.PayrollId = Convert.ToInt64(expenseDataRow["PayrollId"]);
                    payrollEntity.FiscalYearId = Convert.ToInt32(expenseDataRow["FiscalYearId"]);
                    payrollEntity.FiscalYear = expenseDataRow["FiscalYear"].ToString();
                    payrollEntity.PayrollNumber = expenseDataRow["PayrollNumber"].ToString();
                    payrollEntity.PayrollDate = Convert.ToDateTime(expenseDataRow["PayrollDate"].ToString()).ToShortDateString();
                }
                catch (Exception exception)
                {
                    payrollEntity.PayrollDate = expenseDataRow["PayrollDate"].ToString();
                    payrollResponse.ErrorMessage = exception.Message;
                    payrollResponse.Message = exception.Message;
                    payrollResponse.Exception = exception;
                }
            }
            payrollResponse.payrollEntity = payrollEntity;
            CommonDAL commonDAL = new CommonDAL();
            payrollResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            return payrollResponse;
        }

        public PayrollResponse SavePayroll(PayrollRequest payrollRequest)
        {
            try
            {
                if (payrollRequest.payrollEntity.PayrollId > 0)
                    payrollRequest.payrollEntity.SaveString = "U";
                else
                    payrollRequest.payrollEntity.SaveString = "I";

                SqlObject.Parameters = new object[] {
                payrollRequest.payrollEntity.SaveString,
                payrollRequest.payrollEntity.PayrollId,
                payrollRequest.payrollEntity.FiscalYearId,
                payrollRequest.payrollEntity.PayrollDate,
                payrollRequest.payrollEntity.ModifiedBy,
            };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Payroll.USPSAVEPAYROLL, SqlObject.Parameters);
            }
            catch (Exception ex)
            {
                payrollResponse.ErrorMessage = ex.Message;
                payrollResponse.Message = ex.Message;
                payrollResponse.Exception = ex;
            }
           
            return payrollResponse;
        }

        public PayrollResponse GetPayrollProjects(PayrollRequest payrollRequest)
        {
            SqlObject.Parameters = new object[] {
                payrollRequest.payrollProjectEntity.PayrollId
            };
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Payroll.USPGETPAYROLLPROJECTS, SqlObject.Parameters);
            List<PayrollProjectEntity> payrollProjectEntities = new List<PayrollProjectEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                PayrollProjectEntity payrollProjectEntity = new PayrollProjectEntity();
                try
                {
                    payrollProjectEntity.PayrollId = Convert.ToInt64(expenseDataRow["PayrollId"]);
                    payrollProjectEntity.PayrollProjectId = Convert.ToInt64(expenseDataRow["PayrollProjectId"]);
                    payrollProjectEntity.ProjectId = Convert.ToInt32(expenseDataRow["ProjectId"]);
                    payrollProjectEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    payrollProjectEntity.FiscalYear = expenseDataRow["FiscalYear"].ToString();
                    payrollProjectEntity.PayrollNumber = expenseDataRow["PayrollNumber"].ToString();
                    payrollProjectEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    payrollProjectEntity.Notes = expenseDataRow["Notes"].ToString();
                    payrollProjectEntity.PayrollTotal = Convert.ToDecimal(expenseDataRow["PayrollTotal"].ToString());
                    payrollProjectEntity.DrawnAmount = Convert.ToDecimal(expenseDataRow["DrawnAmount"].ToString());
                    payrollProjectEntity.AdjustedAmount = Convert.ToDecimal(expenseDataRow["AdjustedAmount"].ToString());
                    try
                    {
                        payrollProjectEntity.PayrollDate = Convert.ToDateTime(expenseDataRow["PayrollDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        payrollProjectEntity.PayrollDate = expenseDataRow["PayrollDate"].ToString();
                    }
                    payrollProjectEntity.EffectiveDate= Convert.ToDateTime(expenseDataRow["EffectiveDate"].ToString()).ToShortDateString();
                }
                catch (Exception exception)
                {
                    payrollProjectEntity.EffectiveDate = expenseDataRow["EffectiveDate"].ToString();
                    payrollResponse.ErrorMessage = exception.Message;
                    payrollResponse.Message = exception.Message;
                    payrollResponse.Exception = exception;
                }
                finally
                {
                    payrollProjectEntities.Add(payrollProjectEntity);
                }
            }
            payrollResponse.payrollProjectEntities = payrollProjectEntities;
            CommonDAL commonDAL = new CommonDAL();
            payrollResponse.projectEntities = commonDAL.GetProjects();
            return payrollResponse;
        }

        public PayrollResponse GetPayrollProject(PayrollRequest payrollRequest)
        {
            PayrollProjectEntity payrollProjectEntity = new PayrollProjectEntity();
            if (payrollRequest.payrollProjectEntity.PayrollProjectId>0)
            {
                SqlObject.Parameters = new object[] {
                    payrollRequest.payrollProjectEntity.PayrollProjectId
                };  
                var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Payroll.USPGETPAYROLLPROJECT, SqlObject.Parameters);
                
                foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
                {
                    try
                    {
                        payrollProjectEntity.PayrollId = Convert.ToInt64(expenseDataRow["PayrollId"]);
                        payrollProjectEntity.PayrollProjectId = Convert.ToInt64(expenseDataRow["PayrollProjectId"]);
                        payrollProjectEntity.ProjectId = Convert.ToInt32(expenseDataRow["ProjectId"]);
                        payrollProjectEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                        payrollProjectEntity.FiscalYear = expenseDataRow["FiscalYear"].ToString();
                        payrollProjectEntity.PayrollNumber = expenseDataRow["PayrollNumber"].ToString();
                        payrollProjectEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                        payrollProjectEntity.Notes = expenseDataRow["Notes"].ToString();
                        payrollProjectEntity.PayrollTotal = Convert.ToDecimal(expenseDataRow["PayrollTotal"].ToString());
                        payrollProjectEntity.DrawnAmount = Convert.ToDecimal(expenseDataRow["DrawnAmount"].ToString());
                        payrollProjectEntity.AdjustedAmount = Convert.ToDecimal(expenseDataRow["AdjustedAmount"].ToString());
                        try
                        {
                            payrollProjectEntity.PayrollDate = Convert.ToDateTime(expenseDataRow["PayrollDate"].ToString()).ToShortDateString();
                        }
                        catch (Exception)
                        {
                            payrollProjectEntity.PayrollDate = expenseDataRow["PayrollDate"].ToString();
                        }
                        payrollProjectEntity.EffectiveDate = Convert.ToDateTime(expenseDataRow["EffectiveDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception exception)
                    {
                        payrollProjectEntity.EffectiveDate = expenseDataRow["EffectiveDate"].ToString();
                        payrollResponse.ErrorMessage = exception.Message;
                        payrollResponse.Message = exception.Message;
                        payrollResponse.Exception = exception;
                    }
                }
            }
            else
            {
                payrollProjectEntity.PayrollId = payrollRequest.payrollProjectEntity.PayrollId;
            }
            payrollResponse.payrollProjectEntity= payrollProjectEntity;
            CommonDAL commonDAL = new CommonDAL();
            payrollResponse.projectEntities = commonDAL.GetProjects();
            return payrollResponse;
        }

        public PayrollResponse SavePayrollProject(PayrollRequest payrollRequest)
        {
            try
            {
                if (payrollRequest.payrollProjectEntity.PayrollProjectId > 0)
                    payrollRequest.payrollProjectEntity.SaveString = "U";
                else
                    payrollRequest.payrollProjectEntity.SaveString = "I";

                SqlObject.Parameters = new object[] {
                payrollRequest.payrollProjectEntity.SaveString,
                payrollRequest.payrollProjectEntity.PayrollProjectId,
                payrollRequest.payrollProjectEntity.PayrollId,
                payrollRequest.payrollProjectEntity.ProjectId,
                payrollRequest.payrollProjectEntity.EffectiveDate,
                payrollRequest.payrollProjectEntity.PayrollTotal,
                payrollRequest.payrollProjectEntity.BatchNumber,
                payrollRequest.payrollProjectEntity.DrawnAmount,
                payrollRequest.payrollProjectEntity.AdjustedAmount,
                payrollRequest.payrollProjectEntity.Notes,
                payrollRequest.payrollProjectEntity.ModifiedBy,
            };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Payroll.USPSAVEPAYROLLPROJECT, SqlObject.Parameters);
            }
            catch (Exception ex)
            {
                payrollResponse.ErrorMessage = ex.Message;
                payrollResponse.Message = ex.Message;
                payrollResponse.Exception = ex;
            }

            return payrollResponse;
        }

        public PayrollResponse GetPayrollDocuments(PayrollRequest payrollRequest)
        {
            payrollResponse.ErrorMessage = string.Empty;
            payrollResponse.Message = string.Empty;
            PayrollProjectEntity payrollProjectEntity = new PayrollProjectEntity();
            payrollProjectEntity = GetPayrollProject(payrollRequest).payrollProjectEntity;
            DocumentDAL documentDAL = new DocumentDAL();
            DocumentEntity documentEntity = new DocumentEntity();
            documentEntity.DocumentReferenceId = payrollRequest.payrollProjectEntity.PayrollProjectId;
            documentEntity.DocumentTypeId = 4;
            payrollResponse.documentEntities = documentDAL.GetDocuments(documentEntity);
            payrollResponse.payrollProjectEntity = payrollProjectEntity;
            return payrollResponse;
        }

        public PayrollResponse GetPayrollDocument(PayrollRequest payrollRequest)
        {
            payrollResponse.ErrorMessage = string.Empty;
            payrollResponse.Message = string.Empty;
            DocumentDAL documentDAL = new DocumentDAL();
            DocumentEntity documentEntity = new DocumentEntity();
            documentEntity.DocumentReferenceId = payrollRequest.documentEntity.DocumentReferenceId;
            documentEntity.DocumentId = payrollRequest.documentEntity.DocumentId;
            documentEntity.DocumentTypeId = 4;
            payrollResponse.documentEntity = documentDAL.GetDocument(documentEntity);
            CommonDAL commonDAL = new CommonDAL();
            payrollResponse.documentCategoryEntities = commonDAL.GetDocumentCategories();
            return payrollResponse;
        }

        public PayrollResponse SavePayrollDocument(PayrollRequest payrollRequest)
        {
            payrollResponse.ErrorMessage = string.Empty;
            payrollResponse.Message = string.Empty;
            DocumentDAL documentDAL = new DocumentDAL();
            documentDAL.SaveDocument(payrollRequest.documentEntity);
            return payrollResponse;
        }
    }
}
