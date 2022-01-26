using DHSDAL;
using DHSEntities;
using System;

namespace DHSBAL
{
    public class RevenueTypeBAL : IRevenueTypeRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IRevenueTypeDALRepository revenueTypeDALRepository;

        RevenueTypeResponse revenueTypeResponse;
        public RevenueTypeBAL()
        {
            revenueTypeDALRepository = new RevenueTypeDAL();
            revenueTypeResponse = new RevenueTypeResponse();
        }

        public RevenueTypeResponse GetRevenueTypes(RevenueTypeRequest revenueTypeRequest)
        {
            try
            {
                revenueTypeResponse = revenueTypeDALRepository.GetRevenueTypes();
            }
            catch (Exception ex)
            {
                revenueTypeResponse.ErrorMessage = ex.Message;
                revenueTypeResponse.Exception = ex;
            }
            return revenueTypeResponse;
        }

        public RevenueTypeResponse GetRevenueType(RevenueTypeRequest revenueTypeRequest)
        {
            try
            {
                revenueTypeResponse = revenueTypeDALRepository.GetRevenueType(revenueTypeRequest);
            }
            catch (Exception ex)
            {
                revenueTypeResponse.ErrorMessage = ex.Message;
                revenueTypeResponse.Exception = ex;
            }
            return revenueTypeResponse;
        }

        public RevenueTypeResponse SaveRevenueType(RevenueTypeRequest revenueTypeRequest)
        {
            try
            {
                revenueTypeResponse = revenueTypeDALRepository.SaveRevenueType(revenueTypeRequest);
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
