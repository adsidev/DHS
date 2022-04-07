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


        [HttpPost]
        [Route("GetPayrollProjects")]
        public PayrollResponse GetPayrollProjects(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollRepository.GetPayrollProjects(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }


        [HttpPost]
        [Route("GetPayrollProject")]
        public PayrollResponse GetPayrollProject(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollRepository.GetPayrollProject(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }


        [HttpPost]
        [Route("SavePayrollProject")]
        public PayrollResponse SavePayrollProject(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollRepository.SavePayrollProject(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }


        [HttpPost]
        [Route("GetPayrollDocuments")]
        public PayrollResponse GetPayrollDocuments(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollRepository.GetPayrollDocuments(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }


        [HttpPost]
        [Route("GetPayrollDocument")]
        public PayrollResponse GetPayrollDocument(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollRepository.GetPayrollDocument(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }


        [HttpPost]
        [Route("SavePayrollDocument")]
        public PayrollResponse SavePayrollDocument(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollRepository.SavePayrollDocument(payrollRequest);
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
