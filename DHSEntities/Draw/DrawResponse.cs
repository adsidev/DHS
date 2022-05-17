using System.Collections.Generic;
using PagedList;


namespace DHSEntities
{
    public class DrawResponse :ErrorMessages
    {
        public DrawEntity drawEntity { get; set; }
        public List<DrawEntity> drawEntities { get; set; }
        public IPagedList<DrawEntity> pagedDrawEntities { get; set; }
        public RolePermissionEntity rolePermissionEntity { get; set; }
        public List<StatusEntity> statusEntities { get; set; }
        public List<DocumentEntity> documentEntities { get; set; }
        public DocumentEntity documentEntity { get; set; }
        public List<DocumentCategoryEntity> documentCategoryEntities { get; set; }
        public List<TransactionDetailEntity> transactionDetailEntities { get; set; }
        public List<TransactionDetailEntity> priorYearTransactionDetailEntities { get; set; }
        public List<ExpenseEntity> expenseEntities{ get; set; }
        public List<RevenueEntity> revenueEntities{ get; set; }
        public List<FiscalYearEntity> fiscalYearEntities{ get; set; }
        public List<UserEntity> userEntities { get; set; }
        public List<OrgEntity> orgEntities { get; set; }
        public List<ObjectEntity> objectEntities { get; set; }
        public List<ProjectEntity> projectEntities { get; set; }
        public bool IsExists { get; set; }
        public List<RevenueTransactionEntity> revenueTransactionEntities { get; set; }
    }
}
