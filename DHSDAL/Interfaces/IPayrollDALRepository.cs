using DHSEntities;

namespace DHSDAL
{
    public interface IPayrollDALRepository
    {
        PayrollResponse GetPayrolls(PayrollRequest payrollRequest);
        PayrollResponse GetPayroll(PayrollRequest payrollRequest);
        PayrollResponse SavePayroll(PayrollRequest payrollRequest);
        PayrollResponse GetPayrollProjects(PayrollRequest payrollRequest);
        PayrollResponse GetPayrollProject(PayrollRequest payrollRequest);
        PayrollResponse SavePayrollProject(PayrollRequest payrollRequest);
        PayrollResponse GetPayrollDocuments(PayrollRequest payrollRequest);
        PayrollResponse GetPayrollDocument(PayrollRequest payrollRequest);
        PayrollResponse SavePayrollDocument(PayrollRequest payrollRequest);
    }
}
