using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
{
    [RoutePrefix("Status")]
    public class StatusController : ApiController
    {
        IStatusRepository statusRepository;
        StatusResponse statusResponse;

        public StatusController()
        {
            statusRepository = new StatusBAL();
            statusResponse = new StatusResponse();
        }

        [HttpGet]
        [Route("GetStatuses")]
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

        [HttpPost]
        [Route("GetStatus")]
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


        [HttpPost]
        [Route("SaveStatus")]
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

        [HttpPost]
        [Route("CheckStatus")]
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

    }
}
