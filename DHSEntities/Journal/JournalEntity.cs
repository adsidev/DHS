using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class JournalEntity :Audit
    {
        public long JournalId { get; set; }
        public string JournalName { get; set; }
        public string JournalCode { get; set; }
        public string JournalDescription { get; set; }
        public string SaveString { get; set; }
    }
}
