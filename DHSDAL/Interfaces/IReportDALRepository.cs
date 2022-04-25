using DHSEntities;

namespace DHSDAL
{
    public interface IReportDALRepository
    {
        ReportResponse GetGrantProjectReport();
        ReportResponse GetGrantReport(ReportRequest reportRequest);
        ReportResponse GetProjectReceivablesReport(ReportRequest reportRequest);
    }
}
