using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DHSEntities;

namespace DHS.Reconcilation.Models
{
    public class Common
    {
        public static readonly string strUserName = "UserName";
        public static readonly string UserID = "UserID";
        public static readonly int pageNumbers = 13;

        public static void AddSession(string SessionName, string SessionValue)
        {
            HttpContext.Current.Session[SessionName] = SessionValue;
        }

        public static void RemoveSession(string SessionName)
        {
            HttpContext.Current.Session.Remove(SessionName);
        }

        public static string GetSession(string SessionName)
        {
            string strSession = string.Empty;
            if (HttpContext.Current.Session[SessionName] != null)
                strSession = HttpContext.Current.Session[SessionName].ToString();
            return strSession;
        }

        public static bool SessionExists()
        {
            bool IsSessionExists = true;
            if (GetSession(strUserName) == "" || GetSession(strUserName) == null)
                IsSessionExists = false;
            return IsSessionExists;
        }

        public static RolePermissionEntity PagePermissions(int PermissionId)
        {
            List<RolePermissionEntity> rolePermission = (List<RolePermissionEntity>)HttpContext.Current.Session["UserRollPermissions"];
            RolePermissionEntity objRolePermissions = new RolePermissionEntity();
            objRolePermissions.CreateBit = false;
            objRolePermissions.EditBit = false;
            objRolePermissions.ViewBit = false;
            objRolePermissions.DeleteBit = false;

            if (rolePermission != null)
            {
                var permission = from n in rolePermission where ((n.ViewBit == true || n.EditBit == true || n.CreateBit == true || n.DeleteBit == true) && n.PermissionId == PermissionId) select n;
                foreach (var item in permission)
                {
                    objRolePermissions.CreateBit = item.CreateBit;
                    objRolePermissions.EditBit = item.EditBit;
                    objRolePermissions.ViewBit = item.ViewBit;
                    objRolePermissions.DeleteBit = item.DeleteBit;
                }
            }
            return objRolePermissions;
        }
        
        public static bool PageHasPermission(int PermissionId)
        {
            List<RolePermissionEntity> rolePermission = (List<RolePermissionEntity>)HttpContext.Current.Session["UserRollPermissions"];
            if (rolePermission != null)
            {

                var permission = from n in rolePermission where ((n.ViewBit == true || n.EditBit == true || n.CreateBit == true || n.DeleteBit == true) && n.PermissionId == PermissionId) select n;
                if (permission.Count() > 0)
                    return true;
                else
                    return false;
            }
            return true;
        }
                
        public static bool AdminPageHasPermission(int PermissionId)
        {
            List<RolePermissionEntity> rolePermission = (List<RolePermissionEntity>)HttpContext.Current.Session["UserRollPermissions"];
            if (rolePermission != null)
            {

                var permission = from n in rolePermission where ((n.EditBit == true || n.CreateBit == true || n.DeleteBit == true) && n.PermissionId == PermissionId) select n;
                if (permission.Count() > 0)
                    return true;
                else
                    return false;
            }
            return true;
        }
    }

}