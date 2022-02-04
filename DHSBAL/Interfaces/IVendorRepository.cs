using DHSEntities;

namespace DHSBAL
{
    public interface IVendorRepository
    {
        VendorResponse GetVendors(VendorRequest vendorRequest);
        VendorResponse GetVendor(VendorRequest vendorRequest);
        VendorResponse SaveVendor(VendorRequest vendorRequest);
    }
}
