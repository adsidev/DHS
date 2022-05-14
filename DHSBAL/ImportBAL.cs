using DHSDAL;
using DHSEntities;
using System;

namespace DHSBAL
{
    public class ImportBAL : IImportRepository
    {
        IImportDALRepository importDALRepository;

        public ImportBAL()
        {
            importDALRepository = new ImportDAL();
        }

        public ErrorMessages ImportExpense(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            try
            {
                errorMessages = importDALRepository.ImportExpense(importRequest);
            }
            catch (Exception ex)
            {
                errorMessages.ErrorMessage = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }

        public ErrorMessages CheckImportExpense(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            try
            {
                errorMessages = importDALRepository.CheckImportExpense(importRequest);
            }
            catch (Exception ex)
            {
                errorMessages.ErrorMessage = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }

        public ErrorMessages ImportRevenue(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            try
            {
                errorMessages = importDALRepository.ImportRevenue(importRequest);
            }
            catch (Exception ex)
            {
                errorMessages.ErrorMessage = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }

        public ErrorMessages CheckImportRevenue(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            try
            {
                errorMessages = importDALRepository.CheckImportRevenue(importRequest);
            }
            catch (Exception ex)
            {
                errorMessages.ErrorMessage = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }

        public ErrorMessages ImportExpenseTransaction(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            try
            {
                errorMessages = importDALRepository.ImportExpenseTransaction(importRequest);
            }
            catch (Exception ex)
            {
                errorMessages.ErrorMessage = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }


        public ErrorMessages ImportDrawRevenueTransaction(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            try
            {
                errorMessages = importDALRepository.ImportDrawRevenueTransaction(importRequest);
            }
            catch (Exception ex)
            {
                errorMessages.ErrorMessage = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }

        public ErrorMessages ImportExpensesTransaction(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            try
            {
                errorMessages = importDALRepository.ImportExpensesTransaction(importRequest);
            }
            catch (Exception ex)
            {
                errorMessages.ErrorMessage = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }

        public ErrorMessages ImportPriorYearExpenseTransaction(ImportRequest importRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            try
            {
                errorMessages = importDALRepository.ImportPriorYearExpenseTransaction(importRequest);
            }
            catch (Exception ex)
            {
                errorMessages.ErrorMessage = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }
    }
}
