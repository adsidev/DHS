using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
   public  class IssueTypeEntity :Audit
    {
        public int IssueTypeId { get; set; }
        public string IssueType { get; set; }
    }
}
