using DHSEntities;

namespace DHSBAL
{
    public interface IPayrollRepository
    {
        PayrollResponse GetPayrollProjects(PayrollRequest payrollRequest);
        PayrollResponse GetPayrollProject(PayrollRequest payrollRequest);
        PayrollResponse SavePayrollProject(PayrollRequest payrollRequest);
        PayrollResponse GetPayrollDocuments(PayrollRequest payrollRequest);
        PayrollResponse GetPayrollDocument(PayrollRequest payrollRequest);
        PayrollResponse SavePayrollDocument(PayrollRequest payrollRequest);
    }
}
