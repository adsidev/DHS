﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class FiscalYearEntity :Audit
    {
        public int FiscalYearId { get; set; }
        public string FiscalYear { get; set;}
        public int FiscaYearCount { get; set; }
    }
}
