using DHSEntities;

namespace DHSDAL
{
    public interface IImportDALRepository
    {
        ErrorMessages ImportExpense(ImportRequest importRequest);
        ErrorMessages ImportRevenue(ImportRequest importRequest);
        ErrorMessages ImportProject(ImportRequest importRequest);
        ErrorMessages CheckImportExpense(ImportRequest importRequest);
        ErrorMessages CheckImportRevenue(ImportRequest importRequest);
        ErrorMessages ImportExpenseTransaction(ImportRequest importRequest);
        ErrorMessages ImportDrawRevenueTransaction(ImportRequest importRequest);
        ErrorMessages ImportExpensesTransaction(ImportRequest importRequest);
        ErrorMessages ImportPriorYearExpenseTransaction(ImportRequest importRequest);
    }
}
