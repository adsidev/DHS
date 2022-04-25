using DHSEntities;

namespace DHSBAL
{
    public interface IReportRepository
    {
        ReportResponse GetGrantProjectReport();
        ReportResponse GetGrantReport(ReportRequest reportRequest);
        ReportResponse GetProjectReceivablesReport(ReportRequest reportRequest);
    }
}
