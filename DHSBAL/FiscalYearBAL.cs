using DHSDAL;
using DHSEntities;
using System;

namespace DHSBAL
{
    public class FiscalYearBAL : IFiscalYearRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IFiscalYearDALRepository fiscalYearRepository;

        FiscalYearResponse fiscalYearResponse;
        public FiscalYearBAL()
        {
            fiscalYearRepository = new FiscalYearDAL();
            fiscalYearResponse = new FiscalYearResponse();
        }

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

    }
}
