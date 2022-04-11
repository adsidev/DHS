using DHSEntities;

namespace DHSDAL
{
    public interface IPayrollDALRepository
    {  
        PayrollResponse GetPayrollProjects(PayrollRequest payrollRequest);
        PayrollResponse GetPayrollProject(PayrollRequest payrollRequest);
        PayrollResponse SavePayrollProject(PayrollRequest payrollRequest);
        PayrollResponse GetPayrollDocuments(PayrollRequest payrollRequest);
        PayrollResponse GetPayrollDocument(PayrollRequest payrollRequest);
        PayrollResponse SavePayrollDocument(PayrollRequest payrollRequest);
    }
}
