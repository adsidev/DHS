using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class FunctionEntity :Audit
    {
        public long FunctionId { get; set; }
        public string FunctionName { get; set; }
    }
}
