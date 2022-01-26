using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
{
    [RoutePrefix("User")]
    public class UserController : ApiController
    {
        IUserRepository userRepository;
        UserResponse userResponse;

        public UserController()
        {
            userRepository = new UserBAL();
            userResponse = new UserResponse();
        }

        [HttpGet]
        [Route("GetUsers")]
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

        [HttpPost]
        [Route("GetUser")]
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

        [HttpPost]
        [Route("LoginCheck")]
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

        [HttpPost]
        [Route("SaveUser")]
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
    }
}
