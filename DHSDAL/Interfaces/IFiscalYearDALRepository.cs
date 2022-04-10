using DHSEntities;

namespace DHSDAL
{
    public interface IFiscalYearDALRepository
    {
        FiscalYearResponse GetFiscalYears();
        FiscalYearResponse GetFiscalYear(FiscalYearRequest fiscalYearRequest);
        FiscalYearResponse SaveFiscalYear(FiscalYearRequest fiscalYearRequest);
        FiscalYearResponse CheckFiscalYear(FiscalYearRequest fiscalYearRequest);
    }
}
