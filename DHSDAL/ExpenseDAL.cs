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
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ErrorMessage = string.Empty;
            expenseResponse.ErrorMessage = string.Empty;
            expenseEntity.Message = string.Empty;
            expenseResponse.Message = string.Empty;
            if (expenseRequest.expenseEntity.ExpenseId>0)
            {
                SqlObject.Parameters = new object[] {
                    expenseRequest.expenseEntity.ExpenseId
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
            
            expenseResponse.expenseEntity = expenseEntity;
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
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ErrorMessage = string.Empty;
            expenseResponse.ErrorMessage = string.Empty;
            expenseEntity.Message = string.Empty;
            expenseResponse.Message = string.Empty;
            expenseResponse.expenseEntity = GetExpense(expenseRequest).expenseEntity;
            DocumentDAL documentDAL = new DocumentDAL();
            DocumentEntity documentEntity = new DocumentEntity();
            documentEntity.DocumentReferenceId = expenseRequest.expenseEntity.ExpenseId;
            documentEntity.DocumentTypeId = 1;
            expenseResponse.documentEntities = documentDAL.GetDocuments(documentEntity);
            var transactionDetailEntities = new List<TransactionDetailEntity>();
            SqlObject.Parameters = new object[] {
                    expenseRequest.expenseEntity.ExpenseId
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Expense.USPGETTRANSACTIONDETAILS, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
                try
                {
                    transactionDetailEntity.TransactionDetailId = Convert.ToInt64(expenseDataRow["TransactionDetailId"].ToString());
                    transactionDetailEntity.TransactionDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    transactionDetailEntity.FGTCategoryName1 = expenseDataRow["FGTCategoryName1"].ToString();
                    transactionDetailEntity.FGTCategoryName2 = expenseDataRow["FGTCategoryName2"].ToString();
                    transactionDetailEntity.TransactionDescription = expenseDataRow["TransactionDescription"].ToString();
                    transactionDetailEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                    transactionDetailEntity.VendorAdjustments = expenseDataRow["VendorAdjustments"].ToString();
                    transactionDetailEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                    transactionDetailEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    transactionDetailEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    transactionDetailEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    transactionDetailEntity.DrawNumber = expenseDataRow["DrawNumber"].ToString();
                    transactionDetailEntity.CFDA = expenseDataRow["CFDA"].ToString();
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
            return expenseResponse;
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
                        transactionDetailEntity.FGTCategoryId1 = Convert.ToInt64(expenseDataRow["FGTCategoryId1"].ToString());
                        transactionDetailEntity.FGTCategoryId2 = Convert.ToInt64(expenseDataRow["FGTCategoryId2"].ToString());
                        transactionDetailEntity.TransactionDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                        transactionDetailEntity.FGTCategoryName1 = expenseDataRow["FGTCategoryName1"].ToString();
                        transactionDetailEntity.FGTCategoryName2 = expenseDataRow["FGTCategoryName2"].ToString();
                        transactionDetailEntity.TransactionDescription = expenseDataRow["TransactionDescription"].ToString();
                        transactionDetailEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                        transactionDetailEntity.VendorAdjustments = expenseDataRow["VendorAdjustments"].ToString();
                        transactionDetailEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                        transactionDetailEntity.DrawNumber = expenseDataRow["DrawNumber"].ToString();
                    }
                    catch (Exception exception)
                    {
                        transactionDetailEntity.ErrorMessage = exception.Message;
                        transactionDetailEntity.Exception = exception;
                    }
                    finally
                    {
                       
                    }
                }
            }
            else
            {
                transactionDetailEntity.ExpenseId = expenseRequest.transactionDetailEntity.ExpenseId;
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

            
            expenseResponse.drawEntities = GetDrawsByExpenseId(expenseEntity.ExpenseId);
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
            expenseResponse.expenseEntity = GetExpense(expenseRequest).expenseEntity;
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
            expenseResponse.expenseEntity = GetExpense(expenseRequest).expenseEntity;
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
            expenseResponse.expenseEntity = GetExpense(expenseRequest).expenseEntity;
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
    }
}
