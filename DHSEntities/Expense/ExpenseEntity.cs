using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSEntities
{
    public class ExpenseEntity : Audit
    {
        public long ExpenseId { get; set; }
        public long PeriodId { get; set; }
        public string PeriodName { get; set; }
        public long SourceId { get; set; }
        public string SourceName { get; set; }
        public string SourceDescription { get; set; }
        public long JournalId { get; set; }
        public string Journal { get; set; }
        public string JournalName { get; set; }
        public long FunctionId { get; set; }
        public string FunctionName { get; set; }
        public string FunctionDescription { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
        public long ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public long OrgId { get; set; }
        public string OrgName { get; set; }
        public string OrgDescription { get; set; }
        public long ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string ObjectDescription { get; set; }
        public string Fund { get; set; }    
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string CFDA { get; set; }
        public string Reference1 { get;set; }
        public string Reference2 { get;set; }
        public string Reference3 { get;set; }
        public string Reference4 { get;set; }
        public long FGTCategoryId1 { get; set;}
        public long FGTCategoryId2 { get; set;}
        public string FGTCategoryName1 { get; set;}
        public string FGTCategoryName2 { get; set; }
        public decimal NetAmount { get; set; }
        public string Notes { get; set; }
        public string SaveString { get; set; }
        public string ExpenseDate { get; set; }
        public long RevenueId { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string ExpenseNumber { get; set; }
        public string RevenueNumber { get; set; }
        public int FiscalYearId { get; set; }
        public string FiscalYear { get; set; }
        public long AssignedTo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AssignedToUser { get; set; }
        public long ExpenseTransactionDetailId { get; set; }
    }
}
