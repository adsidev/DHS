using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class PayrollRequest
    {
        public PayrollEntity payrollEntity { get; set; }
        public PayrollProjectEntity payrollProjectEntity { get; set; }
        public DocumentEntity documentEntity { get; set; }
    }
}
