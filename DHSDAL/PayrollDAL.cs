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
    }
}
