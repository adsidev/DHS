using System.Collections.Generic;
using PagedList;

namespace DHSEntities
{
    public class RevenueTypeResponse : ErrorMessages
    {
        public List<RevenueTypeEntity> RevenueTypeEntities { get; set;}
        public IPagedList<RevenueTypeEntity> pagedRevenueTypeEntities { get; set;}
        public RevenueTypeEntity RevenueTypeEntity { get; set; }
        public RolePermissionEntity rolePermissionEntity { get; set; }
    }
}
