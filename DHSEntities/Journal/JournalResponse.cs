using System.Collections.Generic;
using PagedList;

namespace DHSEntities
{
    public class JournalResponse : ErrorMessages
    {
        public JournalEntity journalEntity { get; set; }
        public List<JournalEntity> journalEntities { get; set; }
        public IPagedList<JournalEntity> pagedJournalEntities { get; set; }
        public RolePermissionEntity rolePermissionEntity { get; set; }
    }
}
