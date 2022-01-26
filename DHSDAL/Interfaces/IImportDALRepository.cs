﻿using DHSEntities;

namespace DHSDAL
{
    public interface IImportDALRepository
    {
        ErrorMessages ImportExpense(ImportRequest importRequest);
        ErrorMessages ImportRevenue(ImportRequest importRequest);
        ErrorMessages CheckImportExpense(ImportRequest importRequest);
        ErrorMessages CheckImportRevenue(ImportRequest importRequest);
    }
}
