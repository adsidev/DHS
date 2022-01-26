using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class AuditResponse : ErrorMessages
    {
        public RolePermissionEntity rolePermissionEntity { get; set; }
    }
}
