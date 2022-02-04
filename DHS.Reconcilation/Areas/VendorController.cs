using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace Medicaid.Reconcilation.Areas
{
    [RoutePrefix("Vendor")]
    public class VendorController : ApiController
    {
        IVendorRepository vendorRepository;
        VendorResponse vendorResponse;

        public VendorController()
        {
            vendorRepository = new VendorBAL();
            vendorResponse = new VendorResponse();
        }

        [HttpPost]
        [Route("GetVendors")]
        public VendorResponse GetVendors(VendorRequest vendorRequest)
        {
            try
            {
                vendorResponse = vendorRepository.GetVendors(vendorRequest);
            }
            catch (Exception ex)
            {
                vendorResponse.ErrorMessage = ex.Message;
                vendorResponse.Exception = ex;
            }
            return vendorResponse;
        }

        [HttpPost]
        [Route("GetVendor")]
        public VendorResponse GetVendor(VendorRequest vendorRequest)
        {
            try
            {
                vendorResponse = vendorRepository.GetVendor(vendorRequest);
            }
            catch (Exception ex)
            {
                vendorResponse.ErrorMessage = ex.Message;
                vendorResponse.Exception = ex;
            }
            return vendorResponse;
        }

        [HttpPost]
        [Route("SaveVendor")]
        public VendorResponse SaveVendor(VendorRequest vendorRequest)
        {
            try
            {
                vendorResponse = vendorRepository.SaveVendor(vendorRequest);
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
