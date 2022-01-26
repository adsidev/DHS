using DHSEntities;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Linq;

namespace DHSDAL
{
    public class RoleDAL : IRoleDALRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// User Response <see cref="UserResponse"/>
        /// </summary>
        RoleResponse roleResponse;

        public RoleDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            roleResponse = new RoleResponse();
        }

        public RoleResponse SaveRolePermission(RoleRequest roleRequest)
        {
            try
            {
                SqlObject.Parameters = new object[] {
                roleRequest.RolePermissionEntity.RoleId,
                roleRequest.RolePermissionEntity.RoleName,
                roleRequest.RolePermissionEntity.RoleDescription,
                roleRequest.RolePermissionEntity.PermissionId,
                roleRequest.RolePermissionEntity.View,
                roleRequest.RolePermissionEntity.Edit,
                roleRequest.RolePermissionEntity.Create,
                roleRequest.RolePermissionEntity.Delete,
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Role.USPSAVEROLEPERMISSIONS, SqlObject.Parameters);
                if (Convert.ToInt32(intResult) >= 1)
                    roleResponse.Message = Constant.SuccessfulMessage;
                else
                    roleResponse.Message = Constant.ErrorMessage;
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
            var lstRolePermissionEntities = new List<RolePermissionEntity>();
            var roleDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Role.USPGETROLEPEEMISSIONS);
            foreach (DataRow roleDataRow in roleDataSet.Tables[0].Rows)
            {
                RolePermissionEntity rolePermissionEntity = new RolePermissionEntity();
                try
                {
                    rolePermissionEntity.RoleId = Convert.ToInt32(roleDataRow["RoleId"].ToString());
                    rolePermissionEntity.RoleName = roleDataRow["RoleName"].ToString();
                    rolePermissionEntity.PermissionName = roleDataRow["Name"].ToString();
                    rolePermissionEntity.ViewBit = Convert.ToBoolean(roleDataRow["ViewIndicator"].ToString());
                    rolePermissionEntity.EditBit = Convert.ToBoolean(roleDataRow["EditIndicator"].ToString());
                    rolePermissionEntity.CreateBit = Convert.ToBoolean(roleDataRow["CreateIndicator"].ToString());
                    rolePermissionEntity.DeleteBit = Convert.ToBoolean(roleDataRow["DeleteIndicator"].ToString());
                }
                catch (Exception exception)
                {
                    rolePermissionEntity.ErrorMessage = exception.Message;
                    rolePermissionEntity.Exception = exception;
                }
                finally
                {
                    lstRolePermissionEntities.Add(rolePermissionEntity);
                }
            }
            if (lstRolePermissionEntities.Count > 1)
            {
                roleResponse.Message = string.Empty;
                roleResponse.ErrorMessage = string.Empty;
            }
            roleResponse.LstRolePermissionEntities = lstRolePermissionEntities;
            return roleResponse;
        }

        public RoleResponse GetRolePermission(RoleRequest roleRequest)
        {
            SqlObject.Parameters = new object[] {
                roleRequest.RoleId,
            };
            var lstRolePermissionEntities = new List<RolePermissionEntity>();
            var roleDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Role.USPGETROLEPEEMISSION, SqlObject.Parameters);
            foreach (DataRow roleDataRow in roleDataSet.Tables[0].Rows)
            {
                RolePermissionEntity rolePermissionEntity = new RolePermissionEntity();
                try
                {
                    rolePermissionEntity.RoleId = Convert.ToInt32(roleDataRow["RoleId"].ToString());
                    rolePermissionEntity.PermissionId = roleDataRow["PermissionID"].ToString();
                    rolePermissionEntity.RoleName = roleDataRow["RoleName"].ToString();
                    rolePermissionEntity.RoleDescription = roleDataRow["Description"].ToString();
                    rolePermissionEntity.PermissionName = roleDataRow["Name"].ToString();
                    rolePermissionEntity.ViewBit = Convert.ToBoolean(roleDataRow["ViewIndicator"].ToString());
                    rolePermissionEntity.EditBit = Convert.ToBoolean(roleDataRow["EditIndicator"].ToString());
                    rolePermissionEntity.CreateBit = Convert.ToBoolean(roleDataRow["CreateIndicator"].ToString());
                    rolePermissionEntity.DeleteBit = Convert.ToBoolean(roleDataRow["DeleteIndicator"].ToString());
                }
                catch (Exception exception)
                {
                    rolePermissionEntity.ErrorMessage = exception.Message;
                    rolePermissionEntity.Exception = exception;
                }
                finally
                {
                    roleResponse.ErrorMessage = string.Empty;
                    roleResponse.Message = string.Empty;
                    lstRolePermissionEntities.Add(rolePermissionEntity);
                }
            }
            if(lstRolePermissionEntities.Count==0)
            {
                roleResponse.Message = string.Empty;
                roleResponse.ErrorMessage = string.Empty;
            }
            roleResponse.LstRolePermissionEntities = lstRolePermissionEntities;
            return roleResponse;
        }

        public RoleResponse GetUserRollPermissions(RoleRequest roleRequest)
        {
            SqlObject.Parameters = new object[] {
                roleRequest.UserId,
            };
            var lstRolePermissionEntities = new List<RolePermissionEntity>();
            var roleDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Role.USPGETUSERROLES, SqlObject.Parameters);
            foreach (DataRow roleDataRow in roleDataSet.Tables[0].Rows)
            {
                RolePermissionEntity rolePermissionEntity = new RolePermissionEntity();
                try
                {
                    rolePermissionEntity.RoleId = Convert.ToInt32(roleDataRow["RoleId"].ToString());
                    rolePermissionEntity.PermissionName = roleDataRow["PermissionName"].ToString();
                    rolePermissionEntity.ViewBit = Convert.ToBoolean(roleDataRow["ViewIndicator"].ToString());
                    rolePermissionEntity.EditBit = Convert.ToBoolean(roleDataRow["EditIndicator"].ToString());
                    rolePermissionEntity.CreateBit = Convert.ToBoolean(roleDataRow["CreateIndicator"].ToString());
                    rolePermissionEntity.DeleteBit = Convert.ToBoolean(roleDataRow["DeleteIndicator"].ToString());
                }
                catch (Exception exception)
                {
                    rolePermissionEntity.ErrorMessage = exception.Message;
                    rolePermissionEntity.Exception = exception;
                }
                finally
                {
                    roleResponse.Message = string.Empty;
                    lstRolePermissionEntities.Add(rolePermissionEntity);
                }
            }
            if (lstRolePermissionEntities.Count == 0)
            {
                roleResponse.Message = string.Empty;
            }
            roleResponse.LstRolePermissionEntities = lstRolePermissionEntities;
            return roleResponse;
        }

        public RoleResponse PagePermissions(RoleRequest roleRequest)
        {
            var lstRolePermissionEntities = new List<RolePermissionEntity>();
            List<RolePermissionEntity> rolePermission = (List<RolePermissionEntity>)HttpContext.Current.Session["UserRollPermissions"];
            RolePermissionEntity objRolePermissions = new RolePermissionEntity();
            objRolePermissions.CreateBit = false;
            objRolePermissions.EditBit = false;
            objRolePermissions.ViewBit = false;
            objRolePermissions.DeleteBit = false;
            if (rolePermission != null)
            {
                var permission = from n in rolePermission where ((n.ViewBit == true || n.EditBit == true || n.CreateBit == true || n.DeleteBit == true) && n.PermissionName == roleRequest.PageName) select n;
                foreach (var item in permission)
                {
                    objRolePermissions.CreateBit = item.CreateBit;
                    objRolePermissions.EditBit = item.EditBit;
                    objRolePermissions.ViewBit = item.ViewBit;
                    objRolePermissions.DeleteBit = item.DeleteBit;
                    lstRolePermissionEntities.Add(objRolePermissions);
                }
            }

            roleResponse.LstRolePermissionEntities = lstRolePermissionEntities;
            return roleResponse;
        }

        public RoleResponse GetRoles()
        {
            var lstRoles = new List<Roles>();
            var roleDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Role.USPGETROLES);
            foreach (DataRow roleDataRow in roleDataSet.Tables[0].Rows)
            {
                Roles roles = new Roles();
                try
                {
                    roles.RoleId = Convert.ToInt32(roleDataRow["RoleId"].ToString());
                    roles.RoleName = roleDataRow["RoleName"].ToString();
                    roles.RoleDescription = roleDataRow["Description"].ToString();
                }
                catch (Exception exception)
                {
                    roles.ErrorMessage = exception.Message;
                    roles.Exception = exception;
                }
                finally
                {
                    lstRoles.Add(roles);
                }
            }
            if (lstRoles.Count > 1)
            {
                roleResponse.Message = string.Empty;
                roleResponse.ErrorMessage = string.Empty;
            }
            roleResponse.LstRoles = lstRoles;
            return roleResponse;
        }
    }
}
