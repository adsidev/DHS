using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
{
    [RoutePrefix("Expense")]
    public class ExpenseController : ApiController
    {
        IExpenseRepository expenseRepository;
        ExpenseResponse expenseResponse;

        public ExpenseController()
        {
            expenseRepository = new ExpenseBAL();
            expenseResponse = new ExpenseResponse();
        }

        [HttpPost]
        [Route("GetExpenses")]
        public ExpenseResponse GetExpenses(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetExpenses(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("GetExpenseRevenue")]
        public ExpenseResponse GetExpenseRevenue(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetExpenseRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetTransactionDetails")]
        public ExpenseResponse GetTransactionDetails(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetTransactionDetails(expenseRequest);
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
        public ExpenseResponse GetTransactionDetail(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetTransactionDetail(expenseRequest);
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
        public ExpenseResponse SaveTransactionDetail(ExpenseRequest expenseRequest)
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
        [Route("GetExpense")]
        public ExpenseResponse GetExpense(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetExpense(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("SaveExpense")]
        public ExpenseResponse SaveExpense(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveExpense(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetLinkToRevenue")]
        public ExpenseResponse GetLinkToRevenue(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetLinkToRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("SaveLinkToRevenue")]
        public ExpenseResponse SaveLinkToRevenue(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveLinkToRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("DLinkRevenue")]
        public ExpenseResponse DLinkRevenue(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.DLinkRevenue(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetExpenseDocuments")]
        public ExpenseResponse GetExpenseDocuments(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetExpenseDocuments(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetExpenseDocument")]
        public ExpenseResponse GetExpenseDocument(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetExpenseDocument(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("SaveExpenseDocument")]
        public ExpenseResponse SaveExpenseDocument(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveExpenseDocument(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }



        [HttpPost]
        [Route("GetRevenueTransactionByExpenseId")]
        public ExpenseResponse GetRevenueTransactionByExpenseId(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetRevenueTransactionByExpenseId(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetAllTransactionDetails")]
        public ExpenseResponse GetAllTransactionDetails(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetAllTransactionDetails(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }



        [HttpPost]
        [Route("RemoveLinkExpRev")]
        public ExpenseResponse RemoveLinkExpRev(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.RemoveLinkExpRev(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetExpenseTransactions")]
        public ExpenseResponse GetExpenseTransactions(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetExpenseTransactions(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("SaveLinkToExpenseTransaction")]
        public ExpenseResponse SaveLinkToExpenseTransaction(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveLinkToExpenseTransaction(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("GetExpExpTransCompare")]
        public ExpenseResponse GetExpExpTransCompare(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetExpExpTransCompare(expenseRequest);
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
        public ExpenseResponse CheckRelatedTrans(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.CheckRelatedTrans(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("CheckBatchNumber")]
        public ExpenseResponse CheckBatchNumber(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.CheckBatchNumber(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("GetMissingExpenseTransactions")]
        public ExpenseResponse GetMissingExpenseTransactions(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetMissingExpenseTransactions(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        [HttpPost]
        [Route("GetMissingExpenseTransaction")]
        public ExpenseResponse GetMissingExpenseTransaction(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.GetMissingExpenseTransaction(expenseRequest);
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }


        [HttpPost]
        [Route("SaveMissingExpenseTransaction")]
        public ExpenseResponse SaveMissingExpenseTransaction(ExpenseRequest expenseRequest)
        {
            try
            {
                expenseResponse = expenseRepository.SaveMissingExpenseTransaction(expenseRequest);
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
