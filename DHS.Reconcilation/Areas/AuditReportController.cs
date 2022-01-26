using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
{
    [RoutePrefix("AuditReport")]
    public class AuditReportController : ApiController
    {
        IAuditRepository auditRepository;
        AuditResponse auditResponse;

        public AuditReportController()
        {
            auditRepository = new AuditBAL();
            auditResponse = new AuditResponse();
        }

       

    }
}
