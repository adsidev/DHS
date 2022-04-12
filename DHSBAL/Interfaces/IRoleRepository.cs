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
        RoleResponse GetPermissions();
        RoleResponse GetPermission(RoleRequest statusRequest);
        RoleResponse SavePermission(RoleRequest statusRequest);
        RoleResponse CheckPermission(RoleRequest statusRequest);
    }
}

