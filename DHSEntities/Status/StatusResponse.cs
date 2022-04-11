using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class StatusResponse : ErrorMessages
    {
        public StatusEntity statusEntity { get; set; }
        public List<StatusEntity> statusEntities { get; set; }
        public RolePermissionEntity rolePermissionEntity { get; set; }
    }
}
