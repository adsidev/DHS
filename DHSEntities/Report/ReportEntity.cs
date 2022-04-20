
namespace DHSEntities
{
    public class ReportEntity
    {
        public string ExpenseDepositDate { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal CorrectAmount { get; set; }
        public string RelatedTrans { get; set; }
        public string Fund { get; set; }
        public string VendorName { get; set; }
        public string DepartmentName { get; set; }
        public string OrgName { get; set; }
        public string ObjectName { get; set; }
        public string ProjectName { get; set; }
        public string CFDA { get; set; }
        public decimal DrawDownAmount { get; set; }
        public string DrawDownDate { get; set; }
        public string BatchNumber { get;set; }
        public string CashReceipt { get; set; }
        public string DatePosted { get; set; }
        public string Remarks { get; set; }
        public long RevenueTransactionId { get; set; }
    }

    public class ProjectReceivables
    {
        public string ExpenseDepositDate { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal CorrectAmount { get; set; }
        public string Fund { get; set; }
        public string VendorName { get; set; }
        public string DepartmentName { get; set; }
        public string OrgName { get; set; }
        public string ObjectName { get; set; }
        public string ProjectName { get; set; }
        public string CFDA { get; set; }
        public decimal DrawDownAmount { get; set; }
        public string DrawDownDate { get; set; }
        public string BatchNumber { get; set; }
        public string CashReceipt { get; set; }
        public string DatePosted { get; set; }
        public string Remarks { get; set; }
        public long RevenueTransactionId { get; set; }
        public string RelatedTrans { get; set; }
    }

    public class ExpenseAdjustments
    {
        public string ExpenseAdjustmentDate { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal CorrectAmount { get; set; }
        public string Fund { get; set; }
        public string VendorName { get; set; }
        public string DepartmentName { get; set; }
        public string OrgName { get; set; }
        public string ObjectName { get; set; }
        public string ProjectName { get; set; }
        public string CFDA { get; set; }
        public string Remarks { get; set; }
        public string StatusName { get; set; }
        public string OtherBatchNumber { get; set; }
        public string RelatedTrans { get; set; }
    }

    public class RevenueDeposits
    {
        public string RevenueTransactionDate { get; set; }
        public decimal RevenueTransactionAmount { get; set; }
        public string ProjectName { get; set; }
        public string ObjectName { get; set; }
        public string OrgName { get; set; }
        public string BatchNumber { get; set; }
        public string RevenueTypeName { get; set; }
        public string Remarks { get; set; }
    }

    public class RevenueAdjustments
    {
        public string RevenueTransactionDate { get; set; }
        public decimal RevenueTransactionAmount { get; set; }
        public string ProjectName { get; set; }
        public string ObjectName { get; set; }
        public string OrgName { get; set; }
        public string BatchNumber { get; set; }
        public string RevenueTypeName { get; set; }
        public string Remarks { get; set; }
        public string RelatedTrans { get; set; }
        public string StatusName { get; set; }
    }

    public class ProjectPayables
    {
        public string ExpenseDepositDate { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal CorrectAmount { get; set; }
        public string Fund { get; set; }
        public string VendorName { get; set; }
        public string DepartmentName { get; set; }
        public string OrgName { get; set; }
        public string ObjectName { get; set; }
        public string ProjectName { get; set; }
        public string DrawProjectName { get; set; }
        public string CFDA { get; set; }
        public decimal DrawDownAmount { get; set; }
        public string DrawDownDate { get; set; }
        public string BatchNumber { get; set; }
        public string CashReceipt { get; set; }
        public string DatePosted { get; set; }
        public string Remarks { get; set; }
        public long RevenueTransactionId { get; set; }
        public string OtherBatchNumber { get; set; }
        public string RelatedTrans { get; set; }
    }
}
