using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class RoleRequest
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string PageName { get; set; }
        public RolePermissionEntity RolePermissionEntity { get; set; }
        public PermissionEntity permissionEntity { get; set; }
    }
}
