using DHSEntities;

namespace DHSBAL
{
    public interface IReportRepository
    {
        ReportResponse GetGrantProjectReport();
        ReportResponse GetGrantReport(ReportRequest reportRequest);
    }
}
