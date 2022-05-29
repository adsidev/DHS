using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class RevenueExpenseCompareEntity :ErrorMessages
    {
        public string TransactionNumber { get; set; }
        public string TransactionDate { get; set; }
        public decimal DrawDepositAmount { get; set; }
        public decimal ExpenseTotal { get; set; }
        public string OrgName { get; set; }
        public string ObjectName { get; set; }
        public string ProjectName { get; set; }
        public string RevenueTypeName { get; set; }
        public string DrawNumber { get; set; }
        public string BatchNumber { get; set; }
        public decimal DrawnAmount { get; set; }
        public string DrawnDate { get; set; }
        public string CashReceipt { get; set; }
        public string DatePosted { get; set; }
        public string Remarks { get; set; }
    }
}
