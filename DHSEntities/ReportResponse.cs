using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class ReportResponse : ErrorMessages
    {
        public int ProjectId { get; set; }
        public int FiscalYearId { get; set; }
        public List<FiscalYearEntity> fiscalYearEntities { get; set; }
        public List<ProjectEntity> projectEntities { get; set; }
        public RolePermissionEntity rolePermissionEntity { get; set; }

        public List<ReportEntity> reportEntities { get; set;}
        public List<ProjectReceivables> projectReceivables { get; set; }
        public List<ExpenseAdjustments> expenseAdjustments { get; set; }
        public List<RevenueDeposits> revenueDeposits { get; set; }
        public List<RevenueAdjustments> revenueAdjustments { get; set; }
        public List<DrawEntity> drawEntities { get; set; }
    }
}
