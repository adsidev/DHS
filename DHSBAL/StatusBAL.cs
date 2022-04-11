using DHSDAL;
using DHSEntities;
using System;

namespace DHSBAL
{
    public class StatusBAL : IStatusRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IStatusDALRepository statusRepository;

        StatusResponse statusResponse;
        public StatusBAL()
        {
            statusRepository = new StatusDAL();
            statusResponse = new StatusResponse();
        }

        public StatusResponse SaveStatus(StatusRequest statusRequest)
        {
            try
            {
                statusResponse = statusRepository.SaveStatus(statusRequest);
            }
            catch (Exception ex)
            {
                statusResponse.ErrorMessage = ex.Message;
                statusResponse.Exception = ex;
            }
            return statusResponse;
        }

        public StatusResponse GetStatus(StatusRequest statusRequest)
        {
            try
            {
                statusResponse = statusRepository.GetStatus(statusRequest);
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
                statusResponse = statusRepository.CheckStatus(statusRequest);
            }
            catch (Exception ex)
            {
                statusResponse.ErrorMessage = ex.Message;
                statusResponse.Exception = ex;
            }
            return statusResponse;
        }

        public StatusResponse GetStatuses()
        {
            try
            {
                statusResponse = statusRepository.GetStatuses();
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
