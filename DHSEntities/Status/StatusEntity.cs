using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class StatusEntity : Audit
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int StatusCount { get; set; }
    }
}
