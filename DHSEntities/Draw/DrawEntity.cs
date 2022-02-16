using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class DrawEntity : Audit
    {
        public long DrawId { get; set; }
        public string DrawDownDate { get; set; }
        public decimal DrawDownAmount { get; set; }
        public string SaveString { get; set; }
        public string BatchNumber { get; set; }
        public string CashReceipt { get; set; }
        public string DatePosted { get; set; }
        public string DrawDescription { get; set; }
        public string DrawNumber { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int FiscalYearId { get; set; }
        public string FiscalYear { get; set;}
        public long AssignedTo { get; set; }
        public string AssignedToUser { get; set; }
        public decimal DrawAmount { get; set; }
        public decimal RevenueAmount { get; set; }
        public long RevenueId { get; set; }
        public string OrgName { get; set; }
        public string ObjectName { get; set; }
        public string ProjectName { get; set; }
        public decimal AllocatedAmount { get; set; }

    }
}
