

namespace DHSEntities
{
    public class VendorEntity : Audit
    {
        public long VendorId { get; set; }
        public string VendorName { get; set;}
        public string VendorCode { get; set; }
        public string SaveString { get; set; }
    }
}
