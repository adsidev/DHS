using System.Collections.Generic;
using PagedList;

namespace DHSEntities
{
    public class FiscalYearResponse : ErrorMessages
    {
        public FiscalYearEntity fiscalYearEntity { get; set;}
        public List<FiscalYearEntity> fiscalYearEntities { get; set;}
        public IPagedList<FiscalYearEntity> pagerFiscalYearEntities { get; set; }
        public RolePermissionEntity rolePermissionEntity { get; set; }
    }
}
