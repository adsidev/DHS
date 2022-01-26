using DHSEntities;
using DHSDAL;
using System;

namespace DHSBAL
{
    public class AuditBAL :IAuditRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IAuditDALRepository auditReportRepository;

        AuditResponse auditResponse;
        public AuditBAL()
        {
            auditReportRepository = new AuditRpeortDAL();
            auditResponse = new AuditResponse();
        }


    }
}
