using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class TransactionDetailEntity : Audit
    {
        public long TransactionDetailId { get; set; }
        public long ExpenseId { get; set; }
        public long FiscalYearId { get; set; }
        public string FiscalYear { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionDescription { get; set; }
        public decimal TransactionAmount { get; set; }
        public long FGTCategoryId1 { get; set; }
        public string FGTCategoryName1 { get; set; }
        public string FGTCategoryName2 { get; set; }
        public long FGTCategoryId2 { get; set; }
        public string SaveString { get; set; }
        public string VendorAdjustments { get; set; }
        public string TransactionNumber { get; set; }
        public string OrgName { get; set; }
        public string ObjectName { get; set; }
        public string ProjectName { get; set; }
        public string CFDA { get; set; }
        public long DrawId { get; set; }
        public string DrawNumber { get; set; }
        public long VendorId { get; set; }
        public string VendorName { get; set; }
        public string RevenueTransactionNumber { get; set; }
        public string RevenueTransactionDate { get; set; }
        public decimal RevenueTransactionAmount { get; set;}
        public string RevenueProjectName { get; set; }
        public string BatchNumber { get; set; }
        public decimal DrawAmount { get; set; }
        public string DrawDate { get; set; }
        public string StatusName { get; set; }
        public int StatusId { get; set; }
        public int ProjectId { get; set; }
        public string ExpenseNumber { get; set; }
        public string DocumentFile { get; set; }
        public string ObjectDescription { get; set; }
        public string ActivityDescription { get; set; }
        public int ExpenseCount { get; set; }
        public string RelatedTrans { get; set; }
        public string OtherBatchNumber { get; set; }
        public decimal CorrectAmount { get; set; }
        public bool IsMissingExpense { get; set; }
        public string TransObject { get; set; }
        public string TransOrg { get; set; }
        public string TransProject { get; set; }
        public string TransSource { get; set; }
    }
}
