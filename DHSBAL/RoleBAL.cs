using DHSDAL;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSBAL
{
    public class RoleBAL : IRoleRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IRoleDALRepository roleDALRepository;

        RoleResponse roleResponse;
        public RoleBAL()
        {
            roleDALRepository = new RoleDAL();
            roleResponse = new RoleResponse();
        }

        public RoleResponse SaveRolePermission(RoleRequest roleRequest)
        {
            try
            {
                roleResponse = roleDALRepository.SaveRolePermission(roleRequest);
            }
            catch (Exception ex)
            {
                roleResponse.ErrorMessage = ex.Message;
                roleResponse.Exception = ex;
            }
            return roleResponse;
        }

        public RoleResponse GetRolePermissions()
        {
            try
            {
                roleResponse = roleDALRepository.GetRolePermissions();
            }
            catch (Exception ex)
            {
                roleResponse.ErrorMessage = ex.Message;
                roleResponse.Exception = ex;
            }
            return roleResponse;
        }

        public RoleResponse GetRolePermission(RoleRequest roleRequest)
        {
            try
            {
                roleResponse = roleDALRepository.GetRolePermission(roleRequest);
            }
            catch (Exception ex)
            {
                roleResponse.ErrorMessage = ex.Message;
                roleResponse.Exception = ex;
            }
            return roleResponse;
        }

        public RoleResponse GetUserRollPermissions(RoleRequest roleRequest)
        {
            try
            {
                roleResponse = roleDALRepository.GetUserRollPermissions(roleRequest);
            }
            catch (Exception ex)
            {
                roleResponse.ErrorMessage = ex.Message;
                roleResponse.Exception = ex;
            }
            return roleResponse;
        }

        public RoleResponse PagePermissions(RoleRequest roleRequest)
        {
            try
            {
                roleResponse = roleDALRepository.PagePermissions(roleRequest);
            }
            catch (Exception ex)
            {
                roleResponse.ErrorMessage = ex.Message;
                roleResponse.Exception = ex;
            }
            return roleResponse;
        }

        public RoleResponse GetRoles()
        {
            try
            {
                roleResponse = roleDALRepository.GetRoles();
            }
            catch (Exception ex)
            {
                roleResponse.ErrorMessage = ex.Message;
                roleResponse.Exception = ex;
            }
            return roleResponse;
        }
    }
}
