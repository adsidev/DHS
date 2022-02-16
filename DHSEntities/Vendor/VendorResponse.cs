using System.Collections.Generic;
using PagedList;

namespace DHSEntities
{
    public class VendorResponse : ErrorMessages
    {
        public List<VendorEntity> vendorEntities { get; set; }
        public VendorEntity vendorEntity { get; set; }
        public IPagedList<VendorEntity> pagedVendorEntities { get; set; }
        public RolePermissionEntity rolePermissionEntity { get; set; }
        public bool IsExists { get; set; }
    }
}
