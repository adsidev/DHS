using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class Vendor : ErrorMessages
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string VendorDescription { get; set; }
        public int? Form64CategoryId { get; set; }
        public string Form64Category { get; set; }
        public int? ReportingFacilityId { get; set; }
        public string FacilityName { get; set; }
    }
}
