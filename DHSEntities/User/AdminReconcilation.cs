using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHSEntities
{
    public class CheckPagePermissionHeadders
    {
        public static bool PageHasPermission(string PageName)
        {
            List<RolePermissionEntity> rolePermission = (List<RolePermissionEntity>)HttpContext.Current.Session["UserRollPermissions"];
            if (rolePermission != null)
            {
                var permission = from n in rolePermission where ((n.ViewBit == true || n.EditBit == true || n.CreateBit == true || n.DeleteBit == true) && n.PermissionName == PageName) select n;
                if (permission.Count() > 0)
                    return true;
                else
                    return false;
            }
            return true;
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
    }

    public class AdminAndReconciliation
    {
        public bool isAdmin { get; set; }
        public bool IsReconciliation { get; set; }
        public bool IsCostReport { get; set; }
        public string PageToRedirectForAdmin { get; set; }
        public string PageToRedirectForReconciliation { get; set; }
        public string PageToRedirectForCostReport { get; set; }

        public static AdminAndReconciliation AdminPageOrReconciliation()
        {
            AdminAndReconciliation objAdminAndReconciliation = new AdminAndReconciliation();

            objAdminAndReconciliation.isAdmin = false;
            objAdminAndReconciliation.IsReconciliation = false;
            objAdminAndReconciliation.IsCostReport = false;


            int reconciliation = 1;

            // for Reconciliation
            if (CheckPagePermissionHeadders.PageHasPermission("Payments"))
            {
                objAdminAndReconciliation.IsReconciliation = true;
                objAdminAndReconciliation.PageToRedirectForReconciliation = "ManagePayments";

            }
            else if (CheckPagePermissionHeadders.PageHasPermission("Vendors"))
            {
                objAdminAndReconciliation.IsReconciliation = true;
                if (reconciliation == 1)
                    objAdminAndReconciliation.PageToRedirectForReconciliation = "ManageVendors";
                reconciliation++;
            }
            else if (CheckPagePermissionHeadders.PageHasPermission("Accounts"))
            {
                objAdminAndReconciliation.IsReconciliation = true;
                if (reconciliation == 1)
                    objAdminAndReconciliation.PageToRedirectForReconciliation = "ViewAccounts";
                reconciliation++;
            }
            else if (CheckPagePermissionHeadders.PageHasPermission("Reports"))
            {
                objAdminAndReconciliation.IsReconciliation = true;
                if (reconciliation == 1)
                    objAdminAndReconciliation.PageToRedirectForReconciliation = "ViewReports";
                reconciliation++;
            }
            else if (CheckPagePermissionHeadders.PageHasPermission("Form64Categories"))
            {
                objAdminAndReconciliation.IsReconciliation = true;
                if (reconciliation == 1)
                    objAdminAndReconciliation.PageToRedirectForReconciliation = "ManageForm64Categories";
                reconciliation++;
            }

            else
                objAdminAndReconciliation.IsReconciliation = false;

            if (CheckPagePermissionHeadders.PageHasPermission("Expesnses"))
            {
                objAdminAndReconciliation.IsCostReport = true;
                objAdminAndReconciliation.PageToRedirectForCostReport = "ManageExpenses";
            }
            else if (CheckPagePermissionHeadders.PageHasPermission("Revenues"))
            {
                objAdminAndReconciliation.IsCostReport = true;
                objAdminAndReconciliation.PageToRedirectForCostReport = "ManageVendors";
            }
            else if (CheckPagePermissionHeadders.PageHasPermission("CRInvoices"))
            {
                objAdminAndReconciliation.IsCostReport = true;
                objAdminAndReconciliation.PageToRedirectForCostReport = "ManageCRInvoices";
            }
            else if (CheckPagePermissionHeadders.PageHasPermission("CRReports"))
            {
                objAdminAndReconciliation.IsCostReport = true;
                objAdminAndReconciliation.PageToRedirectForCostReport = "ManageCRReports";
            }


            // For Admin
            if (CheckPagePermissionHeadders.PageHasPermission("Rules"))
            {
                objAdminAndReconciliation.isAdmin = true;
                objAdminAndReconciliation.PageToRedirectForAdmin = "ManageRules";
            }
            else if (CheckPagePermissionHeadders.PageHasPermission("Fiscal Years"))
            {
                objAdminAndReconciliation.isAdmin = true;
                objAdminAndReconciliation.PageToRedirectForAdmin = "ManageFiscalYear";
            }
            else if (CheckPagePermissionHeadders.PageHasPermission("Fiscal Year Awards"))
            {
                objAdminAndReconciliation.isAdmin = true;
                objAdminAndReconciliation.PageToRedirectForAdmin = "ManageFiscalYearAward";
            }
            else if (CheckPagePermissionHeadders.PageHasPermission("Invoices"))
            {
                objAdminAndReconciliation.isAdmin = true;
                objAdminAndReconciliation.PageToRedirectForAdmin = "ManageInvoices";

            }
            else if (CheckPagePermissionHeadders.PageHasPermission("Users"))
            {
                objAdminAndReconciliation.isAdmin = true;
                objAdminAndReconciliation.PageToRedirectForAdmin = "ManageUsers";
            }
            else if (CheckPagePermissionHeadders.PageHasPermission("Roles"))
            {
                objAdminAndReconciliation.isAdmin = true;
                objAdminAndReconciliation.PageToRedirectForAdmin = "ManageRoles";
            }
            else if (CheckPagePermissionHeadders.PageHasPermission("Set Base Line"))
            {
                objAdminAndReconciliation.isAdmin = true;
                objAdminAndReconciliation.PageToRedirectForAdmin = "CreateBaseline";
            }
            else if (CheckPagePermissionHeadders.PageHasPermission("Report Configuration"))
            {
                objAdminAndReconciliation.isAdmin = true;
                objAdminAndReconciliation.PageToRedirectForAdmin = "ManageReportConfiguration";
            }
            else
                objAdminAndReconciliation.isAdmin = false;
            return objAdminAndReconciliation;
        }
    }
}
