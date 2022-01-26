using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class FGTCategoryEntity :Audit
    {
        public long FGTCategoryId { get; set; }
        public string FGTCategoryName { get; set; }
        public string FGTCategoryDescription { get; set; }
        public long FGTParentCategoryId { get; set; }
        public string FGTPatentCategoryName { get; set; }
        public string SaveString { get; set; }
    }
}
