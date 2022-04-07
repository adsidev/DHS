using System.Collections.Generic;
using PagedList;

namespace DHSEntities
{
    public class PayrollResponse :ErrorMessages
    {
        public PayrollEntity payrollEntity { get; set; }
        public List<PayrollEntity> payrollEntities { get; set; }
        public IPagedList<PayrollEntity> pagedPayrollEntities { get; set; }
        public RolePermissionEntity rolePermissionEntity { get; set; }
        public List<FiscalYearEntity> fiscalYearEntities { get; set; }
        public List<ProjectEntity> projectEntities { get; set; }
        public PayrollProjectEntity payrollProjectEntity { get; set; }
        public List<PayrollProjectEntity> payrollProjectEntities { get; set;}
        public List<DocumentEntity> documentEntities { get; set; }
        public DocumentEntity documentEntity { get; set; }
        public List<DocumentCategoryEntity> documentCategoryEntities { get; set; }
    }
}
