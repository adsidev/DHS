using DHSEntities;

namespace DHSDAL
{
    public interface IRevenueDALRepository
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
    }
}
