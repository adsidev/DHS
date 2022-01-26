using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class OrgEntity:Audit
    {
        public long OrgId { get; set; }
        public string OrgName { get; set; }
    }
}
