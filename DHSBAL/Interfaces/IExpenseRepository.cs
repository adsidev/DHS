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
    }
}
