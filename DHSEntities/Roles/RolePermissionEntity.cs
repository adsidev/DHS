using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class RolePermissionEntity : Audit
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string View { get; set; }
        public string Edit { get; set; }
        public string Create { get; set; }
        public string Delete { get; set; }

        public bool ViewBit { get; set; }
        public bool EditBit { get; set; }
        public bool CreateBit { get; set; }
        public bool DeleteBit { get; set; }
    }
}
