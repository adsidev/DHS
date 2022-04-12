using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class RoleResponse :ErrorMessages
    {
        public RolePermissionEntity RolePermissionEntity { get; set; }
        public List<RolePermissionEntity> LstRolePermissionEntities { get; set; }
        public RoleEntity roleEntity { get; set; }
        public List<RoleEntity> roleEntities { get; set; }
        public List<PermissionEntity> permissionEntities { get; set; }
        public PermissionEntity permissionEntity { get; set; }
    }
}
