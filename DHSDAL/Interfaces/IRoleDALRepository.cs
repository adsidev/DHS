using DHSEntities;

namespace DHSDAL
{
    public interface IRoleDALRepository
    {
        RoleResponse SaveRolePermission(RoleRequest roleRequest);
        RoleResponse GetRolePermissions();
        RoleResponse GetRolePermission(RoleRequest roleRequest);
        RoleResponse GetUserRollPermissions(RoleRequest roleRequest);
        RoleResponse PagePermissions(RoleRequest roleRequest);
        RoleResponse GetRoles();
    }
}
