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
    }
}
