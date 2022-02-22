using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class RevenueTransactionEntity :Audit
    {
        public long RevenueTransactionId { get; set; }
        public long RevenueId { get; set; }
        public string RevenueTransactionNumber { get; set; }
        public string RevenueTransactionDate { get; set; }
        public decimal RevenueTransactionAmount { get; set; }
        public int RevenueTypeId { get; set; }
        public string RevenueTypeName { get; set; }
        public string OrgName{ get; set; }
        public string ObjectName{ get; set; }
        public string ProjectName { get; set; }
        public string RevenueTranscationDescription { get; set; }
        public string SaveString { get; set; }
        public long DrawId { get; set; }
        public string DrawNumber { get; set; }
        public string BatchNumber { get; set; }
        public decimal DrawAmount { get; set; }
        public string DrawDate { get; set; }
        public int ExpenseCount { get; set; }
        public int CompleteCount { get; set; }
        public string RevenueNumber { get; set; }
        public string DocumentFile { get; set; }
    }
}
