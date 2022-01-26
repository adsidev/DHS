using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class SourceEntity :Audit
    {
        public long SourceId { get; set; }
        public string SourceName { get; set; }
    }
}
