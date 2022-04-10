using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class FiscalYearDAL : IFiscalYearDALRepository
    {
        private readonly string _connectionString;

        FiscalYearResponse fiscalYearResponse;

        public FiscalYearDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            fiscalYearResponse = new FiscalYearResponse();
            fiscalYearResponse.ErrorMessage = String.Empty;
            fiscalYearResponse.Message = String.Empty;
        }

        public FiscalYearResponse GetFiscalYears()
        {
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.FiscalYear.USPGETFISCALYEARS);
            List<FiscalYearEntity> fiscalYearEntities = new List<FiscalYearEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                FiscalYearEntity fiscalYearEntity = new FiscalYearEntity();
                try
                {
                    fiscalYearEntity.FiscalYearId = Convert.ToInt32(expenseDataRow["FiscalYearId"]);
                    fiscalYearEntity.FiscalYear = expenseDataRow["FiscalYear"].ToString();
                }
                catch (Exception exception)
                {
                    fiscalYearResponse.ErrorMessage = exception.Message;
                    fiscalYearResponse.Exception = exception;
                }
                finally
                {
                    fiscalYearEntities.Add(fiscalYearEntity);
                }
            }
            fiscalYearResponse.fiscalYearEntities = fiscalYearEntities;
            return fiscalYearResponse;
        }

        public FiscalYearResponse GetFiscalYear(FiscalYearRequest fiscalYearRequest)
        {
            SqlObject.Parameters = new object[] {
                   fiscalYearRequest.fiscalYearEntity.FiscalYearId
                };
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.FiscalYear.USPGETFISCALYEAR,SqlObject.Parameters);
            FiscalYearEntity fiscalYearEntity = new FiscalYearEntity();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {                
                try
                {
                    fiscalYearEntity.FiscalYearId = Convert.ToInt32(expenseDataRow["FiscalYearId"]);
                    fiscalYearEntity.FiscalYear = expenseDataRow["FiscalYear"].ToString();
                }
                catch (Exception exception)
                {
                    fiscalYearResponse.ErrorMessage = exception.Message;
                    fiscalYearResponse.Exception = exception;
                }
                finally
                {
                    
                }
            }
            fiscalYearResponse.fiscalYearEntity = fiscalYearEntity;
            return fiscalYearResponse;
        }

        public FiscalYearResponse SaveFiscalYear(FiscalYearRequest fiscalYearRequest)
        {
            try
            {
                SqlObject.Parameters = new object[] {
                fiscalYearRequest.fiscalYearEntity.FiscalYearId,
                fiscalYearRequest.fiscalYearEntity.FiscalYear,
                fiscalYearRequest.fiscalYearEntity.ModifiedBy,
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.FiscalYear.USPSAVEFISCALYEAR, SqlObject.Parameters);
                fiscalYearResponse.Message = string.Empty;
                fiscalYearResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                fiscalYearResponse.ErrorMessage = ex.Message;
                fiscalYearResponse.Exception = ex;
            }
            return fiscalYearResponse;
        }

        public FiscalYearResponse CheckFiscalYear(FiscalYearRequest fiscalYearRequest)
        {
            try
            {
                SqlObject.Parameters = new object[] {
                fiscalYearRequest.fiscalYearEntity.FiscalYearId,
                fiscalYearRequest.fiscalYearEntity.FiscalYear
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.FiscalYear.USPCHECKFISCALYEAR, SqlObject.Parameters);
                fiscalYearResponse.Message = string.Empty;
                fiscalYearResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                fiscalYearResponse.ErrorMessage = ex.Message;
                fiscalYearResponse.Exception = ex;
            }
            return fiscalYearResponse;
        }
    }
}
