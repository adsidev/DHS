using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class ProjectDAL : IProjectDALRepository
    {

        private readonly string _connectionString;
        ProjectResponse projectResponse;
        public ProjectDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            projectResponse = new ProjectResponse();
            projectResponse.ErrorMessage = string.Empty;
            projectResponse.Message = string.Empty;
        }

        public ProjectResponse GetProjects()
        {
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Common.USPGETPROJECTS);
            List<ProjectEntity> projectEntities = new List<ProjectEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                ProjectEntity projectEntity = new ProjectEntity();
                try
                {
                    projectEntity.ProjectId = Convert.ToInt32(expenseDataRow["ProjectId"]);
                    projectEntity.ProjectName = expenseDataRow["ProjectCode"].ToString();
                    projectEntity.ProjectDescription = expenseDataRow["ProjectDescription"].ToString();
                    projectEntity.CFDA = expenseDataRow["CFDA"].ToString();
                }
                catch (Exception exception)
                {
                    projectResponse.ErrorMessage = exception.Message;
                    projectResponse.Message = exception.Message;
                    projectResponse.Exception = exception;
                }
                finally
                {
                    projectEntities.Add(projectEntity);
                }
            }
            projectResponse.projectEntities = projectEntities;
            return projectResponse;
        }

        public ProjectResponse GetProjectResponse(ProjectRequest projectRequest)
        {
            List<ProjectEntity> projectEntities = new List<ProjectEntity>();
            if (projectRequest.projectEntity.FiscalYearId>0)
            {
                SqlObject.Parameters = new object[] {
                projectRequest.projectEntity.FiscalYearId
            };
                var expenseDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Project.USPGETPROJECTSUMMARY, SqlObject.Parameters);
                foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
                {
                    ProjectEntity projectEntity = new ProjectEntity();
                    try
                    {
                        projectEntity.ProjectId = Convert.ToInt64(expenseDataRow["ProjectId"].ToString());
                        projectEntity.ProjectName = expenseDataRow["ProjectCode"].ToString();
                        projectEntity.ExpenseAmount = Convert.ToDecimal(expenseDataRow["ExpenseTotal"].ToString());
                        projectEntity.RevenueAmount = Convert.ToDecimal(expenseDataRow["RevenueTotal"].ToString());
                        projectEntity.ExpenseTransactionAmount = Convert.ToDecimal(expenseDataRow["ExpenseTransactionTotal"].ToString());
                        projectEntity.RevenueTransactionAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionTotal"].ToString());
                    }
                    catch (Exception exception)
                    {
                        projectResponse.ErrorMessage = exception.Message;
                        projectResponse.Exception = exception;
                    }
                    finally
                    {
                        projectEntities.Add(projectEntity);
                    }
                }
            }
            projectResponse.projectEntities = projectEntities;
            CommonDAL commonDAL = new CommonDAL();
            projectResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            return projectResponse;
        }

        public ProjectResponse GetProjectDetails(ProjectRequest projectRequest)
        {
            try
            {
                projectResponse.expenseEntities = GetExpenseEntities(projectRequest.projectEntity.ProjectId);
                projectResponse.revenueEntities = GetRevenueEntities(projectRequest.projectEntity.ProjectId);
                projectResponse.transactionDetailEntities = GetTransactionDetailEntities(projectRequest.projectEntity.ProjectId);
                projectResponse.revenueTransactionEntities = GetRevenueTransactionEntities(projectRequest.projectEntity.ProjectId);
            }
            catch (Exception ex)
            {
                projectResponse.ErrorMessage = ex.Message;
                projectResponse.Message = ex.Message;
            }
            
            return projectResponse;
        }

        private List<ExpenseEntity> GetExpenseEntities(long projectId)
        {
            List<ExpenseEntity> expenseEntities = new List<ExpenseEntity>();
            SqlObject.Parameters = new object[] {
                projectId
            };
            var expenseDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Project.USPGETEXPENSESBYPROJECTID, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
            {
                ExpenseEntity expenseEntity = new ExpenseEntity();
                try
                {
                    expenseEntity.ExpenseId = Convert.ToInt64(expenseDataRow["ExpenseId"].ToString());
                    expenseEntity.RevenueId = Convert.ToInt64(expenseDataRow["RevenueId"].ToString());
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
                    expenseEntity.Notes = expenseDataRow["Notes"].ToString();
                    expenseEntity.Reference1 = expenseDataRow["Reference1"].ToString();
                    expenseEntity.Reference2 = expenseDataRow["Reference2"].ToString();
                    expenseEntity.Reference3 = expenseDataRow["Reference3"].ToString();
                    expenseEntity.Reference4 = expenseDataRow["Reference4"].ToString();
                    expenseEntity.NetAmount = Convert.ToDecimal(expenseDataRow["NetAmount"].ToString());
                    expenseEntity.ExpenseNumber = expenseDataRow["ExpenseNumber"].ToString();
                    expenseEntity.RevenueNumber = expenseDataRow["RevenueNumber"].ToString();
                    expenseEntity.StatusName = expenseDataRow["StatusName"].ToString();
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
            return expenseEntities;
        }

        private List<RevenueEntity> GetRevenueEntities(long projectId)
        {
            List<RevenueEntity> revenueEntities = new List<RevenueEntity>();
            SqlObject.Parameters = new object[] {
                projectId
            };
            var revenueDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Project.USPGETREVENUESBYPROJECTID, SqlObject.Parameters);
            foreach (DataRow revenueDataRow in revenueDataSet.Tables[0].Rows)
            {
                RevenueEntity revenueEntity = new RevenueEntity();
                try
                {
                    revenueEntity.RevenueId = Convert.ToInt64(revenueDataRow["RevenueId"].ToString());
                    revenueEntity.DrawId = Convert.ToInt64(revenueDataRow["DrawId"].ToString());
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
                    revenueEntity.DrawNumber = revenueDataRow["DrawNumber"].ToString();
                    revenueEntity.StatusName = revenueDataRow["StatusName"].ToString();
                    revenueEntity.RevenueTypeName = revenueDataRow["RevenueTypeName"].ToString();
                    revenueEntity.CFDA = revenueDataRow["CFDA"].ToString();
                    revenueEntity.Fund = revenueDataRow["Fund"].ToString();
                    revenueEntity.Notes = revenueDataRow["Notes"].ToString();
                    revenueEntity.Reference1 = revenueDataRow["Reference1"].ToString();
                    revenueEntity.Reference2 = revenueDataRow["Reference2"].ToString();
                    revenueEntity.Reference3 = revenueDataRow["Reference3"].ToString();
                    revenueEntity.Reference4 = revenueDataRow["Reference4"].ToString();
                    revenueEntity.NetAmount = Convert.ToDecimal(revenueDataRow["NetAmount"].ToString());
                    revenueEntity.RevenueAmount = Convert.ToDecimal(revenueDataRow["RevenueAmount"].ToString());
                    revenueEntity.FiscalYear = revenueDataRow["FiscalYear"].ToString();
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
            return revenueEntities;
        }

        private List<TransactionDetailEntity> GetTransactionDetailEntities(long projectId)
        {
            List<TransactionDetailEntity> transactionDetailEntities = new List<TransactionDetailEntity>();
            SqlObject.Parameters = new object[] {
                projectId
            };
            var transactionDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Project.USPGETTRANSACTIONSBYPROJECTID, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDataSet.Tables[0].Rows)
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
                    transactionDetailEntity.VendorAdjustments = expenseDataRow["VendorAdjustments"].ToString();
                    transactionDetailEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                    transactionDetailEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    transactionDetailEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    transactionDetailEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    transactionDetailEntity.DrawNumber = expenseDataRow["DrawNumber"].ToString();
                    transactionDetailEntity.VendorName = expenseDataRow["VendorName"].ToString();
                    transactionDetailEntity.StatusName = expenseDataRow["StatusName"].ToString();
                    transactionDetailEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    transactionDetailEntity.RevenueTransactionAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                    transactionDetailEntity.DrawAmount = Convert.ToDecimal(expenseDataRow["DrawAmount"].ToString());
                    transactionDetailEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    transactionDetailEntity.DrawDate = Convert.ToDateTime(expenseDataRow["DrawDate"].ToString()).ToShortDateString();
                    transactionDetailEntity.RevenueTransactionNumber = expenseDataRow["RevenueTransactionNumber"].ToString();
                    transactionDetailEntity.RevenueProjectName = expenseDataRow["RevenueProjectName"].ToString();
                    transactionDetailEntity.RevenueTransactionDate = Convert.ToDateTime(expenseDataRow["RevenueTransactionDate"].ToString()).ToShortDateString();
                    transactionDetailEntity.TransactionDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                }
                catch (Exception exception)
                {
                    transactionDetailEntity.RevenueTransactionDate = expenseDataRow["RevenueTransactionDate"].ToString();
                    transactionDetailEntity.TransactionDate = expenseDataRow["TransactionDate"].ToString();
                }
                finally
                {
                    transactionDetailEntities.Add(transactionDetailEntity);
                }
            }
            return transactionDetailEntities;
        }

        private List<RevenueTransactionEntity> GetRevenueTransactionEntities(long projectId)
        {
            List<RevenueTransactionEntity> revenueTransactionEntities = new List<RevenueTransactionEntity>();
            SqlObject.Parameters = new object[] {
                projectId
            };
            var transactionDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Project.USPGETREVENUETRANSACTIONSBYPROJECTID, SqlObject.Parameters);
            foreach (DataRow revenueDataRow in transactionDataSet.Tables[0].Rows)
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
                    revenueTransactionEntity.BatchNumber = revenueDataRow["BatchNumber"].ToString();
                    revenueTransactionEntity.ProjectName = revenueDataRow["ProjectName"].ToString();
                    revenueTransactionEntity.CompleteCount = Convert.ToInt32(revenueDataRow["CompleteCount"].ToString());
                    revenueTransactionEntity.ExpenseCount = Convert.ToInt32(revenueDataRow["ExpenseCount"].ToString());
                    revenueTransactionEntity.DrawNumber = revenueDataRow["DrawNumber"].ToString();
                    revenueTransactionEntity.RevenueTransactionDate = Convert.ToDateTime(revenueDataRow["RevenueTransactionDate"].ToString()).ToShortDateString();
                }
                catch (Exception exception)
                {
                    revenueTransactionEntity.ErrorMessage = exception.Message;
                    revenueTransactionEntity.Exception = exception;
                    revenueTransactionEntity.RevenueTransactionDate = revenueDataRow["RevenueTransactionDate"].ToString();
                }
                finally
                {
                    revenueTransactionEntities.Add(revenueTransactionEntity);
                }
            }
            return revenueTransactionEntities;
        }
    }
}
