using DHSEntities;

namespace DHSDAL
{
    public interface IPayrollDALRepository
    {
        PayrollResponse GetPayrolls(PayrollRequest payrollRequest);
        PayrollResponse GetPayroll(PayrollRequest payrollRequest);
        PayrollResponse SavePayroll(PayrollRequest payrollRequest);
    }
}
