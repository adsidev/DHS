using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
{
    [RoutePrefix("Role")]
    public class RoleController : ApiController
    {
        IRoleRepository roleRepository;
        RoleResponse roleResponse;

        public RoleController()
        {
            roleRepository = new RoleBAL();
            roleResponse = new RoleResponse();
        }

        [HttpPost]
        [Route("SaveRolePermission")]
        public RoleResponse SaveRolePermission(RoleRequest roleRequest)
        {
            try
            {
                roleResponse = roleRepository.SaveRolePermission(roleRequest);
            }
            catch (Exception ex)
            {
                roleResponse.ErrorMessage = ex.Message;
                roleResponse.Exception = ex;
            }
            return roleResponse;
        }

        [HttpGet]
        [Route("GetRolePermissions")]
        public RoleResponse GetRolePermissions()
        {
            try
            {
                roleResponse = roleRepository.GetRolePermissions();
            }
            catch (Exception ex)
            {
                roleResponse.ErrorMessage = ex.Message;
                roleResponse.Exception = ex;
            }
            return roleResponse;
        }

        [HttpPost]
        [Route("GetRolePermission")]
        public RoleResponse GetRolePermission(RoleRequest roleRequest)
        {
            try
            {
                roleResponse = roleRepository.GetRolePermission(roleRequest);
            }
            catch (Exception ex)
            {
                roleResponse.ErrorMessage = ex.Message;
                roleResponse.Exception = ex;
            }
            return roleResponse;
        }

        [HttpPost]
        [Route("GetUserRollPermissions")]
        public RoleResponse GetUserRollPermissions(RoleRequest roleRequest)
        {
            try
            {
                roleResponse = roleRepository.GetUserRollPermissions(roleRequest);
            }
            catch (Exception ex)
            {
                roleResponse.ErrorMessage = ex.Message;
                roleResponse.Exception = ex;
            }
            return roleResponse;
        }

        [HttpPost]
        [Route("PagePermissions")]
        public RoleResponse PagePermissions(RoleRequest roleRequest)
        {
            try
            {
                roleResponse = roleRepository.PagePermissions(roleRequest);
            }
            catch (Exception ex)
            {
                roleResponse.ErrorMessage = ex.Message;
                roleResponse.Exception = ex;
            }
            return roleResponse;
        }

        [HttpGet]
        [Route("GetRoles")]
        public RoleResponse GetRoles()
        {
            try
            {
                roleResponse = roleRepository.GetRoles();
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
