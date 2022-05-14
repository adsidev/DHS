using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class ExpenseDAL : IExpenseDALRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// FiscalPeriod Response <see cref="CostReportInvoiceResponse"/>
        /// </summary>
        ExpenseResponse expenseResponse;

        public ExpenseDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            expenseResponse = new ExpenseResponse();
        }

        public ExpenseResponse GetExpenses(ExpenseRequest expenseRequest)
        {
            var expenseEntities = new List<ExpenseEntity>();
            SqlObject.Parameters = new object[] {
                expenseRequest.expenseEntity.AssignedTo,
                expenseRequest.expenseEntity.StatusId,
                expenseRequest.expenseEntity.ProjectId,
                expenseRequest.expenseEntity.FiscalYearId,
                expenseRequest.expenseEntity.PeriodId,
                expenseRequest.expenseEntity.SourceId
            };
            var expenseDataSet= SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETEXPENSES, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
            {
                ExpenseEntity expenseEntity = new ExpenseEntity();
                try
                {
                    expenseEntity.ExpenseId = Convert.ToInt64(expenseDataRow["ExpenseId"].ToString());
                    expenseEntity.Journal = expenseDataRow["Journal"].ToString();
                    expenseEntity.JournalName = expenseDataRow["JournalName"].ToString();
                    expenseEntity.PeriodName = expenseDataRow["PeriodName"].ToString();
                    expenseEntity.SourceName = expenseDataRow["SourceCode"].ToString();
                    expenseEntity.SourceDescription = expenseDataRow["SourceDescription"].ToString();
                    expenseEntity.FunctionName = expenseDataRow["FunctionCode"].ToString();
                    expenseEntity.FunctionDescription = expenseDataRow["FunctionDescription"].ToString();
                    expenseEntity.DepartmentName = expenseDataRow["DepartmentCode"].ToString();
                    expenseEntity.DepartmentDescription = expenseDataRow["DepartmentDescription"].ToString();
                    expenseEntity.ActivityName = expenseDataRow["ActivityCode"].ToString();
                    expenseEntity.ActivityDescription = expenseDataRow["ActivityDescription"].ToString();
                    expenseEntity.OrgName = expenseDataRow["OrgCode"].ToString();
                    expenseEntity.OrgDescription = expenseDataRow["OrgDescription"].ToString();
                    expenseEntity.ObjectName = expenseDataRow["ObjectCode"].ToString();
                    expenseEntity.ObjectDescription = expenseDataRow["ObjectDescription"].ToString();
                    expenseEntity.ProjectName = expenseDataRow["ProjectCode"].ToString();
                    expenseEntity.ProjectDescription = expenseDataRow["ProjectDescription"].ToString();
                    expenseEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    expenseEntity.Fund = expenseDataRow["Fund"].ToString();
                    expenseEntity.Reference1 = expenseDataRow["Reference1"].ToString();
                    expenseEntity.Reference2 = expenseDataRow["Reference2"].ToString();
                    expenseEntity.Reference3 = expenseDataRow["Reference3"].ToString();
                    expenseEntity.Reference4 = expenseDataRow["Reference4"].ToString();
                    expenseEntity.NetAmount = Convert.ToDecimal(expenseDataRow["NetAmount"].ToString());
                    expenseEntity.ExpenseNumber = expenseDataRow["ExpenseNumber"].ToString();
                    expenseEntity.RevenueNumber = expenseDataRow["RevenueNumber"].ToString();
                    expenseEntity.StatusName = expenseDataRow["StatusName"].ToString();
                    expenseEntity.AssignedToUser = expenseDataRow["AssignedToUser"].ToString();
                    expenseEntity.FiscalYear = expenseDataRow["FiscalYear"].ToString();
                    expenseEntity.Notes = expenseDataRow["Notes"].ToString();
                    expenseEntity.ExpenseDate = Convert.ToDateTime(expenseDataRow["ExpenseDate"].ToString()).ToShortDateString();
                }
                catch (Exception exception)
                {
                    expenseEntity.ErrorMessage = exception.Message;
                    expenseEntity.Exception = exception;
                }
                finally
                {
                    expenseEntities.Add(expenseEntity);
                }
            }
            if (expenseEntities.Count >= 1 || expenseEntities.Count==0)
            {
                expenseResponse.ErrorMessage = string.Empty;
                expenseResponse.Message = string.Empty;
            }
            CommonDAL commonDAL = new CommonDAL();
            expenseResponse.statusEntities = commonDAL.GetStatuses();
            expenseResponse.projectEntities = commonDAL.GetProjects();
            expenseResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            expenseResponse.periodEntities = commonDAL.GetPeriods();
            expenseResponse.sourceEntities= commonDAL.GetSources();
            UserDAL userDAL = new UserDAL();
            expenseResponse.userEntities = userDAL.GetAssignedTo().userEntities;
            expenseResponse.expenseEntities = expenseEntities;
            return expenseResponse;
        }

        public ExpenseResponse GetExpense(ExpenseRequest expenseRequest)
        {
            expenseResponse.ErrorMessage = string.Empty;
            expenseResponse.Message = string.Empty;          
            
            expenseResponse.expenseEntity = GetExpenseEntity(expenseRequest.expenseEntity.ExpenseId);
            CommonDAL commonDAL = new CommonDAL();
            expenseResponse.activityEntities = commonDAL.GetActivities();
            expenseResponse.periodEntities = commonDAL.GetPeriods();
            expenseResponse.sourceEntities = commonDAL.GetSources();
            expenseResponse.functionEntities = commonDAL.GetFunctions();
            expenseResponse.departmentEntities = commonDAL.GetDepartments();
            expenseResponse.orgEntities = commonDAL.GetOrgs();
            expenseResponse.objectEntities = commonDAL.GetObjects();
            expenseResponse.projectEntities = commonDAL.GetProjects();
            expenseResponse.statusEntities = commonDAL.GetStatuses();
            expenseResponse.fiscalYearEntities = commonDAL.GetFiscalYears();

            JournalDAL journalDAL = new JournalDAL();
            JournalEntity journalEntity = new JournalEntity();
            JournalRequest journalRequest = new JournalRequest();
            journalRequest.journalEntity = journalEntity;

            expenseResponse.journalEntities = journalDAL.GetJournals(journalRequest).journalEntities;

            UserDAL userDAL = new UserDAL();
            expenseResponse.userEntities = userDAL.GetAssignedTo().userEntities;
            return expenseResponse;
        }

        private ExpenseEntity GetExpenseEntity(long ExpenseId)
        {

            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ErrorMessage = string.Empty;
            expenseEntity.Message = string.Empty;
            if (ExpenseId > 0)
            {
                SqlObject.Parameters = new object[] {
                   ExpenseId
                };
                var expenseDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETEXPENSE, SqlObject.Parameters);
                foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
                {
                    try
                    {
                        expenseEntity.ExpenseId = Convert.ToInt64(expenseDataRow["ExpenseId"].ToString());
                        expenseEntity.AssignedTo = Convert.ToInt64(expenseDataRow["AssignedTo"].ToString());
                        expenseEntity.RevenueId = Convert.ToInt64(expenseDataRow["RevenueId"].ToString());
                        expenseEntity.StatusId = Convert.ToInt32(expenseDataRow["StatusId"].ToString());
                        expenseEntity.Journal = expenseDataRow["Journal"].ToString();
                        expenseEntity.JournalName = expenseDataRow["JournalName"].ToString();
                        expenseEntity.PeriodName = expenseDataRow["PeriodName"].ToString();
                        expenseEntity.SourceName = expenseDataRow["SourceCode"].ToString();
                        expenseEntity.SourceDescription = expenseDataRow["SourceDescription"].ToString();
                        expenseEntity.FunctionName = expenseDataRow["FunctionCode"].ToString() + ' ' + expenseDataRow["FunctionDescription"].ToString();
                        expenseEntity.DepartmentName = expenseDataRow["DepartmentCode"].ToString();
                        expenseEntity.DepartmentDescription = expenseDataRow["DepartmentDescription"].ToString();
                        expenseEntity.ActivityName = expenseDataRow["ActivityCode"].ToString();
                        expenseEntity.ActivityDescription = expenseDataRow["ActivityDescription"].ToString();
                        expenseEntity.OrgName = expenseDataRow["OrgCode"].ToString();
                        expenseEntity.OrgDescription = expenseDataRow["OrgDescription"].ToString();
                        expenseEntity.ObjectName = expenseDataRow["ObjectCode"].ToString();
                        expenseEntity.ObjectDescription = expenseDataRow["ObjectDescription"].ToString();
                        expenseEntity.ProjectName = expenseDataRow["ProjectCode"].ToString();
                        expenseEntity.ProjectDescription = expenseDataRow["ProjectDescription"].ToString();
                        expenseEntity.CFDA = expenseDataRow["CFDA"].ToString();
                        expenseEntity.Fund = expenseDataRow["Fund"].ToString();
                        expenseEntity.Reference1 = expenseDataRow["Reference1"].ToString();
                        expenseEntity.Reference2 = expenseDataRow["Reference2"].ToString();
                        expenseEntity.Reference3 = expenseDataRow["Reference3"].ToString();
                        expenseEntity.Reference4 = expenseDataRow["Reference4"].ToString();
                        expenseEntity.ExpenseNumber = expenseDataRow["ExpenseNumber"].ToString();
                        expenseEntity.RevenueNumber = expenseDataRow["RevenueNumber"].ToString();
                        expenseEntity.StatusName = expenseDataRow["StatusName"].ToString();
                        expenseEntity.FiscalYear = expenseDataRow["FiscalYear"].ToString();
                        expenseEntity.Notes = expenseDataRow["Notes"].ToString();
                        expenseEntity.AssignedToUser = expenseDataRow["AssignedToUser"].ToString();
                        expenseEntity.NetAmount = Convert.ToDecimal(expenseDataRow["NetAmount"].ToString());
                        if (expenseDataRow["ExpenseDate"].ToString() != String.Empty)
                            expenseEntity.ExpenseDate = Convert.ToDateTime(expenseDataRow["ExpenseDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception exception)
                    {
                        expenseEntity.ErrorMessage = exception.Message;
                        expenseResponse.ErrorMessage = exception.Message;
                        expenseEntity.Exception = exception;
                        expenseResponse.Exception = exception;
                    }
                    finally
                    {

                    }
                }
            }
            return expenseEntity;
        }

        public ExpenseResponse SaveExpense(ExpenseRequest expenseRequest)
        {
            try
            {
                if (expenseRequest.expenseEntity.ExpenseId > 0)
                    expenseRequest.expenseEntity.SaveString = "U";
                else
                    expenseRequest.expenseEntity.SaveString = "I";

                SqlObject.Parameters = new object[] {
                expenseRequest.expenseEntity.SaveString,
                expenseRequest.expenseEntity.ExpenseId,
                expenseRequest.expenseEntity.PeriodId,
                expenseRequest.expenseEntity.SourceId,
                expenseRequest.expenseEntity.JournalId,
                expenseRequest.expenseEntity.FunctionId,
                expenseRequest.expenseEntity.DepartmentId,
                expenseRequest.expenseEntity.ActivityId,
                expenseRequest.expenseEntity.OrgId,
                expenseRequest.expenseEntity.ObjectId,
                expenseRequest.expenseEntity.Fund,
                expenseRequest.expenseEntity.ProjectId,
                expenseRequest.expenseEntity.Reference1,
                expenseRequest.expenseEntity.Reference2,
                expenseRequest.expenseEntity.Reference3,
                expenseRequest.expenseEntity.Reference4,
                expenseRequest.expenseEntity.NetAmount,
                expenseRequest.expenseEntity.Notes,
                expenseRequest.expenseEntity.ExpenseDate,
                expenseRequest.expenseEntity.StatusId,
                expenseRequest.expenseEntity.ModifiedBy,
                expenseRequest.expenseEntity.FiscalYearId,
                expenseRequest.expenseEntity.AssignedTo,
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Expense.USPSAVEEXPENSE, SqlObject.Parameters);
                expenseResponse.Message = string.Empty;
                expenseResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetTransactionDetails(ExpenseRequest expenseRequest)
        {
            expenseResponse.transactionDetailEntities = GetTransactionDetails(expenseRequest.expenseEntity.ExpenseId);
            return expenseResponse;
        }

        private List<TransactionDetailEntity> GetTransactionDetails(long ExpenseId)
        {
            ExpenseRequest expenseRequest = new ExpenseRequest();
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ExpenseId = ExpenseId;
            expenseRequest.expenseEntity = expenseEntity;
            expenseResponse.expenseEntity = GetExpenseEntity(ExpenseId);

            var transactionDetailEntities = new List<TransactionDetailEntity>();
            SqlObject.Parameters = new object[] {
                    ExpenseId
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETTRANSACTIONDETAILS, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
                try
                {
                    transactionDetailEntity.TransactionDetailId = Convert.ToInt64(expenseDataRow["TransactionDetailId"].ToString());
                    transactionDetailEntity.ExpenseCount = Convert.ToInt32(expenseDataRow["ExpenseCount"].ToString());
                    transactionDetailEntity.FGTCategoryName1 = expenseDataRow["FGTCategoryName1"].ToString();
                    transactionDetailEntity.FGTCategoryName2 = expenseDataRow["FGTCategoryName2"].ToString();
                    transactionDetailEntity.TransactionDescription = expenseDataRow["TransactionDescription"].ToString();
                    transactionDetailEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                    transactionDetailEntity.CorrectAmount = Convert.ToDecimal(expenseDataRow["CorrectAmount"].ToString());
                    transactionDetailEntity.VendorAdjustments = expenseDataRow["VendorAdjustments"].ToString();
                    transactionDetailEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                    transactionDetailEntity.ObjectDescription = expenseDataRow["ObjectDescription"].ToString();
                    transactionDetailEntity.ActivityDescription = expenseDataRow["ActivityDescription"].ToString();
                    transactionDetailEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    transactionDetailEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    transactionDetailEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    transactionDetailEntity.DrawNumber = expenseDataRow["DrawNumber"].ToString();
                    transactionDetailEntity.VendorName = expenseDataRow["VendorName"].ToString();
                    transactionDetailEntity.StatusName = expenseDataRow["StatusName"].ToString();
                    transactionDetailEntity.DocumentFile = expenseDataRow["DocumentFile"].ToString();
                    transactionDetailEntity.RevenueTransactionNumber = expenseDataRow["RevenueTransactionNumber"].ToString();
                    transactionDetailEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    transactionDetailEntity.RevenueTransactionAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                    transactionDetailEntity.DrawAmount = Convert.ToDecimal(expenseDataRow["DrawAmount"].ToString());
                    transactionDetailEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    transactionDetailEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                    transactionDetailEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                    transactionDetailEntity.RevenueProjectName = expenseDataRow["RevenueProjectName"].ToString();
                    try
                    {
                        transactionDetailEntity.DrawDate = Convert.ToDateTime(expenseDataRow["DrawDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        transactionDetailEntity.DrawDate = expenseDataRow["DrawDate"].ToString();
                    }
                    try
                    {
                        transactionDetailEntity.RevenueTransactionDate = Convert.ToDateTime(expenseDataRow["RevenueTransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        transactionDetailEntity.RevenueTransactionDate = expenseDataRow["RevenueTransactionDate"].ToString();
                    }
                    transactionDetailEntity.TransactionDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    
                }
                catch (Exception exception)
                {
                    transactionDetailEntity.ErrorMessage = exception.Message;
                    transactionDetailEntity.Exception = exception; 
                    transactionDetailEntity.TransactionDate = expenseDataRow["TransactionDate"].ToString();
                }
                finally
                {
                    transactionDetailEntities.Add(transactionDetailEntity);
                }
            }
            if (transactionDetailEntities.Count >= 1 || transactionDetailEntities.Count == 0)
            {
                expenseResponse.ErrorMessage = string.Empty;
                expenseResponse.Message = string.Empty;
            }
            return transactionDetailEntities;
        }

        public ExpenseResponse GetTransactionDetail(ExpenseRequest expenseRequest)
        {
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ErrorMessage = string.Empty;
            expenseResponse.ErrorMessage = string.Empty;
            expenseEntity.Message = string.Empty;
            expenseResponse.Message = string.Empty;
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            if (expenseRequest.transactionDetailEntity.TransactionDetailId > 0)
            {
                SqlObject.Parameters = new object[] {
                    expenseRequest.transactionDetailEntity.TransactionDetailId
                };
                var expenseDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETTRANSACTIONDETAIL, SqlObject.Parameters);
                foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
                {  
                    try
                    {
                        transactionDetailEntity.TransactionDetailId = Convert.ToInt64(expenseDataRow["TransactionDetailId"].ToString());
                        transactionDetailEntity.DrawId = Convert.ToInt64(expenseDataRow["DrawId"].ToString());
                        transactionDetailEntity.ExpenseId = Convert.ToInt64(expenseDataRow["ExpenseId"].ToString());
                        transactionDetailEntity.VendorId = Convert.ToInt64(expenseDataRow["VendorId"].ToString());
                        transactionDetailEntity.StatusId = Convert.ToInt32(expenseDataRow["StatusId"].ToString());
                        transactionDetailEntity.FGTCategoryId1 = Convert.ToInt64(expenseDataRow["FGTCategoryId1"].ToString());
                        transactionDetailEntity.FGTCategoryId2 = Convert.ToInt64(expenseDataRow["FGTCategoryId2"].ToString());
                        transactionDetailEntity.FGTCategoryName1 = expenseDataRow["FGTCategoryName1"].ToString();
                        transactionDetailEntity.FGTCategoryName2 = expenseDataRow["FGTCategoryName2"].ToString();
                        transactionDetailEntity.TransactionDescription = expenseDataRow["TransactionDescription"].ToString();
                        transactionDetailEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                        transactionDetailEntity.CorrectAmount = Convert.ToDecimal(expenseDataRow["CorrectAmount"].ToString());
                        transactionDetailEntity.VendorAdjustments = expenseDataRow["VendorAdjustments"].ToString();
                        transactionDetailEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                        transactionDetailEntity.DrawNumber = expenseDataRow["DrawNumber"].ToString();
                        transactionDetailEntity.VendorName = expenseDataRow["VendorName"].ToString();
                        transactionDetailEntity.StatusName = expenseDataRow["StatusName"].ToString();
                        transactionDetailEntity.RevenueTransactionAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                        transactionDetailEntity.RevenueTransactionNumber = expenseDataRow["RevenueTransactionNumber"].ToString();
                        transactionDetailEntity.RevenueProjectName = expenseDataRow["RevenueProjectName"].ToString();
                        transactionDetailEntity.DrawAmount = Convert.ToDecimal(expenseDataRow["DrawAmount"].ToString());
                        transactionDetailEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                        transactionDetailEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                        transactionDetailEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                        try
                        {
                            transactionDetailEntity.DrawDate = Convert.ToDateTime(expenseDataRow["DrawDate"].ToString()).ToShortDateString();
                        }
                        catch (Exception)
                        {
                            transactionDetailEntity.DrawDate = expenseDataRow["DrawDate"].ToString();
                        }
                        try
                        {
                            transactionDetailEntity.RevenueTransactionDate = Convert.ToDateTime(expenseDataRow["RevenueTransactionDate"].ToString()).ToShortDateString();
                        }
                        catch (Exception)
                        {
                            transactionDetailEntity.RevenueTransactionDate = expenseDataRow["RevenueTransactionDate"].ToString();
                        }                        
                        transactionDetailEntity.TransactionDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception exception)
                    {
                        transactionDetailEntity.ErrorMessage = exception.Message;
                        transactionDetailEntity.Exception = exception;
                        transactionDetailEntity.TransactionDate = expenseDataRow["TransactionDate"].ToString();
                    }
                }
            }
            else
            {
                transactionDetailEntity.ExpenseId = expenseRequest.transactionDetailEntity.ExpenseId;
                expenseResponse.drawEntities = GetDrawsByExpenseId(expenseEntity.ExpenseId);
                SqlObject.Parameters = new object[] {
                    expenseRequest.transactionDetailEntity.ExpenseId,
                };
                var revenueDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETCATEGORYBYOBJECT, SqlObject.Parameters);
                foreach (DataRow revenueDataRow in revenueDataSet.Tables[0].Rows)
                {
                    transactionDetailEntity.FGTCategoryId1= Convert.ToInt64(revenueDataRow["FgtParentCategoryId"].ToString()) ;
                    transactionDetailEntity.FGTCategoryId2= Convert.ToInt64(revenueDataRow["FGTCategoryId"].ToString()) ;
                }
            }
            expenseResponse.expenseEntity = expenseEntity;
            expenseResponse.transactionDetailEntity = transactionDetailEntity;

            FGTCategoryEntity fGTCategoryEntity = new FGTCategoryEntity();
            FGTCategoryRequest fGTCategoryRequest = new FGTCategoryRequest();
            FGTCategoryDAL fGTCategoryDAL = new FGTCategoryDAL();
            FGTCategoryResponse fGTCategoryResponse;
            fGTCategoryEntity.FGTParentCategoryId = 0;
            fGTCategoryRequest.fGTCategoryEntity = fGTCategoryEntity;
            fGTCategoryResponse = fGTCategoryDAL.GetFGTCategoriesById(fGTCategoryRequest);
            expenseResponse.fgtCategoryEntities = fGTCategoryResponse.fGTCategoryEntities;

            fGTCategoryEntity.FGTParentCategoryId = transactionDetailEntity.FGTCategoryId1;
            fGTCategoryRequest.fGTCategoryEntity = fGTCategoryEntity;
            fGTCategoryResponse = fGTCategoryDAL.GetFGTCategoriesById(fGTCategoryRequest);
            expenseResponse.fgtCategoryEntities2 = fGTCategoryResponse.fGTCategoryEntities;
            
            expenseResponse.drawEntities = GetDrawsByExpenseId(transactionDetailEntity.ExpenseId);
           // expenseResponse.revenueTransactionEntities = GetRevenueTransactionByExpenseId(transactionDetailEntity.ExpenseId);
            expenseResponse.transactionDetailEntities = GetTransactionDetails(transactionDetailEntity.ExpenseId);
            VendorDAL vendorDAL = new VendorDAL();
            VendorRequest vendorRequest = new VendorRequest();
            VendorEntity vendorEntity = new VendorEntity();
            vendorEntity.VendorName = string.Empty;
            vendorRequest.vendorEntity = vendorEntity;
            expenseResponse.vendorEntities = vendorDAL.GetVendors(vendorRequest).vendorEntities;
            
            CommonDAL commonDAL = new CommonDAL();
            expenseResponse.statusEntities = commonDAL.GetStatuses();

            return expenseResponse;
        }

        public ExpenseResponse SaveTransactionDetail(ExpenseRequest expenseRequest)
        {
            try
            {
                if (expenseRequest.transactionDetailEntity.TransactionDetailId > 0)
                    expenseRequest.transactionDetailEntity.SaveString = "U";
                else
                    expenseRequest.transactionDetailEntity.SaveString = "I";

                SqlObject.Parameters = new object[] {
                expenseRequest.transactionDetailEntity.SaveString,
                expenseRequest.transactionDetailEntity.TransactionDetailId,
                expenseRequest.transactionDetailEntity.ExpenseId,
                expenseRequest.transactionDetailEntity.TransactionDate,
                expenseRequest.transactionDetailEntity.FGTCategoryId1,
                expenseRequest.transactionDetailEntity.FGTCategoryId2,
                expenseRequest.transactionDetailEntity.TransactionDescription,
                expenseRequest.transactionDetailEntity.ModifiedBy,
                expenseRequest.transactionDetailEntity.TransactionAmount,
                expenseRequest.transactionDetailEntity.VendorAdjustments,
                expenseRequest.transactionDetailEntity.DrawId,
                expenseRequest.transactionDetailEntity.VendorId,
                expenseRequest.transactionDetailEntity.StatusId,
                expenseRequest.transactionDetailEntity.RelatedTrans,
                expenseRequest.transactionDetailEntity.CorrectAmount,
                expenseRequest.transactionDetailEntity.OtherBatchNumber,
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Expense.USPSAVETRANSACTIONDETAIL, SqlObject.Parameters);
                expenseResponse.Message = string.Empty;
                expenseResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;

        }
        
        public ExpenseResponse GetExpenseRevenue(ExpenseRequest expenseRequest)
        {
            var revenueEntities = new List<RevenueEntity>();
            SqlObject.Parameters = new object[] {
                expenseRequest.expenseEntity.ExpenseId,
            };
            var revenueDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETEXPENSEREVENUE, SqlObject.Parameters);
            foreach (DataRow revenueDataRow in revenueDataSet.Tables[0].Rows)
            {
                RevenueEntity revenueEntity = new RevenueEntity();
                try
                {
                    revenueEntity.ExpenseRevenueId = Convert.ToInt64(revenueDataRow["ExpenseRevenueId"].ToString());
                    revenueEntity.RevenueId = Convert.ToInt64(revenueDataRow["RevenueId"].ToString());
                    revenueEntity.Journal = revenueDataRow["Journal"].ToString();
                    revenueEntity.JournalName = revenueDataRow["JournalName"].ToString();
                    revenueEntity.PeriodName = revenueDataRow["PeriodName"].ToString();
                    revenueEntity.SourceName = revenueDataRow["SourceCode"].ToString();
                    revenueEntity.SourceDescription = revenueDataRow["SourceDescription"].ToString();
                    revenueEntity.FunctionName = revenueDataRow["FunctionCode"].ToString();
                    revenueEntity.FunctionDescription = revenueDataRow["FunctionDescription"].ToString();
                    revenueEntity.DepartmentName = revenueDataRow["DepartmentCode"].ToString();
                    revenueEntity.DepartmentDescription = revenueDataRow["DepartmentDescription"].ToString();
                    revenueEntity.ActivityName = revenueDataRow["ActivityCode"].ToString();
                    revenueEntity.ActivityDescription = revenueDataRow["ActivityDescription"].ToString();
                    revenueEntity.OrgName = revenueDataRow["OrgCode"].ToString();
                    revenueEntity.OrgDescription = revenueDataRow["OrgDescription"].ToString();
                    revenueEntity.ObjectName = revenueDataRow["ObjectCode"].ToString();
                    revenueEntity.ObjectDescription = revenueDataRow["ObjectDescription"].ToString();
                    revenueEntity.ProjectName = revenueDataRow["ProjectCode"].ToString();
                    revenueEntity.ProjectDescription = revenueDataRow["ProjectDescription"].ToString();
                    revenueEntity.RevenueNumber = revenueDataRow["RevenueNumber"].ToString();
                    revenueEntity.StatusName = revenueDataRow["StatusName"].ToString();
                    revenueEntity.AssignedToUser = revenueDataRow["AssignedToUser"].ToString();
                    revenueEntity.RevenueTypeName = revenueDataRow["RevenueTypeName"].ToString();
                    revenueEntity.FiscalYear = revenueDataRow["FiscalYear"].ToString();
                    revenueEntity.CFDA = revenueDataRow["CFDA"].ToString();
                    revenueEntity.Notes = revenueDataRow["Notes"].ToString();
                    revenueEntity.Fund = revenueDataRow["Fund"].ToString();
                    revenueEntity.Reference1 = revenueDataRow["Reference1"].ToString();
                    revenueEntity.Reference2 = revenueDataRow["Reference2"].ToString();
                    revenueEntity.Reference3 = revenueDataRow["Reference3"].ToString();
                    revenueEntity.Reference4 = revenueDataRow["Reference4"].ToString();
                    revenueEntity.NetAmount = Convert.ToDecimal(revenueDataRow["NetAmount"].ToString());
                    revenueEntity.RevenueDate = Convert.ToDateTime(revenueDataRow["RevenueDate"].ToString()).ToShortDateString();
                }
                catch (Exception exception)
                {
                    revenueEntity.ErrorMessage = exception.Message;
                    revenueEntity.Exception = exception;
                }
                finally
                {
                    revenueEntities.Add(revenueEntity);
                }
            }
            if (revenueEntities.Count >= 1 || revenueEntities.Count == 0)
            {
                expenseResponse.ErrorMessage = string.Empty;
                expenseResponse.Message = string.Empty;
            }
            expenseResponse.expenseEntity = GetExpenseEntity(expenseRequest.expenseEntity.ExpenseId);
            expenseResponse.revenueEntities = revenueEntities;
            return expenseResponse;
        }

        public ExpenseResponse GetLinkToRevenue(ExpenseRequest expenseRequest)
        {
            var revenueEntities = new List<RevenueEntity>();
            SqlObject.Parameters = new object[] {
                expenseRequest.expenseEntity.ExpenseId,
                expenseRequest.expenseEntity.ProjectId
            };
            var revenueDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETLINKTOREVENUE, SqlObject.Parameters);
            foreach (DataRow revenueDataRow in revenueDataSet.Tables[0].Rows)
            {
                RevenueEntity revenueEntity = new RevenueEntity();
                try
                {
                    revenueEntity.RevenueId = Convert.ToInt64(revenueDataRow["RevenueId"].ToString());
                    revenueEntity.Journal = revenueDataRow["Journal"].ToString();
                    revenueEntity.JournalName = revenueDataRow["JournalName"].ToString();
                    revenueEntity.PeriodName = revenueDataRow["PeriodName"].ToString();
                    revenueEntity.SourceName = revenueDataRow["SourceCode"].ToString();
                    revenueEntity.SourceDescription = revenueDataRow["SourceDescription"].ToString();
                    revenueEntity.FunctionName = revenueDataRow["FunctionCode"].ToString();
                    revenueEntity.FunctionDescription = revenueDataRow["FunctionDescription"].ToString();
                    revenueEntity.DepartmentName = revenueDataRow["DepartmentCode"].ToString();
                    revenueEntity.DepartmentDescription = revenueDataRow["DepartmentDescription"].ToString();
                    revenueEntity.ActivityName = revenueDataRow["ActivityCode"].ToString();
                    revenueEntity.ActivityDescription = revenueDataRow["ActivityDescription"].ToString();
                    revenueEntity.OrgName = revenueDataRow["OrgCode"].ToString();
                    revenueEntity.OrgDescription = revenueDataRow["OrgDescription"].ToString();
                    revenueEntity.ObjectName = revenueDataRow["ObjectCode"].ToString();
                    revenueEntity.ObjectDescription = revenueDataRow["ObjectDescription"].ToString();
                    revenueEntity.ProjectName = revenueDataRow["ProjectCode"].ToString();
                    revenueEntity.ProjectDescription = revenueDataRow["ProjectDescription"].ToString();
                    revenueEntity.RevenueNumber = revenueDataRow["RevenueNumber"].ToString();
                    revenueEntity.StatusName = revenueDataRow["StatusName"].ToString();
                    revenueEntity.AssignedToUser = revenueDataRow["AssignedToUser"].ToString();
                    revenueEntity.RevenueTypeName = revenueDataRow["RevenueTypeName"].ToString();
                    revenueEntity.FiscalYear = revenueDataRow["FiscalYear"].ToString();
                    revenueEntity.CFDA = revenueDataRow["CFDA"].ToString();
                    revenueEntity.Notes = revenueDataRow["Notes"].ToString();
                    revenueEntity.Fund = revenueDataRow["Fund"].ToString();
                    revenueEntity.Reference1 = revenueDataRow["Reference1"].ToString();
                    revenueEntity.Reference2 = revenueDataRow["Reference2"].ToString();
                    revenueEntity.Reference3 = revenueDataRow["Reference3"].ToString();
                    revenueEntity.Reference4 = revenueDataRow["Reference4"].ToString();
                    revenueEntity.NetAmount = Convert.ToDecimal(revenueDataRow["NetAmount"].ToString());
                    revenueEntity.RevenueDate = Convert.ToDateTime(revenueDataRow["RevenueDate"].ToString()).ToShortDateString();
                }
                catch (Exception exception)
                {
                    revenueEntity.ErrorMessage = exception.Message;
                    revenueEntity.Exception = exception;
                }
                finally
                {
                    revenueEntities.Add(revenueEntity);
                }
            }
            if (revenueEntities.Count >= 1 || revenueEntities.Count == 0)
            {
                expenseResponse.ErrorMessage = string.Empty;
                expenseResponse.Message = string.Empty;
            }
            expenseResponse.revenueEntities = revenueEntities;
            expenseResponse.expenseEntity = GetExpenseEntity(expenseRequest.expenseEntity.ExpenseId);
            CommonDAL commonDAL = new CommonDAL();
            expenseResponse.projectEntities = commonDAL.GetProjects();
            return expenseResponse;
        }

        public ExpenseResponse SaveLinkToRevenue(ExpenseRequest expenseRequest)
        {
            try
            {
                var revenueEntities = new List<RevenueEntity>();
                SqlObject.Parameters = new object[] {
                    expenseRequest.expenseEntity.ExpenseId,
                    expenseRequest.expenseEntity.ProjectId
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Expense.USPSAVELINKTOREVENUE, SqlObject.Parameters);
                expenseResponse.Message = string.Empty;
                expenseResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse DLinkRevenue(ExpenseRequest expenseRequest)
        {

            try
            {
                var revenueEntities = new List<RevenueEntity>();
                SqlObject.Parameters = new object[] {
                    expenseRequest.expenseEntity.ExpenseId,
                    expenseRequest.expenseEntity.ProjectId
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Expense.USPDLINKREVENUE, SqlObject.Parameters);
                expenseResponse.Message = string.Empty;
                expenseResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetExpenseDocuments(ExpenseRequest expenseRequest)
        {
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ErrorMessage = string.Empty;
            expenseResponse.ErrorMessage = string.Empty;
            expenseEntity.Message = string.Empty;
            expenseResponse.Message = string.Empty;
            expenseResponse.expenseEntity = GetExpenseEntity(expenseRequest.expenseEntity.ExpenseId);
            DocumentDAL documentDAL = new DocumentDAL();
            DocumentEntity documentEntity = new DocumentEntity();
            documentEntity.DocumentReferenceId = expenseRequest.expenseEntity.ExpenseId;
            documentEntity.DocumentTypeId = 1;
            expenseResponse.documentEntities = documentDAL.GetDocuments(documentEntity);
            return expenseResponse;
        }

        public ExpenseResponse GetExpenseDocument(ExpenseRequest expenseRequest)
        {
            expenseResponse.ErrorMessage = string.Empty;
            expenseResponse.Message = string.Empty;
            DocumentDAL documentDAL = new DocumentDAL();
            DocumentEntity documentEntity = new DocumentEntity();
            documentEntity.DocumentReferenceId = expenseRequest.documentEntity.DocumentReferenceId;
            documentEntity.DocumentId = expenseRequest.documentEntity.DocumentId;
            documentEntity.DocumentTypeId = 1;
            expenseResponse.documentEntity = documentDAL.GetDocument(documentEntity);
            CommonDAL commonDAL = new CommonDAL();
            expenseResponse.documentCategoryEntities = commonDAL.GetDocumentCategories();
            return expenseResponse;
        }
        
        public ExpenseResponse SaveExpenseDocument(ExpenseRequest expenseRequest)
        {
            expenseResponse.ErrorMessage = string.Empty;
            expenseResponse.Message = string.Empty;
            DocumentDAL documentDAL = new DocumentDAL();
            documentDAL.SaveDocument(expenseRequest.documentEntity);
            return expenseResponse;
        }

        private List<DrawEntity> GetDrawsByExpenseId(long expenseId)
        {
            var drawEntities = new List<DrawEntity>();
            SqlObject.Parameters = new object[] {
                expenseId
            };
            var drawDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETDRAWSBYEXPENSEID, SqlObject.Parameters);
            foreach (DataRow drawDataRow in drawDataSet.Tables[0].Rows)
            {
                DrawEntity drawEntity = new DrawEntity();
                try
                {
                    drawEntity.DrawId = Convert.ToInt64(drawDataRow["DrawId"].ToString());
                    drawEntity.DrawDownAmount = Convert.ToDecimal(drawDataRow["DarwDownAmount"].ToString());
                    drawEntity.DrawDownDate = Convert.ToDateTime(drawDataRow["DrawDownDate"].ToString()).ToShortDateString();
                    if (drawDataRow["DatePosted"].ToString() != "")
                        drawEntity.DatePosted = Convert.ToDateTime(drawDataRow["DatePosted"].ToString()).ToShortDateString();
                    drawEntity.BatchNumber = drawDataRow["BatchNumber"].ToString();
                    drawEntity.DrawNumber = drawDataRow["DrawNumber"].ToString();
                    drawEntity.StatusName = drawDataRow["StatusName"].ToString();
                    drawEntity.FiscalYear = drawDataRow["FiscalYear"].ToString();
                    drawEntity.DrawNumber = drawDataRow["DrawNumber"].ToString();
                    drawEntity.CashReceipt = drawDataRow["CashReceipt"].ToString();
                    drawEntity.DrawDescription = drawDataRow["DrawDescription"].ToString();
                    drawEntity.AssignedToUser = drawDataRow["AssignedToUser"].ToString();
                }
                catch (Exception exception)
                {
                    drawEntity.ErrorMessage = exception.Message;
                    drawEntity.Exception = exception;
                }
                finally
                {
                    drawEntities.Add(drawEntity);
                }
            }
            if (drawEntities.Count >= 1 || drawEntities.Count == 0)
            {
                expenseResponse.ErrorMessage = string.Empty;
                expenseResponse.Message = string.Empty;
            }
            return drawEntities;
        }

        public ExpenseResponse GetRevenueTransactionByExpenseId(ExpenseRequest expenseRequest)
        {
            List<RevenueTransactionEntity> revenueTransactionEntities = new List<RevenueTransactionEntity>();
            SqlObject.Parameters = new object[] {
                expenseRequest.transactionDetailEntity.ExpenseId,
                expenseRequest.transactionDetailEntity.ProjectId
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETREVENUETRANSACTIONBYEXPENSEID, SqlObject.Parameters);
            foreach (DataRow revenueDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
                try
                {
                    revenueTransactionEntity.RevenueTransactionId = Convert.ToInt64(revenueDataRow["RevenueTransactionId"].ToString());
                    revenueTransactionEntity.RevenueTypeName = revenueDataRow["RevenueTypeName"].ToString();
                    revenueTransactionEntity.RevenueTranscationDescription = revenueDataRow["RevenueTranscationDescription"].ToString();
                    revenueTransactionEntity.RevenueTransactionAmount = Convert.ToDecimal(revenueDataRow["RevenueTransactionAmount"].ToString());
                    revenueTransactionEntity.RevenueTransactionNumber = revenueDataRow["RevenueTransactionNumber"].ToString();
                    revenueTransactionEntity.OrgName = revenueDataRow["OrgName"].ToString();
                    revenueTransactionEntity.ObjectName = revenueDataRow["ObjectName"].ToString();
                    revenueTransactionEntity.ProjectName = revenueDataRow["ProjectName"].ToString();
                    revenueTransactionEntity.DrawNumber = revenueDataRow["DrawNumber"].ToString();
                    revenueTransactionEntity.DrawAmount = Convert.ToDecimal(revenueDataRow["DarwDownAmount"].ToString());
                    revenueTransactionEntity.BatchNumber = revenueDataRow["BatchNumber"].ToString();
                    revenueTransactionEntity.RevenueTransactionDate = Convert.ToDateTime(revenueDataRow["RevenueTransactionDate"].ToString()).ToShortDateString();
                    revenueTransactionEntity.DrawDate = Convert.ToDateTime(revenueDataRow["DrawDate"].ToString()).ToShortDateString();
                }
                catch (Exception exception)
                {
                    revenueTransactionEntity.ErrorMessage = exception.Message;
                    revenueTransactionEntity.Exception = exception;
                    revenueTransactionEntity.RevenueTransactionDate = revenueDataRow["RevenueTransactionDate"].ToString();
                    revenueTransactionEntity.DrawDate = revenueDataRow["DrawDate"].ToString()   ;
                }
                finally
                {
                    revenueTransactionEntities.Add(revenueTransactionEntity);
                }
            }
            if (revenueTransactionEntities.Count >= 1 || revenueTransactionEntities.Count == 0)
            {
                expenseResponse.ErrorMessage = string.Empty;
                expenseResponse.Message = string.Empty;
            }
            expenseResponse.revenueTransactionEntities = revenueTransactionEntities;
            CommonDAL commonDAL = new CommonDAL();
            expenseResponse.projectEntities = commonDAL.GetProjects();
            return expenseResponse;
        }

        public ExpenseResponse GetAllTransactionDetails(ExpenseRequest expenseRequest)
        {
            var transactionDetailEntities = new List<TransactionDetailEntity>();
            SqlObject.Parameters = new object[] {
                    expenseRequest.transactionDetailEntity.ProjectId,
                    expenseRequest.transactionDetailEntity.TransactionNumber,
                    expenseRequest.transactionDetailEntity.RevenueTransactionNumber,
                    expenseRequest.transactionDetailEntity.StatusId,
                    expenseRequest.transactionDetailEntity.FGTCategoryId2,
                    expenseRequest.transactionDetailEntity.FiscalYearId,
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETALLTRANSACTIONDETAILS, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
                try
                {
                    transactionDetailEntity.ExpenseNumber = expenseDataRow["ExpenseNumber"].ToString();
                    transactionDetailEntity.TransactionDetailId = Convert.ToInt64(expenseDataRow["TransactionDetailId"].ToString());
                    transactionDetailEntity.ExpenseCount = Convert.ToInt32(expenseDataRow["ExpenseCount"].ToString());
                    transactionDetailEntity.FGTCategoryName1 = expenseDataRow["FGTCategoryName1"].ToString();
                    transactionDetailEntity.FGTCategoryName2 = expenseDataRow["FGTCategoryName2"].ToString();
                    transactionDetailEntity.TransactionDescription = expenseDataRow["TransactionDescription"].ToString();
                    transactionDetailEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                    transactionDetailEntity.CorrectAmount = Convert.ToDecimal(expenseDataRow["CorrectAmount"].ToString());
                    transactionDetailEntity.VendorAdjustments = expenseDataRow["VendorAdjustments"].ToString();
                    transactionDetailEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                    transactionDetailEntity.ObjectDescription = expenseDataRow["ObjectDescription"].ToString();
                    transactionDetailEntity.ActivityDescription = expenseDataRow["ActivityDescription"].ToString();
                    transactionDetailEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    transactionDetailEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    transactionDetailEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    transactionDetailEntity.VendorName = expenseDataRow["VendorName"].ToString();
                    transactionDetailEntity.StatusName = expenseDataRow["StatusName"].ToString();
                    transactionDetailEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    transactionDetailEntity.RevenueTransactionAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                    transactionDetailEntity.DrawAmount = Convert.ToDecimal(expenseDataRow["DrawAmount"].ToString());
                    transactionDetailEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    transactionDetailEntity.DocumentFile = expenseDataRow["DocumentFile"].ToString();
                    transactionDetailEntity.RevenueTransactionNumber = expenseDataRow["RevenueTransactionNumber"].ToString();
                    transactionDetailEntity.RevenueProjectName = expenseDataRow["RevenueProjectName"].ToString();
                    transactionDetailEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                    transactionDetailEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                    try
                    {
                        transactionDetailEntity.DrawDate = Convert.ToDateTime(expenseDataRow["DrawDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        transactionDetailEntity.DrawDate = expenseDataRow["DrawDate"].ToString();
                    }
                    try
                    {
                        transactionDetailEntity.RevenueTransactionDate = Convert.ToDateTime(expenseDataRow["RevenueTransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        transactionDetailEntity.RevenueTransactionDate = expenseDataRow["RevenueTransactionDate"].ToString();
                    }
                    try
                    {
                        transactionDetailEntity.TransactionDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        transactionDetailEntity.TransactionDate = expenseDataRow["TransactionDate"].ToString();
                    }
                }
                catch (Exception exception)
                {
                    transactionDetailEntity.ErrorMessage = exception.Message;
                    transactionDetailEntity.Exception = exception;
                }
                finally
                {
                    transactionDetailEntities.Add(transactionDetailEntity);
                }
            }
            if (transactionDetailEntities.Count >= 1 || transactionDetailEntities.Count == 0)
            {
                expenseResponse.ErrorMessage = string.Empty;
                expenseResponse.Message = string.Empty;
            }
            expenseResponse.transactionDetailEntities = transactionDetailEntities;
            CommonDAL commonDAL = new CommonDAL();
            expenseResponse.projectEntities = commonDAL.GetProjects();
            expenseResponse.statusEntities = commonDAL.GetStatuses();
            expenseResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            FGTCategoryDAL fGTCategoryDAL = new FGTCategoryDAL();
            expenseResponse.fgtCategoryEntities2 = fGTCategoryDAL.GetFGTCategories().fGTCategoryEntities;
            return expenseResponse;
        }

        public ExpenseResponse RemoveLinkExpRev(ExpenseRequest expenseRequest)
        {
            expenseResponse.ErrorMessage = string.Empty;
            expenseResponse.Message= string.Empty;
            try
            {
                SqlObject.Parameters = new object[] {
                    expenseRequest.transactionDetailEntity.TransactionDetailId,
                    expenseRequest.transactionDetailEntity.ModifiedBy
                };
                var transactionDetailDataSet = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Expense.USPREMOVELINKEXPREV, SqlObject.Parameters);
            }
            catch (Exception ex)
            {
                expenseResponse.Exception = ex;
                expenseResponse.ErrorMessage = ex.Message;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetExpenseTransactions(ExpenseRequest expenseRequest)
        {
            List<ExpenseEntity> expenseEntities = new List<ExpenseEntity>();
            SqlObject.Parameters = new object[] {
                    expenseRequest.expenseEntity.ExpenseId,
                    expenseRequest.expenseEntity.ProjectId,
                    expenseRequest.expenseEntity.PeriodId,
                    expenseRequest.expenseEntity.OrgId,
                    expenseRequest.expenseEntity.ObjectId,
                    expenseRequest.expenseEntity.StartDate,
                    expenseRequest.expenseEntity.EndDate,
                    expenseRequest.expenseEntity.NetAmount,
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETEXPENSETRANSACTIONS, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
               ExpenseEntity expenseEntity = new ExpenseEntity();
                try
                {
                    expenseEntity.ExpenseTransactionDetailId = Convert.ToInt64(expenseDataRow["ExpenseTransactionDetailId"].ToString());
                    expenseEntity.ExpenseId = expenseRequest.expenseEntity.ExpenseId;
                    expenseEntity.Journal = expenseDataRow["Journal"].ToString();
                    expenseEntity.PeriodName = expenseDataRow["Period"].ToString();
                    expenseEntity.SourceName = expenseDataRow["Source"].ToString();
                    expenseEntity.OrgName = expenseDataRow["Org"].ToString();
                    expenseEntity.ObjectName = expenseDataRow["Object"].ToString();
                    expenseEntity.ProjectName = expenseDataRow["Project"].ToString();
                    expenseEntity.Fund = expenseDataRow["Fund"].ToString();
                    expenseEntity.Reference1 = expenseDataRow["Ref1"].ToString();
                    expenseEntity.Reference2 = expenseDataRow["Ref2"].ToString();
                    expenseEntity.Reference3 = expenseDataRow["Ref3"].ToString();
                    expenseEntity.Reference4 = expenseDataRow["Ref4"].ToString();
                    expenseEntity.NetAmount = Convert.ToDecimal(expenseDataRow["GrossAmount"].ToString());
                    expenseEntity.FiscalYear = expenseDataRow["Year"].ToString();
                    expenseEntity.Notes = expenseDataRow["Comment"].ToString();
                    expenseEntity.ExpenseDate = Convert.ToDateTime(expenseDataRow["EffectiveDate"].ToString()).ToShortDateString();
                }
                catch (Exception exception)
                {
                    expenseEntity.ErrorMessage = exception.Message;
                    expenseEntity.Exception = exception;
                }
                finally
                {
                    expenseEntities.Add(expenseEntity);
                }
            }
            if (expenseEntities.Count >= 1 || expenseEntities.Count == 0)
            {
                expenseResponse.ErrorMessage = string.Empty;
                expenseResponse.Message = string.Empty;
            }
            expenseResponse.expenseEntities = expenseEntities;
            CommonDAL commonDAL = new CommonDAL();
            expenseResponse.periodEntities = commonDAL.GetPeriods();
            expenseResponse.orgEntities = commonDAL.GetOrgs();
            expenseResponse.objectEntities = commonDAL.GetObjects();
            expenseResponse.projectEntities = commonDAL.GetProjects();
            return expenseResponse;
        }

        public ExpenseResponse SaveLinkToExpenseTransaction(ExpenseRequest expenseRequest)
        {
            try {
                expenseResponse.Message = string.Empty;
                expenseResponse.ErrorMessage = string.Empty;
                foreach (var expenseEntity in expenseRequest.expenseEntities)
                {
                    SqlObject.Parameters = new object[] {
                    expenseEntity.ExpenseId,
                    expenseEntity.ExpenseTransactionDetailId,
                    expenseEntity.CreatedBy,
                    };
                    var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Expense.USPSAVELINKTOEXPENSETRANSACTION, SqlObject.Parameters);
                }
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;
        }

        public ExpenseResponse GetExpExpTransCompare(ExpenseRequest expenseRequest)
        {
            var expenseEntities = new List<ExpenseEntity>();
            SqlObject.Parameters = new object[] {
                expenseRequest.expenseEntity.ProjectId,
                expenseRequest.expenseEntity.FiscalYearId,
                expenseRequest.expenseEntity.StatusId,
                expenseRequest.expenseEntity.Difference,
            };
            var expenseDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETEXPEXPTRANSCOMPARE, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
            {
                ExpenseEntity expenseEntity = new ExpenseEntity();
                try
                {
                    expenseEntity.ExpenseId = Convert.ToInt64(expenseDataRow["ExpenseId"].ToString());
                    expenseEntity.Journal = expenseDataRow["Journal"].ToString();
                    expenseEntity.JournalName = expenseDataRow["JournalName"].ToString();
                    expenseEntity.PeriodName = expenseDataRow["PeriodName"].ToString();
                    expenseEntity.SourceName = expenseDataRow["SourceCode"].ToString();
                    expenseEntity.SourceDescription = expenseDataRow["SourceDescription"].ToString();
                    expenseEntity.FunctionName = expenseDataRow["FunctionCode"].ToString();
                    expenseEntity.FunctionDescription = expenseDataRow["FunctionDescription"].ToString();
                    expenseEntity.DepartmentName = expenseDataRow["DepartmentCode"].ToString();
                    expenseEntity.DepartmentDescription = expenseDataRow["DepartmentDescription"].ToString();
                    expenseEntity.ActivityName = expenseDataRow["ActivityCode"].ToString();
                    expenseEntity.ActivityDescription = expenseDataRow["ActivityDescription"].ToString();
                    expenseEntity.OrgName = expenseDataRow["OrgCode"].ToString();
                    expenseEntity.OrgDescription = expenseDataRow["OrgDescription"].ToString();
                    expenseEntity.ObjectName = expenseDataRow["ObjectCode"].ToString();
                    expenseEntity.ObjectDescription = expenseDataRow["ObjectDescription"].ToString();
                    expenseEntity.ProjectName = expenseDataRow["ProjectCode"].ToString();
                    expenseEntity.ProjectDescription = expenseDataRow["ProjectDescription"].ToString();
                    expenseEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    expenseEntity.Fund = expenseDataRow["Fund"].ToString();
                    expenseEntity.Reference1 = expenseDataRow["Reference1"].ToString();
                    expenseEntity.Reference2 = expenseDataRow["Reference2"].ToString();
                    expenseEntity.Reference3 = expenseDataRow["Reference3"].ToString();
                    expenseEntity.Reference4 = expenseDataRow["Reference4"].ToString();
                    expenseEntity.NetAmount = Convert.ToDecimal(expenseDataRow["NetAmount"].ToString());
                    expenseEntity.ReconciledAmount = Convert.ToDecimal(expenseDataRow["ReconciledAmount"].ToString());
                    expenseEntity.ExpenseNumber = expenseDataRow["ExpenseNumber"].ToString();
                    expenseEntity.RevenueNumber = expenseDataRow["RevenueNumber"].ToString();
                    expenseEntity.StatusName = expenseDataRow["StatusName"].ToString();
                    expenseEntity.AssignedToUser = expenseDataRow["AssignedToUser"].ToString();
                    expenseEntity.FiscalYear = expenseDataRow["FiscalYear"].ToString();
                    expenseEntity.Notes = expenseDataRow["Notes"].ToString();
                    expenseEntity.ExpenseDate = Convert.ToDateTime(expenseDataRow["ExpenseDate"].ToString()).ToShortDateString();
                }
                catch (Exception exception)
                {
                    expenseEntity.ErrorMessage = exception.Message;
                    expenseEntity.Exception = exception;
                }
                finally
                {
                    expenseEntities.Add(expenseEntity);
                }
            }
            if (expenseEntities.Count >= 1 || expenseEntities.Count == 0)
            {
                expenseResponse.ErrorMessage = string.Empty;
                expenseResponse.Message = string.Empty;
            }
            CommonDAL commonDAL = new CommonDAL();
            expenseResponse.projectEntities = commonDAL.GetProjects();
            expenseResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            expenseResponse.statusEntities = commonDAL.GetStatuses();
            expenseResponse.expenseEntities = expenseEntities;
            return expenseResponse;
        }

        public ExpenseResponse CheckRelatedTrans(ExpenseRequest expenseRequest)
        {
            expenseResponse.ErrorMessage = String.Empty;
            expenseResponse.Message = String.Empty;
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            SqlObject.Parameters = new object[] {
                expenseRequest.transactionDetailEntity.TransactionDetailId,
                expenseRequest.transactionDetailEntity.RelatedTrans
            };
            var expenseDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPCHECKRELATEDTRANSACTION, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
            {
                try
                {
                    transactionDetailEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                }
                catch (Exception exception)
                {
                    expenseResponse.ErrorMessage = exception.Message;
                    expenseResponse.Exception = exception;
                }
            }
            expenseResponse.transactionDetailEntity = transactionDetailEntity;
            return expenseResponse;
        }

        public ExpenseResponse CheckBatchNumber(ExpenseRequest expenseRequest)
        {
            expenseResponse.ErrorMessage = String.Empty;
            expenseResponse.Message = String.Empty;
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            SqlObject.Parameters = new object[] {
                expenseRequest.transactionDetailEntity.OtherBatchNumber,
            };
            var expenseDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPCHECKOTHERBATCHNUMBER, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
            {
                try
                {
                    transactionDetailEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                }
                catch (Exception exception)
                {
                    expenseResponse.ErrorMessage = exception.Message;
                    expenseResponse.Exception = exception;
                }
            }
            expenseResponse.transactionDetailEntity = transactionDetailEntity;
            return expenseResponse;
        }

        public ExpenseResponse GetMissingExpenseTransactions(ExpenseRequest expenseRequest)
        {
            var transactionDetailEntities = new List<TransactionDetailEntity>();
            SqlObject.Parameters = new object[] {
                    expenseRequest.transactionDetailEntity.TransProject,
                    expenseRequest.transactionDetailEntity.TransactionNumber,
                    expenseRequest.transactionDetailEntity.RevenueTransactionNumber,
                    expenseRequest.transactionDetailEntity.StatusId,
                    expenseRequest.transactionDetailEntity.FGTCategoryId2,
                    expenseRequest.transactionDetailEntity.FiscalYearId,
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETMISSINGEXPENSETRANSACTIONS, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
                try
                {
                    transactionDetailEntity.TransactionDetailId = Convert.ToInt64(expenseDataRow["TransactionDetailId"].ToString());
                    transactionDetailEntity.ExpenseCount = Convert.ToInt32(expenseDataRow["ExpenseCount"].ToString());
                    transactionDetailEntity.FGTCategoryName1 = expenseDataRow["FGTCategoryName1"].ToString();
                    transactionDetailEntity.FGTCategoryName2 = expenseDataRow["FGTCategoryName2"].ToString();
                    transactionDetailEntity.TransactionDescription = expenseDataRow["TransactionDescription"].ToString();
                    transactionDetailEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                    transactionDetailEntity.CorrectAmount = Convert.ToDecimal(expenseDataRow["CorrectAmount"].ToString());
                    transactionDetailEntity.VendorAdjustments = expenseDataRow["VendorAdjustments"].ToString();
                    transactionDetailEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                    transactionDetailEntity.TransOrg = expenseDataRow["OrgName"].ToString();
                    transactionDetailEntity.TransObject = expenseDataRow["ObjectName"].ToString();
                    transactionDetailEntity.TransProject = expenseDataRow["ProjectName"].ToString();
                    transactionDetailEntity.VendorName = expenseDataRow["VendorName"].ToString();
                    transactionDetailEntity.StatusName = expenseDataRow["StatusName"].ToString();
                    transactionDetailEntity.RevenueTransactionAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                    transactionDetailEntity.DrawAmount = Convert.ToDecimal(expenseDataRow["DrawAmount"].ToString());
                    transactionDetailEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    transactionDetailEntity.DocumentFile = expenseDataRow["DocumentFile"].ToString();
                    transactionDetailEntity.RevenueTransactionNumber = expenseDataRow["RevenueTransactionNumber"].ToString();
                    transactionDetailEntity.RevenueProjectName = expenseDataRow["RevenueProjectName"].ToString();
                    transactionDetailEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                    transactionDetailEntity.FiscalYear = expenseDataRow["FiscalYear"].ToString();
                    transactionDetailEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                    try
                    {
                        transactionDetailEntity.DrawDate = Convert.ToDateTime(expenseDataRow["DrawDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        transactionDetailEntity.DrawDate = expenseDataRow["DrawDate"].ToString();
                    }
                    try
                    {
                        transactionDetailEntity.RevenueTransactionDate = Convert.ToDateTime(expenseDataRow["RevenueTransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        transactionDetailEntity.RevenueTransactionDate = expenseDataRow["RevenueTransactionDate"].ToString();
                    }
                    transactionDetailEntity.TransactionDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                }
                catch (Exception exception)
                {
                    transactionDetailEntity.ErrorMessage = exception.Message;
                    transactionDetailEntity.Exception = exception;
                    transactionDetailEntity.TransactionDate = expenseDataRow["TransactionDate"].ToString();
                }
                finally
                {
                    transactionDetailEntities.Add(transactionDetailEntity);
                }
            }
            if (transactionDetailEntities.Count >= 1 || transactionDetailEntities.Count == 0)
            {
                expenseResponse.ErrorMessage = string.Empty;
                expenseResponse.Message = string.Empty;
            }
            expenseResponse.transactionDetailEntities = transactionDetailEntities;
            CommonDAL commonDAL = new CommonDAL();
            expenseResponse.projectEntities = commonDAL.GetProjects();
            expenseResponse.statusEntities = commonDAL.GetStatuses();
            expenseResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            FGTCategoryDAL fGTCategoryDAL = new FGTCategoryDAL();
            expenseResponse.fgtCategoryEntities2 = fGTCategoryDAL.GetFGTCategories().fGTCategoryEntities;
            return expenseResponse;
        }

        public ExpenseResponse GetMissingExpenseTransaction(ExpenseRequest expenseRequest)
        {
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ErrorMessage = string.Empty;
            expenseResponse.ErrorMessage = string.Empty;
            expenseEntity.Message = string.Empty;
            expenseResponse.Message = string.Empty;
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            if (expenseRequest.transactionDetailEntity.TransactionDetailId > 0)
            {
                SqlObject.Parameters = new object[] {
                    expenseRequest.transactionDetailEntity.TransactionDetailId
                };
                var expenseDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETMISSINGEXPENSETRANSACTION, SqlObject.Parameters);
                foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
                {
                    try
                    {
                        transactionDetailEntity.TransactionDetailId = Convert.ToInt64(expenseDataRow["TransactionDetailId"].ToString());
                        transactionDetailEntity.DrawId = Convert.ToInt64(expenseDataRow["DrawId"].ToString());
                        transactionDetailEntity.VendorId = Convert.ToInt64(expenseDataRow["VendorId"].ToString());
                        transactionDetailEntity.FiscalYearId = Convert.ToInt64(expenseDataRow["FiscalYearId"].ToString());
                        transactionDetailEntity.StatusId = Convert.ToInt32(expenseDataRow["StatusId"].ToString());
                        transactionDetailEntity.FGTCategoryId1 = Convert.ToInt64(expenseDataRow["FGTCategoryId1"].ToString());
                        transactionDetailEntity.FGTCategoryId2 = Convert.ToInt64(expenseDataRow["FGTCategoryId2"].ToString());
                        transactionDetailEntity.FGTCategoryName1 = expenseDataRow["FGTCategoryName1"].ToString();
                        transactionDetailEntity.FGTCategoryName2 = expenseDataRow["FGTCategoryName2"].ToString();
                        transactionDetailEntity.TransactionDescription = expenseDataRow["TransactionDescription"].ToString();
                        transactionDetailEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                        transactionDetailEntity.CorrectAmount = Convert.ToDecimal(expenseDataRow["CorrectAmount"].ToString());
                        transactionDetailEntity.VendorAdjustments = expenseDataRow["VendorAdjustments"].ToString();
                        transactionDetailEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                        transactionDetailEntity.DrawNumber = expenseDataRow["DrawNumber"].ToString();
                        transactionDetailEntity.VendorName = expenseDataRow["VendorName"].ToString();
                        transactionDetailEntity.StatusName = expenseDataRow["StatusName"].ToString();
                        transactionDetailEntity.FiscalYear = expenseDataRow["FiscalYear"].ToString();
                        try
                        {
                            transactionDetailEntity.RevenueTransactionAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                        }
                        catch (Exception)
                        {
                            transactionDetailEntity.RevenueTransactionAmount = 0;
                        }
                        transactionDetailEntity.RevenueTransactionNumber = expenseDataRow["RevenueTransactionNumber"].ToString();
                        transactionDetailEntity.RevenueProjectName = expenseDataRow["RevenueProjectName"].ToString();
                        try
                        {
                            transactionDetailEntity.DrawAmount = Convert.ToDecimal(expenseDataRow["DrawAmount"].ToString());
                        }
                        catch (Exception)
                        {
                            transactionDetailEntity.DrawAmount = 0;
                        }                        
                        transactionDetailEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                        transactionDetailEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                        transactionDetailEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                        transactionDetailEntity.TransOrg = expenseDataRow["TransOrg"].ToString();
                        transactionDetailEntity.TransObject = expenseDataRow["TransObject"].ToString();
                        transactionDetailEntity.TransProject = expenseDataRow["TransProject"].ToString();
                        transactionDetailEntity.TransSource = expenseDataRow["TransSource"].ToString();
                        try
                        {
                            transactionDetailEntity.DrawDate = Convert.ToDateTime(expenseDataRow["DrawDate"].ToString()).ToShortDateString();
                        }
                        catch (Exception)
                        {
                            transactionDetailEntity.DrawDate = expenseDataRow["DrawDate"].ToString();
                        }
                        try
                        {
                            transactionDetailEntity.RevenueTransactionDate = Convert.ToDateTime(expenseDataRow["RevenueTransactionDate"].ToString()).ToShortDateString();
                        }
                        catch (Exception)
                        {
                            transactionDetailEntity.RevenueTransactionDate = expenseDataRow["RevenueTransactionDate"].ToString();
                        }
                        transactionDetailEntity.TransactionDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception exception)
                    {
                        transactionDetailEntity.ErrorMessage = exception.Message;
                        transactionDetailEntity.Exception = exception;
                        transactionDetailEntity.TransactionDate = expenseDataRow["TransactionDate"].ToString();
                    }
                }
            }
           
            expenseResponse.transactionDetailEntity = transactionDetailEntity;

            FGTCategoryEntity fGTCategoryEntity = new FGTCategoryEntity();
            FGTCategoryRequest fGTCategoryRequest = new FGTCategoryRequest();
            FGTCategoryDAL fGTCategoryDAL = new FGTCategoryDAL();
            FGTCategoryResponse fGTCategoryResponse;
            fGTCategoryEntity.FGTParentCategoryId = 0;
            fGTCategoryRequest.fGTCategoryEntity = fGTCategoryEntity;
            fGTCategoryResponse = fGTCategoryDAL.GetFGTCategoriesById(fGTCategoryRequest);
            expenseResponse.fgtCategoryEntities = fGTCategoryResponse.fGTCategoryEntities;

            fGTCategoryEntity.FGTParentCategoryId = transactionDetailEntity.FGTCategoryId1;
            fGTCategoryRequest.fGTCategoryEntity = fGTCategoryEntity;
            fGTCategoryResponse = fGTCategoryDAL.GetFGTCategoriesById(fGTCategoryRequest);
            expenseResponse.fgtCategoryEntities2 = fGTCategoryResponse.fGTCategoryEntities;

            expenseResponse.transactionDetailEntities = GetTransactionDetails(transactionDetailEntity.ExpenseId);
            VendorDAL vendorDAL = new VendorDAL();
            VendorRequest vendorRequest = new VendorRequest();
            VendorEntity vendorEntity = new VendorEntity();
            vendorEntity.VendorName = string.Empty;
            vendorRequest.vendorEntity = vendorEntity;
            expenseResponse.vendorEntities = vendorDAL.GetVendors(vendorRequest).vendorEntities;

            CommonDAL commonDAL = new CommonDAL();
            expenseResponse.statusEntities = commonDAL.GetStatuses();
            expenseResponse.fiscalYearEntities = commonDAL.GetFiscalYears();

            return expenseResponse;
        }

        public ExpenseResponse SaveMissingExpenseTransaction(ExpenseRequest expenseRequest)
        {
            try
            {
                if (expenseRequest.transactionDetailEntity.TransactionDetailId > 0)
                    expenseRequest.transactionDetailEntity.SaveString = "U";
                else
                    expenseRequest.transactionDetailEntity.SaveString = "I";
                expenseRequest.transactionDetailEntity.IsMissingExpense = true;
                SqlObject.Parameters = new object[] {
                expenseRequest.transactionDetailEntity.SaveString,
                expenseRequest.transactionDetailEntity.TransactionDetailId,
                expenseRequest.transactionDetailEntity.TransactionDate,
                expenseRequest.transactionDetailEntity.FGTCategoryId1,
                expenseRequest.transactionDetailEntity.FGTCategoryId2,
                expenseRequest.transactionDetailEntity.TransactionDescription,
                expenseRequest.transactionDetailEntity.ModifiedBy,
                expenseRequest.transactionDetailEntity.TransactionAmount,
                expenseRequest.transactionDetailEntity.VendorAdjustments,
                expenseRequest.transactionDetailEntity.DrawId,
                expenseRequest.transactionDetailEntity.VendorId,
                expenseRequest.transactionDetailEntity.StatusId,
                expenseRequest.transactionDetailEntity.RelatedTrans,
                expenseRequest.transactionDetailEntity.CorrectAmount,
                expenseRequest.transactionDetailEntity.OtherBatchNumber,
                expenseRequest.transactionDetailEntity.TransOrg,
                expenseRequest.transactionDetailEntity.TransObject,
                expenseRequest.transactionDetailEntity.TransProject,
                expenseRequest.transactionDetailEntity.TransSource,
                expenseRequest.transactionDetailEntity.FiscalYearId,
                expenseRequest.transactionDetailEntity.ExpenseId,
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Expense.USPSAVEMISSINGEXPENSETRANSACTION, SqlObject.Parameters);
                expenseResponse.Message = string.Empty;
                expenseResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                expenseResponse.ErrorMessage = ex.Message;
                expenseResponse.Exception = ex;
            }
            return expenseResponse;

        }

        public ExpenseResponse GetMissingRevenueTransaction(ExpenseRequest expenseRequest)
        {
            List<RevenueTransactionEntity> revenueTransactionEntities = new List<RevenueTransactionEntity>();
            SqlObject.Parameters = new object[] {
                expenseRequest.transactionDetailEntity.FiscalYearId,
                expenseRequest.transactionDetailEntity.TransProject
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPLINKMISSINGREVENUETRANSACTION, SqlObject.Parameters);
            foreach (DataRow revenueDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
                try
                {
                    revenueTransactionEntity.RevenueTransactionId = Convert.ToInt64(revenueDataRow["RevenueTransactionId"].ToString());
                    revenueTransactionEntity.RevenueTypeName = revenueDataRow["RevenueTypeName"].ToString();
                    revenueTransactionEntity.RevenueTranscationDescription = revenueDataRow["RevenueTranscationDescription"].ToString();
                    revenueTransactionEntity.RevenueTransactionAmount = Convert.ToDecimal(revenueDataRow["RevenueTransactionAmount"].ToString());
                    revenueTransactionEntity.RevenueTransactionNumber = revenueDataRow["RevenueTransactionNumber"].ToString();
                    revenueTransactionEntity.OrgName = revenueDataRow["OrgName"].ToString();
                    revenueTransactionEntity.ObjectName = revenueDataRow["ObjectName"].ToString();
                    revenueTransactionEntity.ProjectName = revenueDataRow["ProjectName"].ToString();
                    revenueTransactionEntity.DrawNumber = revenueDataRow["DrawNumber"].ToString();
                    revenueTransactionEntity.DrawAmount = Convert.ToDecimal(revenueDataRow["DarwDownAmount"].ToString());
                    revenueTransactionEntity.BatchNumber = revenueDataRow["BatchNumber"].ToString();
                    try
                    {
                        revenueTransactionEntity.RevenueTransactionDate = Convert.ToDateTime(revenueDataRow["RevenueTransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        revenueTransactionEntity.RevenueTransactionDate = revenueDataRow["RevenueTransactionDate"].ToString();
                    }
                    
                    try
                    {
                        revenueTransactionEntity.DrawDate = Convert.ToDateTime(revenueDataRow["DrawDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        revenueTransactionEntity.DrawDate = revenueDataRow["DrawDate"].ToString();
                    }                    
                }
                catch (Exception exception)
                {
                    revenueTransactionEntity.ErrorMessage = exception.Message;
                    revenueTransactionEntity.Exception = exception;
                }
                finally
                {
                    revenueTransactionEntities.Add(revenueTransactionEntity);
                }
            }
            if (revenueTransactionEntities.Count >= 1 || revenueTransactionEntities.Count == 0)
            {
                expenseResponse.ErrorMessage = string.Empty;
                expenseResponse.Message = string.Empty;
            }
            expenseResponse.revenueTransactionEntities = revenueTransactionEntities;
            //CommonDAL commonDAL = new CommonDAL();
            //expenseResponse.projectEntities = commonDAL.GetProjects();
            return expenseResponse;
        }

        public ExpenseResponse GetMissingExpenses(ExpenseRequest expenseRequest)
        {
            List<ExpenseEntity> expenseEntities = new List<ExpenseEntity>();
            SqlObject.Parameters = new object[] {
                expenseRequest.expenseEntity.FiscalYearId,
                expenseRequest.expenseEntity.ProjectName
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETMISSINGEXPENSES, SqlObject.Parameters);
            foreach (DataRow revenueDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                ExpenseEntity expenseEntity = new ExpenseEntity();
                try
                {
                    expenseEntity.ExpenseId = Convert.ToInt64(revenueDataRow["ExpenseId"].ToString());
                    expenseEntity.ExpenseNumber = revenueDataRow["ExpenseNumber"].ToString();
                }
                catch (Exception exception)
                {
                    expenseEntity.ErrorMessage = exception.Message;
                    expenseEntity.Exception = exception;
                }
                finally
                {
                    expenseEntities.Add(expenseEntity);
                }
            }
            if (expenseEntities.Count >= 1 || expenseEntities.Count == 0)
            {
                expenseResponse.ErrorMessage = string.Empty;
                expenseResponse.Message = string.Empty;
            }
            expenseResponse.expenseEntities = expenseEntities;
            return expenseResponse;
        }


        public ExpenseResponse GetPriorYearExpenseTransactions(ExpenseRequest expenseRequest)
        {
            var transactionDetailEntities = new List<TransactionDetailEntity>();
            SqlObject.Parameters = new object[] {
                    expenseRequest.transactionDetailEntity.ProjectId,
                    expenseRequest.transactionDetailEntity.TransactionNumber,
                    expenseRequest.transactionDetailEntity.RevenueTransactionNumber,
                    expenseRequest.transactionDetailEntity.StatusId,
                    expenseRequest.transactionDetailEntity.FGTCategoryId2,
                    expenseRequest.transactionDetailEntity.FiscalYearId,
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETPRIORYEAREXPENSETRANSACTIONS, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
                try
                {
                    transactionDetailEntity.ExpenseNumber = expenseDataRow["ExpenseNumber"].ToString();
                    transactionDetailEntity.TransactionDetailId = Convert.ToInt64(expenseDataRow["ExpenseTransactionPriorYearId"].ToString());
                    transactionDetailEntity.ExpenseCount = Convert.ToInt32(expenseDataRow["ExpenseCount"].ToString());
                    transactionDetailEntity.FGTCategoryName1 = expenseDataRow["FGTCategoryName1"].ToString();
                    transactionDetailEntity.FGTCategoryName2 = expenseDataRow["FGTCategoryName2"].ToString();
                    transactionDetailEntity.TransactionDescription = expenseDataRow["TransactionDescription"].ToString();
                    transactionDetailEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                    transactionDetailEntity.CorrectAmount = Convert.ToDecimal(expenseDataRow["CorrectAmount"].ToString());
                    transactionDetailEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                    transactionDetailEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    transactionDetailEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    transactionDetailEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    transactionDetailEntity.VendorName = expenseDataRow["VendorName"].ToString();
                    transactionDetailEntity.StatusName = expenseDataRow["StatusName"].ToString();
                    transactionDetailEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    transactionDetailEntity.RevenueTransactionAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                    transactionDetailEntity.DrawAmount = Convert.ToDecimal(expenseDataRow["DrawAmount"].ToString());
                    transactionDetailEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    transactionDetailEntity.DocumentFile = expenseDataRow["DocumentFile"].ToString();
                    transactionDetailEntity.RevenueTransactionNumber = expenseDataRow["RevenueTransactionNumber"].ToString();
                    transactionDetailEntity.RevenueProjectName = expenseDataRow["RevenueProjectName"].ToString();
                    transactionDetailEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                    transactionDetailEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                    try
                    {
                        transactionDetailEntity.DrawDate = Convert.ToDateTime(expenseDataRow["DrawDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        transactionDetailEntity.DrawDate = expenseDataRow["DrawDate"].ToString();
                    }
                    try
                    {
                        transactionDetailEntity.RevenueTransactionDate = Convert.ToDateTime(expenseDataRow["RevenueTransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        transactionDetailEntity.RevenueTransactionDate = expenseDataRow["RevenueTransactionDate"].ToString();
                    }
                    try
                    {
                        transactionDetailEntity.TransactionDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        transactionDetailEntity.TransactionDate = expenseDataRow["TransactionDate"].ToString();
                    }
                }
                catch (Exception exception)
                {
                    transactionDetailEntity.ErrorMessage = exception.Message;
                    transactionDetailEntity.Exception = exception;
                }
                finally
                {
                    transactionDetailEntities.Add(transactionDetailEntity);
                }
            }
            if (transactionDetailEntities.Count >= 1 || transactionDetailEntities.Count == 0)
            {
                expenseResponse.ErrorMessage = string.Empty;
                expenseResponse.Message = string.Empty;
            }
            expenseResponse.transactionDetailEntities = transactionDetailEntities;
            CommonDAL commonDAL = new CommonDAL();
            expenseResponse.projectEntities = commonDAL.GetProjects();
            expenseResponse.statusEntities = commonDAL.GetStatuses();
            expenseResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            FGTCategoryDAL fGTCategoryDAL = new FGTCategoryDAL();
            expenseResponse.fgtCategoryEntities2 = fGTCategoryDAL.GetFGTCategories().fGTCategoryEntities;
            return expenseResponse;
        }
    }
}
