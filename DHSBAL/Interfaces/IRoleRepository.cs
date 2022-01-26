using DHSEntities;

namespace DHSBAL
{
    public interface IRoleRepository
    {
        RoleResponse SaveRolePermission(RoleRequest roleRequest);
        RoleResponse GetRolePermissions();
        RoleResponse GetRolePermission(RoleRequest roleRequest);
        RoleResponse GetUserRollPermissions(RoleRequest roleRequest);
        RoleResponse PagePermissions(RoleRequest roleRequest);
        RoleResponse GetRoles();
    }
}
