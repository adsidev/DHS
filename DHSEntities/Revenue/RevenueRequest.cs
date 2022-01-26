using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class RevenueRequest
    {
        public RevenueEntity revenueEntity { get; set; }
        public DocumentEntity documentEntity { get; set; }
        public string SearchOn { get; set; }
        public string SearchKey { get; set; }
        public RevenueTransactionEntity revenueTransactionEntity { get; set; }
    }
}
