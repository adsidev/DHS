﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class ProjectEntity :Audit
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
}
