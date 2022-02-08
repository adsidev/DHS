using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class ReportResponse : ErrorMessages
    {
        public List<FiscalYearEntity> fiscalYearEntities { get; set; }
        public List<ProjectEntity> projectEntities { get; set; }
        public RolePermissionEntity rolePermissionEntity { get; set; }
    }
}
