using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
{
    [RoutePrefix("Payroll")]
    public class PayrollController : ApiController
    {
        IPayrollRepository PayrollRepository;
        PayrollResponse PayrollResponse;

        public PayrollController()
        {
            PayrollRepository = new PayrollBAL();
            PayrollResponse = new PayrollResponse();
        }

        [HttpPost]
        [Route("GetPayrolls")]
        public PayrollResponse GetPayrolls(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollRepository.GetPayrolls(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }


        [HttpPost]
        [Route("GetPayroll")]
        public PayrollResponse GetPayroll(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollRepository.GetPayroll(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }


        [HttpPost]
        [Route("SavePayroll")]
        public PayrollResponse SavePayroll(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollRepository.SavePayroll(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }


    }
}
