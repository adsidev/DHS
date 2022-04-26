using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class ReportRequest
    {
        public int FiscalYearId { get; set; }
        public int ProjectId { get; set; }
        public int ProjectStatusId { get; set; }
    }
}
