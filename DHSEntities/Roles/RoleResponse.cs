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
        public Roles roles { get; set; }
        public List<Roles> LstRoles { get; set; }
    }
}
