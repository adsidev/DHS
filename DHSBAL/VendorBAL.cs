using DHSDAL;
using DHSEntities;
using System;

namespace DHSBAL
{
    public class VendorBAL : IVendorRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IVendorDALRepository vendorDALRepository;

        VendorResponse vendorResponse;
        public VendorBAL()
        {
            vendorDALRepository = new VendorDAL();
            vendorResponse = new VendorResponse();
        }

        public VendorResponse GetVendors(VendorRequest vendorRequest)
        {
            try
            {
                vendorResponse = vendorDALRepository.GetVendors();
            }
            catch (Exception ex)
            {
                vendorResponse.ErrorMessage = ex.Message;
                vendorResponse.Exception = ex;
            }
            return vendorResponse;
        }

        public VendorResponse GetVendor(VendorRequest vendorRequest)
        {
            try
            {
                vendorResponse = vendorDALRepository.GetVendor(vendorRequest);
            }
            catch (Exception ex)
            {
                vendorResponse.ErrorMessage = ex.Message;
                vendorResponse.Exception = ex;
            }
            return vendorResponse;
        }

        public VendorResponse SaveVendor(VendorRequest vendorRequest)
        {
            try
            {
                vendorResponse = vendorDALRepository.SaveVendor(vendorRequest);
            }
            catch (Exception ex)
            {
                vendorResponse.ErrorMessage = ex.Message;
                vendorResponse.Exception = ex;
            }
            return vendorResponse;
        }
    }
}
