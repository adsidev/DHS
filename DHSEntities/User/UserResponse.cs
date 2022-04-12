using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class UserResponse : ErrorMessages
    {
        public UserEntity userEntity { get; set; }
        public List<UserEntity> userEntities { get; set; }
        public RolePermissionEntity rolePermissionEntity { get; set; }
        public AdminAndReconciliation adminAndReconciliation { get; set; }
        public List<RoleEntity> roleEntities { get; set; }
    }
}
