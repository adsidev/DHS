using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
{
    [RoutePrefix("FiscalYear")]
    public class FiscalYearController : ApiController
    {
        IFiscalYearRepository fiscalYearRepository;
        FiscalYearResponse fiscalYearResponse;

        public FiscalYearController()
        {
            fiscalYearRepository = new FiscalYearBAL();
            fiscalYearResponse = new FiscalYearResponse();
        }

        [HttpGet]
        [Route("GetFiscalYears")]
        public FiscalYearResponse GetFiscalYears()
        {
            try
            {
                fiscalYearResponse = fiscalYearRepository.GetFiscalYears();
            }
            catch (Exception ex)
            {
                fiscalYearResponse.ErrorMessage = ex.Message;
                fiscalYearResponse.Exception = ex;
            }
            return fiscalYearResponse;
        }

        [HttpPost]
        [Route("GetFiscalYear")]
        public FiscalYearResponse GetFiscalYear(FiscalYearRequest fiscalYearRequest)
        {
            try
            {
                fiscalYearResponse = fiscalYearRepository.GetFiscalYear(fiscalYearRequest);
            }
            catch (Exception ex)
            {
                fiscalYearResponse.ErrorMessage = ex.Message;
                fiscalYearResponse.Exception = ex;
            }
            return fiscalYearResponse;
        }


        [HttpPost]
        [Route("SaveFiscalYear")]
        public FiscalYearResponse SaveFiscalYear(FiscalYearRequest fiscalYearRequest)
        {
            try
            {
                fiscalYearResponse = fiscalYearRepository.SaveFiscalYear(fiscalYearRequest);
            }
            catch (Exception ex)
            {
                fiscalYearResponse.ErrorMessage = ex.Message;
                fiscalYearResponse.Exception = ex;
            }
            return fiscalYearResponse;
        }

        [HttpPost]
        [Route("CheckFiscalYear")]
        public FiscalYearResponse CheckFiscalYear(FiscalYearRequest fiscalYearRequest)
        {
            try
            {
                fiscalYearResponse = fiscalYearRepository.CheckFiscalYear(fiscalYearRequest);
            }
            catch (Exception ex)
            {
                fiscalYearResponse.ErrorMessage = ex.Message;
                fiscalYearResponse.Exception = ex;
            }
            return fiscalYearResponse;
        }

    }
}
