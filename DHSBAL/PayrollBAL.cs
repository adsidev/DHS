﻿using DHSDAL;
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

        public PayrollResponse GetPayrolls(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollDALRepository.GetPayrolls(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }

        public PayrollResponse GetPayroll(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollDALRepository.GetPayroll(payrollRequest);
            }
            catch (Exception ex)
            {
                PayrollResponse.ErrorMessage = ex.Message;
                PayrollResponse.Exception = ex;
            }
            return PayrollResponse;
        }

        public PayrollResponse SavePayroll(PayrollRequest payrollRequest)
        {
            try
            {
                PayrollResponse = PayrollDALRepository.SavePayroll(payrollRequest);
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
