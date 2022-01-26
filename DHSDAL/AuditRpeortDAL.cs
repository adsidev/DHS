using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DHSDAL
{
    public class AuditRpeortDAL : IAuditDALRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// FiscalYear Response <see cref="AuditResponse"/>
        /// </summary>
        AuditResponse auditResponse;

        public AuditRpeortDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            auditResponse = new AuditResponse();
        }

    }
}
