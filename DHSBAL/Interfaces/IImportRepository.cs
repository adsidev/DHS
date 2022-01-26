using DHSEntities;

namespace DHSBAL
{
    public interface IImportRepository
    {
        ErrorMessages ImportExpense(ImportRequest importRequest);
        ErrorMessages ImportRevenue(ImportRequest importRequest);
        ErrorMessages CheckImportExpense(ImportRequest importRequest);
        ErrorMessages CheckImportRevenue(ImportRequest importRequest);
    }
}
