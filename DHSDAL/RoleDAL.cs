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
            roleResponse.ErrorMessage = string.Empty;
            roleResponse.Message = string.Empty;
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
            List<RoleEntity> roleEntities = new List<RoleEntity>();
            var roleDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Role.USPGETROLES);
            foreach (DataRow roleDataRow in roleDataSet.Tables[0].Rows)
            {
                RoleEntity roleEntity = new RoleEntity();
                try
                {
                    roleEntity.RoleId = Convert.ToInt32(roleDataRow["RoleId"].ToString());
                    roleEntity.RoleName = roleDataRow["RoleName"].ToString();
                    roleEntity.RoleDescription = roleDataRow["Description"].ToString();
                }
                catch (Exception exception)
                {
                    roleEntity.ErrorMessage = exception.Message;
                    roleEntity.Exception = exception;
                }
                finally
                {
                    roleEntities.Add(roleEntity);
                }
            }
            if (roleEntities.Count > 1)
            {
                roleResponse.Message = string.Empty;
                roleResponse.ErrorMessage = string.Empty;
            }
            roleResponse.roleEntities = roleEntities;
            return roleResponse;
        }

        public RoleResponse GetPermissions()
        {
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Permission.USPGETPERMISSIONS);
            List<PermissionEntity> statusEntities = new List<PermissionEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                PermissionEntity permissionEntity = new PermissionEntity();
                try
                {
                    permissionEntity.PermissionId = Convert.ToInt32(expenseDataRow["PermissionId"]);
                    permissionEntity.PermissionName = expenseDataRow["PermissionName"].ToString();
                    permissionEntity.PermissionDescription = expenseDataRow["PermissionDescription"].ToString();
                }
                catch (Exception exception)
                {
                    roleResponse.ErrorMessage = exception.Message;
                    roleResponse.Exception = exception;
                }
                finally
                {
                    statusEntities.Add(permissionEntity);
                }
            }
            roleResponse.permissionEntities = statusEntities;
            return roleResponse;
        }

        public RoleResponse GetPermission(RoleRequest statusRequest)
        {
            SqlObject.Parameters = new object[] {
                   statusRequest.permissionEntity.PermissionId
                };
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Permission.USPGETPERMISSION, SqlObject.Parameters);
            PermissionEntity permissionEntity = new PermissionEntity();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                try
                {
                    permissionEntity.PermissionId = Convert.ToInt32(expenseDataRow["PermissionId"]);
                    permissionEntity.PermissionName = expenseDataRow["PermissionName"].ToString();
                    permissionEntity.PermissionDescription = expenseDataRow["PermissionDescription"].ToString();
                }
                catch (Exception exception)
                {
                    roleResponse.ErrorMessage = exception.Message;
                    roleResponse.Exception = exception;
                }
                finally
                {

                }
            }
            roleResponse.permissionEntity = permissionEntity;
            return roleResponse;
        }

        public RoleResponse SavePermission(RoleRequest statusRequest)
        {
            try
            {
                SqlObject.Parameters = new object[] {
                statusRequest.permissionEntity.PermissionId,
                statusRequest.permissionEntity.PermissionName,
                statusRequest.permissionEntity.PermissionDescription,
                statusRequest.permissionEntity.ModifiedBy,
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Permission.USPSAVEPERMISSION, SqlObject.Parameters);
                roleResponse.Message = string.Empty;
                roleResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                roleResponse.ErrorMessage = ex.Message;
                roleResponse.Exception = ex;
            }
            return roleResponse;
        }

        public RoleResponse CheckPermission(RoleRequest statusRequest)
        {
            try
            {
                SqlObject.Parameters = new object[] {
                statusRequest.permissionEntity.PermissionId,
                statusRequest.permissionEntity.PermissionName
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Permission.USPCHECKPERMISSION, SqlObject.Parameters);
                roleResponse.Message = string.Empty;
                roleResponse.ErrorMessage = string.Empty;
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
