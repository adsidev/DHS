using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class ProjectEntity :Audit
    {
        public long ProjectId { get; set; }
        public long FiscalYearId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string CFDA { get; set; }
        public decimal ExpenseAmount { get; set; }
        public decimal RevenueAmount { get; set; }
        public decimal ExpenseTransactionAmount { get; set; }
        public decimal RevenueTransactionAmount { get; set; }
    }
}
