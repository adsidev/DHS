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
        RoleResponse GetPermissions();
        RoleResponse GetPermission(RoleRequest statusRequest);
        RoleResponse SavePermission(RoleRequest statusRequest);
        RoleResponse CheckPermission(RoleRequest statusRequest);
    }
}
