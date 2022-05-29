using System.Collections.Generic;
using PagedList;


namespace DHSEntities
{
    public class ProjectResponse : ErrorMessages
    {
        public List<ProjectEntity> projectEntities { get; set; }
        public List<ExpenseEntity> expenseEntities { get; set; }
        public List<RevenueEntity> revenueEntities { get; set;}
        public List<TransactionDetailEntity> transactionDetailEntities { get; set; }
        public List<RevenueTransactionEntity> revenueTransactionEntities { get;set; }
        public ProjectEntity projectEntity { get; set; }
        public RolePermissionEntity rolePermissionEntity { get; set; }
        public IPagedList<ProjectEntity> pagedProjectEntities { get; set; }
        public List<FiscalYearEntity> fiscalYearEntities { get; set; }
        public List<ProjectStatusEntity> projectStatusEntities { get; set; }
        public List<ProjectGroupEntity> projectGroupEntities { get; set; }
        public ProjectStatusEntity projectStatusEntity { get; set; }
    }
}
