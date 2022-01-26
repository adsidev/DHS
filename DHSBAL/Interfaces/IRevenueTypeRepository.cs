using DHSEntities;

namespace DHSBAL
{
    public interface IRevenueTypeRepository
    {
        RevenueTypeResponse GetRevenueTypes(RevenueTypeRequest revenueTypeRequest);
        RevenueTypeResponse GetRevenueType(RevenueTypeRequest revenueTypeRequest);
        RevenueTypeResponse SaveRevenueType(RevenueTypeRequest revenueTypeRequest);
    }
}
