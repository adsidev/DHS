using System.Collections.Generic;
using PagedList;

namespace DHSEntities
{
    public class FGTCategoryResponse :ErrorMessages
    {
        public FGTCategoryEntity fGTCategoryEntity { get; set; }
        public List<FGTCategoryEntity> fGTCategoryEntities { get; set; }
        public IPagedList<FGTCategoryEntity> pagedFGTCategoryEntities { get; set; }
        public RolePermissionEntity rolePermissionEntity { get; set; }
    }
}
