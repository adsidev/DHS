using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class UserDAL : IUserDALRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// User Response <see cref="UserResponse"/>
        /// </summary>
        UserResponse userResponse;

        public UserDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            userResponse = new UserResponse();
        }

        public UserResponse LoginCheck(UserRequest userRequest)
        {
            try
            {
                SqlObject.Parameters = new object[] {
                userRequest.UserEntity.UserName,
                userRequest.UserEntity.Password,
                };
                var userDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.User.USPVERIFYUSER, SqlObject.Parameters);
                if (userDataSet.Tables.Count == 1)
                {
                    DataRow userDataRow = userDataSet.Tables[0].Rows[0];
                    UserEntity users = new UserEntity
                    {
                        UserId = Convert.ToInt32(userDataRow["UserID"].ToString()),
                        UserName = userDataRow["UserName"].ToString(),
                        Email = userDataRow["Email"].ToString(),
                        RoleId = Convert.ToInt32(userDataRow["RoleID"].ToString()),
                        RoleName = userDataRow["RoleName"].ToString()
                    };
                    userResponse.UserEntity = users;
                    userResponse.Message = string.Empty;
                    userResponse.ErrorMessage = string.Empty;
                }
                else
                {
                    userResponse.Message = "No Records found with the given User Name and Password. Please try again with some other credentials.";
                }
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
                SqlObject.Parameters = new object[] {
                userRequest.UserId,
                userRequest.UserEntity.UserName,
                userRequest.UserEntity.Password,
                userRequest.UserEntity.Email,
                userRequest.UserEntity.RoleId,
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.User.USPSAVEUSERACCOUNT, SqlObject.Parameters);
                userResponse.Message = string.Empty;
                userResponse.ErrorMessage = string.Empty;
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
                UserEntity users = new UserEntity();
                if (userRequest.UserId>0)
                {
                    SqlObject.Parameters = new object[] { userRequest.UserId };
                    var userDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.User.USPGETUSERACCOUNT, SqlObject.Parameters);
                    DataRow userDataRow = userDataSet.Tables[0].Rows[0];
                    users.UserId = Convert.ToInt32(userDataRow["UserID"].ToString());
                    users.UserName = userDataRow["UserName"].ToString();
                    users.Email = userDataRow["Email"].ToString();
                    users.RoleId = Convert.ToInt32(userDataRow["RoleID"].ToString());
                    users.RoleName = userDataRow["RoleName"].ToString();
                    users.Password = userDataRow["Password"].ToString();
                }
                
                userResponse.UserEntity = users;
                userResponse.Message = string.Empty;
                userResponse.ErrorMessage = string.Empty;

                RoleDAL roleDAL = new RoleDAL();
                RoleResponse roleResponse = new RoleResponse();
                roleResponse = roleDAL.GetRoles();
                userResponse.LstRoles = roleResponse.LstRoles;
            }
            catch (Exception exception)
            {
                userResponse.ErrorMessage = exception.Message;
                userResponse.Exception = exception;
            }
            return userResponse;
        }

        public UserResponse GetUsers()
        {
            var lstUserEntities = new List<UserEntity>();
            var userDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.User.USPGETUSERACCOUNTS);
            foreach (DataRow userDataRow in userDataSet.Tables[0].Rows)
            {
                UserEntity users = new UserEntity();
                try
                {
                    users.UserId = Convert.ToInt32(userDataRow["UserID"].ToString());
                    users.UserName = userDataRow["UserName"].ToString();
                    users.Email = userDataRow["Email"].ToString();
                    users.RoleId = Convert.ToInt32(userDataRow["RoleID"].ToString());
                    users.RoleName = userDataRow["RoleName"].ToString();
                    users.Password = userDataRow["Password"].ToString();
                }
                catch (Exception exception)
                {
                    users.ErrorMessage = exception.Message;
                    users.Exception = exception;
                }
                finally
                {
                    lstUserEntities.Add(users);
                }
            }
            if (lstUserEntities.Count > 1)
            {
                userResponse.Message = string.Empty;
                userResponse.ErrorMessage = string.Empty;
            }
            userResponse.userEntities = lstUserEntities;
            return userResponse;
        }

        public UserResponse GetAssignedTo()
        {
            var lstUserEntities = new List<UserEntity>();
            var userDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.User.USPGETASSIGNEDTO);
            foreach (DataRow userDataRow in userDataSet.Tables[0].Rows)
            {
                UserEntity users = new UserEntity();
                try
                {
                    users.UserId = Convert.ToInt32(userDataRow["UserID"].ToString());
                    users.UserName = userDataRow["UserName"].ToString();
                    users.Email = userDataRow["Email"].ToString();
                    users.RoleId = Convert.ToInt32(userDataRow["RoleID"].ToString());
                    users.RoleName = userDataRow["RoleName"].ToString();
                    users.Password = userDataRow["Password"].ToString();
                }
                catch (Exception exception)
                {
                    users.ErrorMessage = exception.Message;
                    users.Exception = exception;
                }
                finally
                {
                    lstUserEntities.Add(users);
                }
            }
            if (lstUserEntities.Count > 1)
            {
                userResponse.Message = string.Empty;
                userResponse.ErrorMessage = string.Empty;
            }
            userResponse.userEntities = lstUserEntities;
            return userResponse;
        }
    }
}
