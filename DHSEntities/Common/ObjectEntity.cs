using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class ObjectEntity :Audit
    {
        public long ObjectId { get; set; }
        public string ObjectName { get; set; }
    }
}
