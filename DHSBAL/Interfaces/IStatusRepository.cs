using DHSEntities;

namespace DHSBAL
{
    public interface IStatusRepository
    {
        StatusResponse GetStatuses();
        StatusResponse GetStatus(StatusRequest statusRequest);
        StatusResponse SaveStatus(StatusRequest statusRequest);
        StatusResponse CheckStatus(StatusRequest statusRequest);
    }
}
