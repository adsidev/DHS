using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class ProjectRequest
    {
        public ProjectEntity projectEntity { get; set; }
        public ProjectStatusEntity projectStatusEntity { get; set; }
        public ProjectGroupEntity projectGroupEntity { get; set; }
    }
}
