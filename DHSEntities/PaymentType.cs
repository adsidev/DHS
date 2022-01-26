using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class PaymentType : Audit
    {
        public int PaymentTypeID { get; set; }
        public string PaymentTypeName { get; set; }
    }
}
