using DHSDAL;
using DHSEntities;
using System;

namespace DHSBAL
{
    public class PayrollBAL : IPayrollRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IPayrollDALRepository PayrollDALRepository;

        PayrollResponse PayrollResponse;
        public PayrollBAL()
        {
            PayrollDALRepository = new PayrollDAL();
            PayrollResponse = new PayrollResponse();
        }

        public PayrollResponse GetPayrollProjects(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollDALRepository.GetPayrollProjects(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }

        public PayrollResponse GetPayrollProject(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollDALRepository.GetPayrollProject(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }

        public PayrollResponse SavePayrollProject(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollDALRepository.SavePayrollProject(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }

        public PayrollResponse GetPayrollDocuments(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollDALRepository.GetPayrollDocuments(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }

        public PayrollResponse GetPayrollDocument(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollDALRepository.GetPayrollDocument(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }

        public PayrollResponse SavePayrollDocument(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollDALRepository.SavePayrollDocument(payrollRequest);
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
