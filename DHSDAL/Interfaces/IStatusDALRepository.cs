using DHSEntities;

namespace DHSDAL
{
    public interface IStatusDALRepository
    {
        StatusResponse GetStatuses();
        StatusResponse GetStatus(StatusRequest statusRequest);
        StatusResponse SaveStatus(StatusRequest statusRequest);
        StatusResponse CheckStatus(StatusRequest statusRequest);
    }
}
