using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class RevenueTypeEntity :Audit
    {
        public int RevenueTypeId { get; set; }
        public string RevenueTypeName { get; set; }
        public string SaveString { get; set; }
        public string RevenueTypeDescription { get; set; }
    }
}
