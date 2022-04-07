using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class PayrollProjectEntity:Audit
    {
        public long PayrollProjectId { get; set; }
        public long PayrollId { get; set; }
        public string PayrollNumber { get; set; }
        public string FiscalYear { get; set; }
        public string PayrollDate { get; set; }
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string EffectiveDate { get; set; }
        public decimal PayrollTotal { get; set; }
        public string BatchNumber { get; set; }
        public decimal DrawnAmount { get; set; }
        public decimal AdjustedAmount { get; set; }
        public string Notes { get; set; }
        public string SaveString { get; set; }
    }
}
