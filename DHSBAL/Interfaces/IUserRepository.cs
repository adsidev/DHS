using DHSEntities;

namespace DHSBAL
{
    public interface IUserRepository
    {
        UserResponse LoginCheck(UserRequest userRequest);
        UserResponse SaveUser(UserRequest userRequest);
        UserResponse GetUser(UserRequest userRequest);
        UserResponse GetUsers();
    }
}
