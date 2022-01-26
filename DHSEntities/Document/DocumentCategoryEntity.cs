using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class DocumentCategoryEntity :Audit
    {
        public int DocumentCategoryId { get; set; }
        public string DocumentCategoryName { get; set; }

    }
}
