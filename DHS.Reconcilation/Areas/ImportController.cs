using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
{
    [RoutePrefix("Import")]
    public class ImportController : ApiController
    {
        IImportRepository importRepository;

        public ImportController()
        {
            importRepository = new ImportBAL();
        }

        [HttpPost]
        [Route("ImportExpense")]
        public ErrorMessages ImportExpense(ImportRequest ImportRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            try
            {
                errorMessages = importRepository.ImportExpense(ImportRequest);
            }
            catch (Exception ex)
            {
                errorMessages.ErrorMessage = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }

        [HttpPost]
        [Route("CheckImportExpense")]
        public ErrorMessages CheckImportExpense(ImportRequest ImportRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            try
            {
                errorMessages = importRepository.CheckImportExpense(ImportRequest);
            }
            catch (Exception ex)
            {
                errorMessages.ErrorMessage = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }


        [HttpPost]
        [Route("ImportRevenue")]
        public ErrorMessages ImportRevenue(ImportRequest ImportRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            try
            {
                errorMessages = importRepository.ImportRevenue(ImportRequest);
            }
            catch (Exception ex)
            {
                errorMessages.ErrorMessage = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }

        [HttpPost]
        [Route("CheckImportRevenue")]
        public ErrorMessages CheckImportRevenue(ImportRequest ImportRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            try
            {
                errorMessages = importRepository.CheckImportRevenue(ImportRequest);
            }
            catch (Exception ex)
            {
                errorMessages.ErrorMessage = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }

        [HttpPost]
        [Route("ImportExpenseTransaction")]
        public ErrorMessages ImportExpenseTransaction(ImportRequest ImportRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            try
            {
                errorMessages = importRepository.ImportExpenseTransaction(ImportRequest);
            }
            catch (Exception ex)
            {
                errorMessages.ErrorMessage = ex.Message;
                errorMessages.Exception = ex;
            }
            return errorMessages;
        }

        [HttpPost]
        [Route("ImportDrawRevenueTransaction")]
        public ErrorMessages ImportDrawRevenueTransaction(ImportRequest ImportRequest)
        {
            ErrorMessages errorMessages = new ErrorMessages();
            try
            {
                errorMessages = importRepository.ImportDrawRevenueTransaction(ImportRequest);
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
