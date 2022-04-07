using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class PayrollEntity:Audit
    {
        public long PayrollId { get; set; }
        public string PayrollNumber { get; set; }
        public long FiscalYearId { get; set; }
        public string FiscalYear { get; set; }
        public string PayrollDate { get; set; }
        public string SaveString { get; set; }
    }
}
