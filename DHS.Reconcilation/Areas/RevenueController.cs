using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace Medicaid.Reconcilation.Areas
{
    [RoutePrefix("Revenue")]
    public class RevenueController : ApiController
    {
        IRevenueRepository expenseRepository;
        RevenueResponse expenseResponse;

        public RevenueController()
        {
            expenseRepository = new RevenueBAL();
            expenseResponse = new RevenueResponse();
        }

        [HttpPost]
        [Route("GetRevenues")]
        public RevenueResponse GetRevenues(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenues(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("GetRevenue")]
        public RevenueResponse GetRevenue(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("SaveRevenue")]
        public RevenueResponse SaveRevenue(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("GetRevenueExpense")]
        public RevenueResponse GetRevenueExpense(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueExpense(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetRevenueDocuments")]
        public RevenueResponse GetRevenueDocuments(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueDocuments(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("GetRevenueDocument")]
        public RevenueResponse GetRevenueDocument(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueDocument(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("SaveRevenueDocument")]
        public RevenueResponse SaveRevenueDocument(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveRevenueDocument(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("LinkToDraw")]
        public RevenueResponse LinkToDraw(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.LinkToDraw(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("SaveLinkToDraw")]
        public RevenueResponse SaveLinkToDraw(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveLinkToDraw(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("DrawRevenue")]
        public RevenueResponse DrawRevenue(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.DrawRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetRevenueTransactions")]
        public RevenueResponse GetRevenueTransactions(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueTransactions(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetRevenueTransaction")]
        public RevenueResponse GetRevenueTransaction(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueTransaction(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("SaveRevenueTransaction")]
        public RevenueResponse SaveRevenueTransaction(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveRevenueTransaction(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("GetRevenueInformation")]
        public RevenueResponse GetRevenueInformation(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueInformation(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

    }
}
