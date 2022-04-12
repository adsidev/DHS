using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class PermissionEntity :Audit
    {
        public long PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }
    }
}
