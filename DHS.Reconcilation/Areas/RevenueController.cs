using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
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
        public RevenueResponse GetRevenues(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenues(revenueRequest);
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
        public RevenueResponse GetRevenue(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenue(revenueRequest);
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
        public RevenueResponse SaveRevenue(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveRevenue(revenueRequest);
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
        public RevenueResponse GetRevenueExpense(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueExpense(revenueRequest);
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
        public RevenueResponse GetRevenueDocuments(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueDocuments(revenueRequest);
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
        public RevenueResponse GetRevenueDocument(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueDocument(revenueRequest);
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
        public RevenueResponse SaveRevenueDocument(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveRevenueDocument(revenueRequest);
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
        public RevenueResponse LinkToDraw(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.LinkToDraw(revenueRequest);
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
        public RevenueResponse SaveLinkToDraw(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveLinkToDraw(revenueRequest);
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
        public RevenueResponse DrawRevenue(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.DrawRevenue(revenueRequest);
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
        public RevenueResponse GetRevenueTransactions(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueTransactions(revenueRequest);
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
        public RevenueResponse GetRevenueTransaction(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueTransaction(revenueRequest);
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
        public RevenueResponse SaveRevenueTransaction(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveRevenueTransaction(revenueRequest);
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
        public RevenueResponse GetRevenueInformation(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueInformation(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("GetRevenueTransactionDetails")]
        public RevenueResponse GetRevenueTransactionDetails(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueTransactionDetails(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("DeleteRevenueTransaction")]
        public RevenueResponse DeleteRevenueTransaction(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.DeleteRevenueTransaction(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetManageReceivables")]
        public RevenueResponse GetManageReceivables(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetManageReceivables(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetTransactionDetail")]
        public RevenueResponse GetTransactionDetail(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetTransactionDetail(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("SaveTransactionDetail")]
        public RevenueResponse SaveTransactionDetail(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveTransactionDetail(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("CheckRelatedTrans")]
        public RevenueResponse CheckRelatedTrans(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.CheckRelatedTrans(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("GetMissingRevenueTransactions")]
        public RevenueResponse GetMissingRevenueTransactions(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetMissingRevenueTransactions(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetMissingRevenueTransaction")]
        public RevenueResponse GetMissingRevenueTransaction(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetMissingRevenueTransaction(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("SaveMissingRevenueTransaction")]
        public RevenueResponse SaveMissingRevenueTransaction(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveMissingRevenueTransaction(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("DrawMissingRevenue")]
        public RevenueResponse DrawMissingRevenue(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.DrawMissingRevenue(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("MissingRevenues")]
        public RevenueResponse MissingRevenues(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.MissingRevenues(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetExpenseRevenueTransactions")]
        public RevenueResponse GetExpenseRevenueTransactions(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetExpenseRevenueTransactions(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetRevenueExpenseCompare")]
        public RevenueResponse GetRevenueExpenseCompare(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueExpenseCompare(revenueRequest);
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
