using DHSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSDAL
{
    public interface IUserDALRepository
    {
        UserResponse LoginCheck(UserRequest userRequest);
        UserResponse SaveUser(UserRequest userRequest);
        UserResponse GetUser(UserRequest userRequest);
        UserResponse GetUsers();
    }
}
