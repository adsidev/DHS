using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
{
    [RoutePrefix("Report")]
    public class ReportController : ApiController
    {
        IReportRepository reportRepository;
        ReportResponse reportResponse;

        public ReportController()
        {
            reportRepository = new ReportBAL();
            reportResponse = new ReportResponse();
        }

        [HttpPost]
        [Route("GetGrantProjectReport")]
        public ReportResponse GetGrantProjectReport(ReportRequest reportRequest)
        {
            try
            {
                reportResponse = reportRepository.GetGrantProjectReport();
            }
            catch (Exception ex)
            {
                reportResponse.ErrorMessage = ex.Message;
                reportResponse.Exception = ex;
            }
            return reportResponse;
        }

        [HttpPost]
        [Route("GetGrantReport")]
        public ReportResponse GetGrantReport(ReportRequest reportRequest)
        {
            try
            {
                reportResponse = reportRepository.GetGrantReport(reportRequest);
            }
            catch (Exception ex)
            {
                reportResponse.ErrorMessage = ex.Message;
                reportResponse.Exception = ex;
            }
            return reportResponse;
        }

        [HttpPost]
        [Route("GetProjectReceivablesReport")]
        public ReportResponse GetProjectReceivablesReport(ReportRequest reportRequest)
        {
            try
            {
                reportResponse = reportRepository.GetProjectReceivablesReport(reportRequest);
            }
            catch (Exception ex)
            {
                reportResponse.ErrorMessage = ex.Message;
                reportResponse.Exception = ex;
            }
            return reportResponse;
        }

        

    }
}
