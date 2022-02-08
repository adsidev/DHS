using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class ReportDAL : IReportDALRepository
    { /// <summary>
      /// Connection string
      /// </summary>
        private readonly string _connectionString;

        ReportResponse reportResponse;

        public ReportDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            reportResponse = new ReportResponse();
        }

        public ReportResponse GetGrantProjectReport()
        {
            reportResponse.ErrorMessage = string.Empty;
            reportResponse.Message = string.Empty;
            try
            {
               CommonDAL commonDAL = new CommonDAL();
                reportResponse.projectEntities = commonDAL.GetProjects();
                reportResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            }
            catch (Exception exception)
            {
                reportResponse.ErrorMessage = exception.Message;
                reportResponse.Exception = exception;
            }
            finally
            {
                
            }
            return reportResponse;
        }
    }
}
