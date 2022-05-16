using DHSEntities;

namespace DHSBAL
{
    public interface IExpenseRepository
    {
        ExpenseResponse GetExpenses(ExpenseRequest expenseRequest);
        ExpenseResponse GetExpense(ExpenseRequest expenseRequest);
        ExpenseResponse SaveExpense(ExpenseRequest expenseRequest);
        ExpenseResponse GetTransactionDetails(ExpenseRequest expenseRequest);
        ExpenseResponse GetTransactionDetail(ExpenseRequest expenseRequest);
        ExpenseResponse SaveTransactionDetail(ExpenseRequest expenseRequest);
        ExpenseResponse GetExpenseRevenue(ExpenseRequest expenseRequest);
        ExpenseResponse GetLinkToRevenue(ExpenseRequest expenseRequest);
        ExpenseResponse SaveLinkToRevenue(ExpenseRequest expenseRequest);
        ExpenseResponse DLinkRevenue(ExpenseRequest expenseRequest);
        ExpenseResponse GetExpenseDocuments(ExpenseRequest expenseRequest);
        ExpenseResponse GetExpenseDocument(ExpenseRequest expenseRequest);
        ExpenseResponse SaveExpenseDocument(ExpenseRequest expenseRequest);
        ExpenseResponse GetRevenueTransactionByExpenseId(ExpenseRequest expenseRequest);
        ExpenseResponse GetAllTransactionDetails(ExpenseRequest expenseRequest);
        ExpenseResponse RemoveLinkExpRev(ExpenseRequest expenseRequest);
        ExpenseResponse GetExpenseTransactions(ExpenseRequest expenseRequest);
        ExpenseResponse SaveLinkToExpenseTransaction(ExpenseRequest expenseRequest);
        ExpenseResponse GetExpExpTransCompare(ExpenseRequest expenseRequest);
        ExpenseResponse CheckRelatedTrans(ExpenseRequest expenseRequest);
        ExpenseResponse CheckBatchNumber(ExpenseRequest expenseRequest);
        ExpenseResponse GetMissingExpenseTransactions(ExpenseRequest expenseRequest);
        ExpenseResponse GetMissingExpenseTransaction(ExpenseRequest expenseRequest);
        ExpenseResponse SaveMissingExpenseTransaction(ExpenseRequest expenseRequest);
        ExpenseResponse GetMissingRevenueTransaction(ExpenseRequest expenseRequest);
        ExpenseResponse GetMissingExpenses(ExpenseRequest expenseRequest);
        ExpenseResponse GetPriorYearExpenseTransactions(ExpenseRequest expenseRequest);
        ExpenseResponse GetPriorYearTransactionDetail(ExpenseRequest expenseRequest);
    }
}
