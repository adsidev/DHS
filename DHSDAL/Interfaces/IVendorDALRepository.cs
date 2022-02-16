using DHSEntities;

namespace DHSDAL
{
    public interface IVendorDALRepository
    {
        VendorResponse GetVendors(VendorRequest vendorRequest);
        VendorResponse GetVendor(VendorRequest vendorRequest);
        VendorResponse SaveVendor(VendorRequest vendorRequest);
        VendorResponse CheckVendorName(VendorRequest vendorRequest);
    }
}
