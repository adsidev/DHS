using System.Collections.Generic;
using PagedList;


namespace DHSEntities
{
    public class RevenueResponse : ErrorMessages
    {
        public RevenueEntity revenueEntity { get; set; }
        public List<RevenueEntity> revenueEntities { get; set; }
        public List<ExpenseEntity> expenseEntities { get; set; }
        public IPagedList<RevenueEntity> pagedRevenueEntities { get; set; }
        public IPagedList<RevenueTransactionEntity> pagedrevenueTransactionEntities{ get; set; }
        public RolePermissionEntity rolePermissionEntity { get; set; }
        public List<PeriodEntity> periodEntities { get; set; }
        public List<SourceEntity> sourceEntities { get; set; }
        public List<FunctionEntity> functionEntities { get; set; }
        public List<DepartmentEntity> departmentEntities { get; set; }
        public List<OrgEntity> orgEntities { get; set; }
        public List<ObjectEntity> objectEntities { get; set; }
        public List<ProjectEntity> projectEntities { get; set; }
        public List<ActivityEntity> activityEntities { get; set; }
        public List<FGTCategoryEntity> fgtCategoryEntities { get; set; }
        public List<FGTCategoryEntity> fgtCategoryEntities2 { get; set; }
        public List<DrawEntity> drawEntities { get; set; }
        public List<DrawEntity> drawRevenueEntities { get; set; }
        public List<JournalEntity> journalEntities { get; set; }
        public List<StatusEntity> statusEntities { get; set; }
        public List<DocumentEntity> documentEntities { get; set; }
        public DocumentEntity documentEntity { get; set; }
        public List<DocumentCategoryEntity> documentCategoryEntities { get; set; }
        public List<RevenueTypeEntity> revenueTypeEntities { get; set; }
        public List<FiscalYearEntity> fiscalYearEntities { get; set; }
        public List<UserEntity> userEntities { get; set; }
        public List<VendorEntity> vendorEntities { get; set; }
        public List<RevenueTransactionEntity> revenueTransactionEntities { get; set; }
        public RevenueTransactionEntity revenueTransactionEntity { get; set; }

        public List<TransactionDetailEntity> transactionDetailEntities { get; set; }
        public List<TransactionDetailEntity> priorYearTransactionDetailEntities { get; set; }
        public TransactionDetailEntity transactionDetailEntity { get; set; }
        public IPagedList<TransactionDetailEntity> pagedTransactionDetailEntities { get; set; }
    }
}
