using DHSEntities;

namespace DHSDAL
{
    public interface IVendorDALRepository
    {
        VendorResponse GetVendors();
        VendorResponse GetVendor(VendorRequest vendorRequest);
        VendorResponse SaveVendor(VendorRequest vendorRequest);
    }
}
