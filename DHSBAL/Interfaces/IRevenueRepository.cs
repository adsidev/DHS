using DHSEntities;

namespace DHSBAL
{
    public interface IRevenueRepository
    {
        RevenueResponse GetRevenues(RevenueRequest revenueRequest);
        RevenueResponse GetRevenue(RevenueRequest revenueRequest);
        RevenueResponse SaveRevenue(RevenueRequest revenueRequest);
        RevenueResponse GetRevenueExpense(RevenueRequest revenueRequest);
        RevenueResponse GetRevenueDocuments(RevenueRequest revenueRequest);
        RevenueResponse GetRevenueDocument(RevenueRequest revenueRequest);
        RevenueResponse SaveRevenueDocument(RevenueRequest revenueRequest);
        RevenueResponse LinkToDraw(RevenueRequest revenueRequest);
        RevenueResponse SaveLinkToDraw(RevenueRequest revenueRequest);
        RevenueResponse DrawRevenue(RevenueRequest revenueRequest);
        RevenueResponse GetRevenueTransactions(RevenueRequest revenueRequest);
        RevenueResponse GetRevenueTransaction(RevenueRequest revenueRequest);
        RevenueResponse SaveRevenueTransaction(RevenueRequest revenueRequest);
        RevenueResponse GetRevenueInformation(RevenueRequest revenueRequest);
        RevenueResponse GetRevenueTransactionDetails(RevenueRequest revenueRequest);
        RevenueResponse DeleteRevenueTransaction(RevenueRequest revenueRequest);
        RevenueResponse GetManageReceivables(RevenueRequest revenueRequest);
        RevenueResponse GetTransactionDetail(RevenueRequest revenueRequest);
        RevenueResponse SaveTransactionDetail(ExpenseRequest expenseRequest);
        RevenueResponse CheckRelatedTrans(RevenueRequest revenueRequest);
        RevenueResponse GetMissingRevenueTransactions(RevenueRequest revenueRequest);
        RevenueResponse GetMissingRevenueTransaction(RevenueRequest revenueRequest);
        RevenueResponse SaveMissingRevenueTransaction(RevenueRequest revenueRequest);
        RevenueResponse DrawMissingRevenue(RevenueRequest revenueRequest);
        RevenueResponse MissingRevenues(RevenueRequest revenueRequest);
    }
}
