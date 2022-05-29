using DHSDAL;
using DHSEntities;
using System;

namespace DHSBAL
{
    public class RevenueBAL : IRevenueRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IRevenueDALRepository expenseDALRepository;

        RevenueResponse expenseResponse;
        public RevenueBAL()
        {
            expenseDALRepository = new RevenueDAL();
            expenseResponse = new RevenueResponse();
        }

        public RevenueResponse GetRevenues(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetRevenues(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse GetRevenue(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse SaveRevenue(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.SaveRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse GetRevenueExpense(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetRevenueExpense(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse GetRevenueDocuments(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetRevenueDocuments(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse GetRevenueDocument(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetRevenueDocument(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse SaveRevenueDocument(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.SaveRevenueDocument(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse LinkToDraw(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.LinkToDraw(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse SaveLinkToDraw(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.SaveLinkToDraw(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }
        public RevenueResponse DrawRevenue(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.DrawRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse GetRevenueTransactions(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetRevenueTransactions(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse GetRevenueTransaction(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetRevenueTransaction(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse SaveRevenueTransaction(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.SaveRevenueTransaction(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse GetRevenueInformation(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetRevenueInformation(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse GetRevenueTransactionDetails(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetRevenueTransactionDetails(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        public RevenueResponse DeleteRevenueTransaction(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.DeleteRevenueTransaction(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse GetManageReceivables(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetManageReceivables(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse GetTransactionDetail(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetTransactionDetail(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse SaveTransactionDetail(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.SaveTransactionDetail(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse CheckRelatedTrans(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.CheckRelatedTrans(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse GetMissingRevenueTransactions(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetMissingRevenueTransactions(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse GetMissingRevenueTransaction(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetMissingRevenueTransaction(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse SaveMissingRevenueTransaction(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.SaveMissingRevenueTransaction(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse DrawMissingRevenue(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.DrawMissingRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse MissingRevenues(RevenueRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.MissingRevenues(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse GetExpenseRevenueTransactions(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetExpenseRevenueTransactions(revenueRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public RevenueResponse GetRevenueExpenseCompare(RevenueRequest revenueRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetRevenueExpenseCompare(revenueRequest);
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
