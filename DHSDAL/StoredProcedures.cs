using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSDAL
{
    public class StoredProcedures
    {
        public class User
        {
            public const string USPVERIFYUSER = "sp_VerifyUser";
            public const string USPSAVEUSERACCOUNT = "sp_SaveUserAccount";
            public const string USPGETUSERACCOUNTS= "sp_GetUserAccounts";
            public const string USPGETUSERACCOUNT= "sp_GetUserAccount";
            public const string USPGETASSIGNEDTO= "sp_GetAssignedTo";
        }

        public class Role
        {
            public const string USPSAVEROLEPERMISSIONS = "sp_SaveRolePermissions";
            public const string USPGETROLEPEEMISSIONS = "sp_GetRolePermissions";
            public const string USPGETROLEPEEMISSION = "sp_GetRolePermission";
            public const string USPGETUSERROLES = "sp_GetUserRoles";
            public const string USPGETROLES = "sp_GetRoles";
        }

        public class Rule
        {
            public const string USPSAVEMEDICAIDRULE = "sp_SaveMedicaidRule";
            public const string USPGETMEDICAIDRULES = "sp_GetMedicaidRules";
            public const string USPGETMEDICAIDRULE = "sp_GetMedicaidRule";
        }

        public class Expense
        {
            public const string USPGETEXPENSES = "sp_GetExpenses";
            public const string USPGETDRAWSBYEXPENSEID = "sp_GetDrawsByExpenseId";
            public const string USPGETREVENUETRANSACTIONBYEXPENSEID = "sp_GetRevenueTransactionByExpenseId";
            public const string USPGETEXPENSE = "sp_GetExpense";
            public const string USPSAVEEXPENSE = "sp_SaveExpense";
            public const string USPGETTRANSACTIONDETAILS = "sp_GetTransactionDetails";
            public const string USPGETALLTRANSACTIONDETAILS = "sp_GetAllTransactionDetails";
            public const string USPGETTRANSACTIONDETAIL = "sp_GetTransactionDetail";
            public const string USPSAVETRANSACTIONDETAIL = "sp_SaveTransactionDetail";
            public const string USPGETEXPENSEREVENUE = "sp_GetExpenseRevenue";
            public const string USPGETLINKTOREVENUE = "sp_GetLinkToRevenue";
            public const string USPSAVELINKTOREVENUE = "sp_SaveLinkToRevenue";
            public const string USPDLINKREVENUE = "sp_SaveDLinkRevenue";
            public const string USPREMOVELINKEXPREV = "sp_RemoveLinkExpRev";
            public const string USPGETCATEGORYBYOBJECT = "sp_GetCategoryByObject";
        }

        public class Revenue
        {
            public const string USPGETREVENUES = "sp_GetRevenues";
            public const string USPGETREVENUE = "sp_GetRevenue";
            public const string USPSAVEREVENUE = "sp_SaveRevenue";
            public const string USPGETREVENUEEXPENSE = "sp_GetRevenueExpense";
            public const string USPSAVELINKTODRAW = "sp_SaveLinkToDraw";
            public const string USPGETREVENUEDRAWS = "sp_GetRevenueDraws";
            public const string USPGETLINKTODRAW = "sp_GetLinkToDraw";
            public const string USPGETREVENUETRANSACTIONS = "sp_GetRevenueTransactions";
            public const string USPGETREVENUETRANSACTION = "sp_GetRevenueTransaction";
            public const string USPSAVEREVENEUTRANSACTION = "sp_SaveRevenueTransaction";
            public const string USPGETALLREVENUETRANSACTIONS = "sp_GetAllRevenueTransactions";
            public const string USPGETTRANSACTIONSBYREVENUEID = "sp_GetTransactionsByRevenueId";
            public const string USPDELETEREVENUETRANSACTION = "sp_DeleteRevenueTransaction";
        }

        public class Draw
        {
            public const string USPGETDRAWS = "sp_GetDraws";
            public const string USPGETDRAW = "sp_GetDraw";
            public const string USPGETTRANSACTIONBYDRAWID = "sp_GetTransactionByDrawId";
            public const string USPGETREVENUESBYDRAWID = "sp_GetRevenuesByDrawId";
            public const string USPGETEXPENSESBYDRAWID = "sp_GetExpensesByDrawId";
            public const string USPCHECKDRAWBYBATCHNUMBER = "sp_CheckDrawByBatchNumber";
            public const string USPGETREVENUETRANSACTIONSBYDRAWID = "sp_GetRevenueTransactionByDrawId";
            public const string USPSAVEDRAW = "sp_SaveDraw";
            public const string USPDELETEDRAW = "sp_DeleteDraw";
        }


        public class Journal
        {
            public const string USPGETJOURNALS = "sp_GetJournals";
            public const string USPGETJOURNAL = "sp_GetJournal";
            public const string USPSAVEJOURNAL = "sp_SaveJournal";
        }


        public class FGTCategory
        {
            public const string USPGETFGTCATEGORIES = "sp_GetFGTCategories";
            public const string USPGETFGTCATEGORIESBYPARENTID = "sp_GetFGTCategoriesByParentId";
            public const string USPGETFGTCATEGORY = "sp_GetFGTCategory";
            public const string USPSAVEFGTCATEGORY = "sp_SaveFGTCategory";
            public const string USPGETFGTCATEGORIESBYTYPEID = "sp_GetFGTCategoriesByTypeId";            
        }

        public class Common
        {
            public const string USPGETPERIODS = "sp_GetPeriods";
            public const string USPGETSOURCES = "sp_GetSources";
            public const string USPGETFUNCTIONS = "sp_GetFunctions";
            public const string USPGETDEPARTMENTS = "sp_GetDepartments";
            public const string USPGETACTIVITIES = "sp_GetActivities";
            public const string USPGETORGS = "sp_GetOrgs";
            public const string USPGETOBJECTS = "sp_GetObjects";
            public const string USPGETPROJECTS = "sp_GetProjects";
            public const string USPGETSTATUSES = "sp_GetStatuses";
            public const string USPGETDOCUMENTCATEGORIES = "sp_GetDocumentCategories";
            public const string USPGETREVENUETYPES = "sp_GetRevenueTypes";
            public const string USPGETFISCALYEARS = "sp_GetFiscalYears";
        }

        public class Document
        {
            public const string USPGETDOCUMENTS = "sp_GetDocuments";
            public const string USPGETDOCUMENT = "sp_GetDocument";
            public const string USPSAVEDOCUMENT = "sp_SaveDocument";
        }

        public class Import
        {
            public const string USPIMPORTEXPENSE = "sp_ImportExpense";
            public const string USPIMPORTREVENUE = "sp_ImportRevenue";
            public const string USPIMPORTEXPENSETRANSACTION = "sp_ImportExpenseTransaction";
            public const string USPIMPORTEXPENSESTRANSACTION = "sp_ImportExpensesTransaction";
            public const string USPIMPORTDRAWREVENUETRANSACTION = "sp_ImportDrawRevenueTransaction";
            public const string USPCHECKIMPORTEXPENSE = "sp_CheckImportExpense";
            public const string USPCHECKIMPORTREVENUE = "sp_CheckImportRevenue";
        }

        public class RevenueType
        {
            public const string USPGETREVENUETYPES = "sp_GetRevenueTypes";
            public const string USPGETREVENUETYPE = "sp_GetRevenueType";
            public const string USPSAVEREVENUETYPE = "sp_SaveRevenueType";
        }
        public class Vendor
        {
            public const string USPGETVENDORS = "sp_GetVendors";
            public const string USPGETVENDOR = "sp_GetVendor";
            public const string USPCHECKVENDORBYVENDORNAME = "sp_CheckVendorByVendorName";
            public const string USPSAVEVENDOR = "sp_SaveVendor";
        }

        public class Project
        {
            public const string USPGETEXPENSESBYPROJECTID = "sp_GetExpensesByProjectId";
            public const string USPGETPROJECTSUMMARY = "sp_GetProjectSummary";
            public const string USPGETREVENUESBYPROJECTID = "sp_GetRevenuesByProjectId";
            public const string USPGETTRANSACTIONSBYPROJECTID = "sp_GetTransactionsByProjectId";
            public const string USPGETREVENUETRANSACTIONSBYPROJECTID = "sp_GetRevenueTransactionByProjectId";
            public const string USPGETPROJECTSUM = "sp_GetProjectSum";
        }

    }
}
