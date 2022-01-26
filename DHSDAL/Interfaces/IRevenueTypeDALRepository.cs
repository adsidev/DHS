using DHSEntities;

namespace DHSDAL
{
    public interface IRevenueTypeDALRepository
    {
        RevenueTypeResponse GetRevenueTypes();
        RevenueTypeResponse GetRevenueType(RevenueTypeRequest revenueTypeRequest);
        RevenueTypeResponse SaveRevenueType(RevenueTypeRequest revenueTypeRequest);
    }
}
