using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class ExpenseRequest
    {
        public ExpenseEntity expenseEntity { get; set; }
        public TransactionDetailEntity transactionDetailEntity { get; set; }
        public DocumentEntity documentEntity { get; set; }
        public string SearchOn { get; set; }
        public string SearchKey { get; set; }

        public List<ExpenseEntity> expenseEntities { get; set; }
    }
}
