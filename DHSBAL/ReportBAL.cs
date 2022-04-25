using DHSDAL;
using DHSEntities;
using System;

namespace DHSBAL
{
    public class ReportBAL : IReportRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IReportDALRepository reportDALRepository;

        ReportResponse reportResponse;
        public ReportBAL()
        {
            reportDALRepository = new ReportDAL();
            reportResponse = new ReportResponse();
        }

        public ReportResponse GetGrantProjectReport()
        {
            try
            {
                reportResponse = reportDALRepository.GetGrantProjectReport();
            }
            catch (Exception ex)
            {
                reportResponse.ErrorMessage = ex.Message;
                reportResponse.Exception = ex;
            }
            return reportResponse;
        }

        public ReportResponse GetGrantReport(ReportRequest reportRequest)
        {
            try
            {
                reportResponse = reportDALRepository.GetGrantReport(reportRequest);
            }
            catch (Exception ex)
            {
                reportResponse.ErrorMessage = ex.Message;
                reportResponse.Exception = ex;
            }
            return reportResponse;
        }

        public ReportResponse GetProjectReceivablesReport(ReportRequest reportRequest)
        {
            try
            {
                reportResponse = reportDALRepository.GetProjectReceivablesReport(reportRequest);
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
