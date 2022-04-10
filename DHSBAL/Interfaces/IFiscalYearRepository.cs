using DHSEntities;

namespace DHSBAL
{
    public interface IFiscalYearRepository
    {
        FiscalYearResponse GetFiscalYears();
        FiscalYearResponse GetFiscalYear(FiscalYearRequest fiscalYearRequest);
        FiscalYearResponse SaveFiscalYear(FiscalYearRequest fiscalYearRequest);
        FiscalYearResponse CheckFiscalYear(FiscalYearRequest fiscalYearRequest);
    }
}
