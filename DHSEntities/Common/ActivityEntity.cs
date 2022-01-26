using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class ActivityEntity : Audit
    {
        public long ActivityId { get; set; }
        public string ActivityName { get; set; }

    }
}
