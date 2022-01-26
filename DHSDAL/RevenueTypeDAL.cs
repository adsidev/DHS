using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class RevenueTypeDAL : IRevenueTypeDALRepository
    { /// <summary>
      /// Connection string
      /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// FiscalPeriod Response <see cref="CostReportInvoiceResponse"/>
        /// </summary>
        RevenueTypeResponse revenueTypeResponse;

        public RevenueTypeDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            revenueTypeResponse = new RevenueTypeResponse();
        }

        public RevenueTypeResponse GetRevenueTypes()
        {
            List<RevenueTypeEntity> revenueTypeEntities = new List<RevenueTypeEntity>();
            SqlObject.Parameters = new object[] {
            };
            var revenueTypeDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.RevenueType.USPGETREVENUETYPES, SqlObject.Parameters);
            foreach (DataRow revenueTypeDataRow in revenueTypeDataSet.Tables[0].Rows)
            {
                RevenueTypeEntity revenueTypeEntity = new RevenueTypeEntity();
                try
                {
                    revenueTypeEntity.RevenueTypeId = Convert.ToInt32(revenueTypeDataRow["RevenueTypeId"].ToString());
                    revenueTypeEntity.RevenueTypeName = revenueTypeDataRow["RevenueTypeName"].ToString();
                    revenueTypeEntity.RevenueTypeDescription = revenueTypeDataRow["RevenueTypeDescription"].ToString();
                }
                catch (Exception exception)
                {
                    revenueTypeEntity.ErrorMessage = exception.Message;
                    revenueTypeEntity.Exception = exception;
                }
                finally
                {
                    revenueTypeEntities.Add(revenueTypeEntity);
                }
            }
            if (revenueTypeEntities.Count >= 1 || revenueTypeEntities.Count == 0)
            {
                revenueTypeResponse.ErrorMessage = string.Empty;
                revenueTypeResponse.Message = string.Empty;
            }
            revenueTypeResponse.RevenueTypeEntities = revenueTypeEntities;
            return revenueTypeResponse;
        }

        public RevenueTypeResponse GetRevenueType(RevenueTypeRequest revenueTypeRequest)
        {
            RevenueTypeEntity revenueTypeEntity = new RevenueTypeEntity();
            revenueTypeEntity.ErrorMessage = string.Empty;
            revenueTypeResponse.ErrorMessage = string.Empty;
            revenueTypeEntity.Message = string.Empty;
            revenueTypeResponse.Message = string.Empty;
            if (revenueTypeRequest.RevenueTypeEntity.RevenueTypeId > 0)
            {
                SqlObject.Parameters = new object[] {
                    revenueTypeRequest.RevenueTypeEntity.RevenueTypeId
                };
                var revenueTypeDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.RevenueType.USPGETREVENUETYPE, SqlObject.Parameters);
                foreach (DataRow revenueTypeDataRow in revenueTypeDataSet.Tables[0].Rows)
                {
                    try
                    {
                        revenueTypeEntity.RevenueTypeId = Convert.ToInt32(revenueTypeDataRow["RevenueTypeId"].ToString());
                        revenueTypeEntity.RevenueTypeName = revenueTypeDataRow["RevenueTypeName"].ToString();
                        revenueTypeEntity.RevenueTypeDescription = revenueTypeDataRow["RevenueTypeDescription"].ToString();
                    }
                    catch (Exception exception)
                    {
                        revenueTypeEntity.ErrorMessage = exception.Message;
                        revenueTypeResponse.ErrorMessage = exception.Message;
                        revenueTypeEntity.Exception = exception;
                        revenueTypeResponse.Exception = exception;
                    }
                    finally
                    {

                    }
                }
            }

            revenueTypeResponse.RevenueTypeEntity = revenueTypeEntity;
            return revenueTypeResponse;
        }

        public RevenueTypeResponse SaveRevenueType(RevenueTypeRequest revenueTypeRequest)
        {
            try
            {
                if (revenueTypeRequest.RevenueTypeEntity.RevenueTypeId > 0)
                    revenueTypeRequest.RevenueTypeEntity.SaveString = "U";
                else
                    revenueTypeRequest.RevenueTypeEntity.SaveString = "I";

                SqlObject.Parameters = new object[] {
                revenueTypeRequest.RevenueTypeEntity.SaveString,
                revenueTypeRequest.RevenueTypeEntity.RevenueTypeId,
                revenueTypeRequest.RevenueTypeEntity.RevenueTypeName,
                revenueTypeRequest.RevenueTypeEntity.RevenueTypeDescription,
                revenueTypeRequest.RevenueTypeEntity.ModifiedBy
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.RevenueType.USPSAVEREVENUETYPE, SqlObject.Parameters);
                revenueTypeResponse.Message = string.Empty;
                revenueTypeResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                revenueTypeResponse.ErrorMessage = ex.Message;
                revenueTypeResponse.Exception = ex;
            }
            return revenueTypeResponse;
        }

    }
}
