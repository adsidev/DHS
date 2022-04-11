using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class StatusDAL : IStatusDALRepository
    {
        private readonly string _connectionString;

        StatusResponse statusResponse;

        public StatusDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            statusResponse = new StatusResponse();
            statusResponse.ErrorMessage = String.Empty;
            statusResponse.Message = String.Empty;
        }

        public StatusResponse GetStatuses()
        {
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Status.USPGETSTATUSES);
            List<StatusEntity> statusEntities = new List<StatusEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                StatusEntity statusEntity = new StatusEntity();
                try
                {
                    statusEntity.StatusId = Convert.ToInt32(expenseDataRow["StatusId"]);
                    statusEntity.StatusName = expenseDataRow["Status"].ToString();
                }
                catch (Exception exception)
                {
                    statusResponse.ErrorMessage = exception.Message;
                    statusResponse.Exception = exception;
                }
                finally
                {
                    statusEntities.Add(statusEntity);
                }
            }
            statusResponse.statusEntities = statusEntities;
            return statusResponse;
        }

        public StatusResponse GetStatus(StatusRequest statusRequest)
        {
            SqlObject.Parameters = new object[] {
                   statusRequest.statusEntity.StatusId
                };
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Status.USPGETSTATUS,SqlObject.Parameters);
            StatusEntity statusEntity = new StatusEntity();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {                
                try
                {
                    statusEntity.StatusId = Convert.ToInt32(expenseDataRow["StatusId"]);
                    statusEntity.StatusName = expenseDataRow["Status"].ToString();
                }
                catch (Exception exception)
                {
                    statusResponse.ErrorMessage = exception.Message;
                    statusResponse.Exception = exception;
                }
                finally
                {
                    
                }
            }
            statusResponse.statusEntity = statusEntity;
            return statusResponse;
        }

        public StatusResponse SaveStatus(StatusRequest statusRequest)
        {
            try
            {
                SqlObject.Parameters = new object[] {
                statusRequest.statusEntity.StatusId,
                statusRequest.statusEntity.StatusName,
                statusRequest.statusEntity.ModifiedBy,
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Status.USPSAVESTATUS, SqlObject.Parameters);
                statusResponse.Message = string.Empty;
                statusResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                statusResponse.ErrorMessage = ex.Message;
                statusResponse.Exception = ex;
            }
            return statusResponse;
        }

        public StatusResponse CheckStatus(StatusRequest statusRequest)
        {
            try
            {
                SqlObject.Parameters = new object[] {
                statusRequest.statusEntity.StatusId,
                statusRequest.statusEntity.StatusName
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Status.USPCHECKSTATUS, SqlObject.Parameters);
                statusResponse.Message = string.Empty;
                statusResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                statusResponse.ErrorMessage = ex.Message;
                statusResponse.Exception = ex;
            }
            return statusResponse;
        }
    }
}
