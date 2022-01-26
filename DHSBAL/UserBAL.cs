using DHSDAL;
using DHSEntities;
using System;

namespace DHSBAL
{
    public class UserBAL : IUserRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IUserDALRepository userRepository;

        UserResponse userResponse;
        public UserBAL()
        {
            userRepository = new UserDAL();
            userResponse = new UserResponse();
        }

        public UserResponse LoginCheck(UserRequest userRequest)
        {
            try
            {
                userResponse = userRepository.LoginCheck(userRequest);
            }
            catch (Exception ex)
            {
                userResponse.ErrorMessage = ex.Message;
                userResponse.Exception = ex;
            }
            return userResponse;
        }

        public UserResponse SaveUser(UserRequest userRequest)
        {
            try
            {
                userResponse = userRepository.SaveUser(userRequest);
            }
            catch (Exception ex)
            {
                userResponse.ErrorMessage = ex.Message;
                userResponse.Exception = ex;
            }
            return userResponse;
        }

        public UserResponse GetUser(UserRequest userRequest)
        {
            try
            {
                userResponse = userRepository.GetUser(userRequest);
            }
            catch (Exception ex)
            {
                userResponse.ErrorMessage = ex.Message;
                userResponse.Exception = ex;
            }
            return userResponse;
        }

        public UserResponse GetUsers()
        {
            try
            {
                userResponse = userRepository.GetUsers();
            }
            catch (Exception ex)
            {
                userResponse.ErrorMessage = ex.Message;
                userResponse.Exception = ex;
            }
            return userResponse;
        }

    }
}
