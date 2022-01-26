using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class DocumentEntity :Audit
    {
        public long DocumentId { get; set; }
        public long DocumentReferenceId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentDescription { get; set; }
        public string DocumentFile { get; set; }
        public int DocumentCategoryId { get; set; }
        public string DocumentCategory { get;set; }
        public string SaveString { get;set; }
        public int DisplayOrder { get; set; }
    }
}
