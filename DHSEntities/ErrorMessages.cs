using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class ErrorMessages
    {
        public string ErrorNumber { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}
