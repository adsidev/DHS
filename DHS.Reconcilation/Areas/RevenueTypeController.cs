using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace Medicaid.Reconcilation.Areas
{
    [RoutePrefix("RevenueType")]
    public class RevenueTypeController : ApiController
    {
        IRevenueTypeRepository revenueTypeRepository;
        RevenueTypeResponse revenueTypeResponse;

        public RevenueTypeController()
        {
            revenueTypeRepository = new RevenueTypeBAL();
            revenueTypeResponse = new RevenueTypeResponse();
        }

        [HttpPost]
        [Route("GetRevenueTypes")]
        public RevenueTypeResponse GetRevenueTypes(RevenueTypeRequest revenueTypeRequest)
        {
            try
            {
                revenueTypeResponse = revenueTypeRepository.GetRevenueTypes(revenueTypeRequest);
            }
            catch (Exception ex)
            {
                revenueTypeResponse.ErrorMessage = ex.Message;
                revenueTypeResponse.Exception = ex;
            }
            return revenueTypeResponse;
        }

        [HttpPost]
        [Route("GetRevenueType")]
        public RevenueTypeResponse GetRevenueType(RevenueTypeRequest revenueTypeRequest)
        {
            try
            {
                revenueTypeResponse = revenueTypeRepository.GetRevenueType(revenueTypeRequest);
            }
            catch (Exception ex)
            {
                revenueTypeResponse.ErrorMessage = ex.Message;
                revenueTypeResponse.Exception = ex;
            }
            return revenueTypeResponse;
        }

        [HttpPost]
        [Route("SaveRevenueType")]
        public RevenueTypeResponse SaveRevenueType(RevenueTypeRequest revenueTypeRequest)
        {
            try
            {
                revenueTypeResponse = revenueTypeRepository.SaveRevenueType(revenueTypeRequest);
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
