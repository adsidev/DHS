using DHSEntities;

namespace DHSBAL
{
    public interface IPayrollRepository
    {
        PayrollResponse GetPayrolls(PayrollRequest payrollRequest);
        PayrollResponse GetPayroll(PayrollRequest payrollRequest);
        PayrollResponse SavePayroll(PayrollRequest payrollRequest);
    }
}
