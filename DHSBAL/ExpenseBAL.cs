using DHSDAL;
using DHSEntities;
using System;

namespace DHSBAL
{
    public class ExpenseBAL : IExpenseRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IExpenseDALRepository expenseDALRepository;

        ExpenseResponse expenseResponse;
        public ExpenseBAL()
        {
            expenseDALRepository = new ExpenseDAL();
            expenseResponse = new ExpenseResponse();
        }

        public ExpenseResponse GetExpenses(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetExpenses(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetTransactionDetails(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetTransactionDetails(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetTransactionDetail(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetTransactionDetail(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse SaveTransactionDetail(ExpenseRequest expenseRequest)
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

        public ExpenseResponse GetExpense(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetExpense(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse SaveExpense(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.SaveExpense(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetExpenseRevenue(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetExpenseRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetLinkToRevenue(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetLinkToRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse SaveLinkToRevenue(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.SaveLinkToRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse DLinkRevenue(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.DLinkRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetExpenseDocuments(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetExpenseDocuments(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetExpenseDocument(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetExpenseDocument(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse SaveExpenseDocument(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.SaveExpenseDocument(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetRevenueTransactionByExpenseId(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetRevenueTransactionByExpenseId(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        public ExpenseResponse GetAllTransactionDetails(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetAllTransactionDetails(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse RemoveLinkExpRev(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.RemoveLinkExpRev(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetExpenseTransactions(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetExpenseTransactions(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse SaveLinkToExpenseTransaction(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.SaveLinkToExpenseTransaction(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetExpExpTransCompare(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetExpExpTransCompare(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse CheckRelatedTrans(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.CheckRelatedTrans(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse CheckBatchNumber(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.CheckBatchNumber(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetMissingExpenseTransactions(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetMissingExpenseTransactions(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetMissingExpenseTransaction(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetMissingExpenseTransaction(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse SaveMissingExpenseTransaction(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.SaveMissingExpenseTransaction(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetMissingRevenueTransaction(ExpenseRequest expenseRequest)
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

        public ExpenseResponse GetMissingExpenses(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetMissingExpenses(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetPriorYearExpenseTransactions(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetPriorYearExpenseTransactions(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetPriorYearTransactionDetail(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.GetPriorYearTransactionDetail(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse SavePriorYearExpenseTransaction(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.SavePriorYearExpenseTransaction(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse RemovePriorYearRevenueTranscation(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseDALRepository.RemovePriorYearRevenueTranscation(expenseRequest);
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
