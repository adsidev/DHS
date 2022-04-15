using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class RevenueDAL : IRevenueDALRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// FiscalPeriod Response <see cref="CostReportInvoiceResponse"/>
        /// </summary>
        RevenueResponse revenueResponse;

        public RevenueDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            revenueResponse = new RevenueResponse();
        }

        public RevenueResponse GetRevenues(RevenueRequest revenueRequest)
        {
            var revenueEntities = new List<RevenueEntity>();
            SqlObject.Parameters = new object[] {
                revenueRequest.revenueEntity.AssignedTo,
                revenueRequest.revenueEntity.StatusId,
                revenueRequest.revenueEntity.ProjectId,
                revenueRequest.revenueEntity.FiscalYearId,
                revenueRequest.revenueEntity.PeriodId,
                revenueRequest.revenueEntity.SourceId
            };
            var revenueDataSet= SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPGETREVENUES, SqlObject.Parameters);
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
                    revenueEntity.AssignedToUser = revenueDataRow["AssignedToUser"].ToString();
                    revenueEntity.FiscalYear = revenueDataRow["FiscalYear"].ToString();
                    revenueEntity.RevenueTypeName = revenueDataRow["RevenueTypeName"].ToString();
                    revenueEntity.CFDA = revenueDataRow["CFDA"].ToString();
                    revenueEntity.Fund = revenueDataRow["Fund"].ToString();
                    revenueEntity.Reference1 = revenueDataRow["Reference1"].ToString();
                    revenueEntity.Reference2 = revenueDataRow["Reference2"].ToString();
                    revenueEntity.Reference3 = revenueDataRow["Reference3"].ToString();
                    revenueEntity.Reference4 = revenueDataRow["Reference4"].ToString();
                    revenueEntity.Notes = revenueDataRow["Notes"].ToString();
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
            if (revenueEntities.Count >= 1 || revenueEntities.Count==0)
            {
                revenueResponse.ErrorMessage = string.Empty;
                revenueResponse.Message = string.Empty;
            }
            revenueResponse.revenueEntities = revenueEntities;
           RevenueEntity revenueEntity1 = new RevenueEntity();
            revenueEntity1.RevenueId = 0;
            revenueEntity1.AssignedTo = 0;
            revenueEntity1.StatusId = 0;
            revenueRequest.revenueEntity = revenueEntity1;
            revenueResponse.drawEntities = DrawRevenues(revenueRequest).drawEntities;
            CommonDAL commonDAL = new CommonDAL();
            revenueResponse.statusEntities = commonDAL.GetStatuses();
            revenueResponse.projectEntities = commonDAL.GetProjects();
            revenueResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            revenueResponse.periodEntities = commonDAL.GetPeriods();
            revenueResponse.sourceEntities = commonDAL.GetSources();
            UserDAL userDAL = new UserDAL();
            revenueResponse.userEntities = userDAL.GetAssignedTo().userEntities;

            return revenueResponse;
        }

        public RevenueResponse GetRevenue(RevenueRequest revenueRequest)
        {
            revenueResponse.revenueEntity = GetRevenueInfo(revenueRequest).revenueEntity;
            CommonDAL commonDAL = new CommonDAL();
            revenueResponse.periodEntities = commonDAL.GetPeriods();
            revenueResponse.activityEntities = commonDAL.GetActivities();
            revenueResponse.sourceEntities = commonDAL.GetSources();
            revenueResponse.functionEntities = commonDAL.GetFunctions();
            revenueResponse.departmentEntities = commonDAL.GetDepartments();
            revenueResponse.orgEntities = commonDAL.GetOrgs();
            revenueResponse.objectEntities = commonDAL.GetObjects();
            revenueResponse.projectEntities = commonDAL.GetProjects();
            revenueResponse.statusEntities = commonDAL.GetStatuses();
            revenueResponse.revenueTypeEntities = commonDAL.GetRevenueTypes();
            revenueResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            DrawRequest drawRequest = new DrawRequest();
            DrawDAL drawDAL = new DrawDAL();
            DrawEntity drawEntity = new DrawEntity();
            drawEntity.AssignedTo = 0;
            drawEntity.StatusId = 0;
            drawRequest.drawEntity = drawEntity;
            revenueResponse.drawEntities = drawDAL.GetDraws(drawRequest).drawEntities;
            JournalDAL journalDAL = new JournalDAL();
            JournalEntity journalEntity = new JournalEntity();
            JournalRequest journalRequest = new JournalRequest();
            journalRequest.journalEntity = journalEntity;

            revenueResponse.journalEntities = journalDAL.GetJournals(journalRequest).journalEntities;

            UserDAL userDAL = new UserDAL();
            revenueResponse.userEntities = userDAL.GetAssignedTo().userEntities;

            revenueResponse.drawEntities = DrawRevenues(revenueRequest).drawEntities;
            return revenueResponse;
        }

        public RevenueResponse SaveRevenue(RevenueRequest revenueRequest)
        {
            try
            {
                if (revenueRequest.revenueEntity.RevenueId > 0)
                    revenueRequest.revenueEntity.SaveString = "U";
                else
                    revenueRequest.revenueEntity.SaveString = "I";

                SqlObject.Parameters = new object[] {
                revenueRequest.revenueEntity.SaveString,
                revenueRequest.revenueEntity.RevenueId,
                revenueRequest.revenueEntity.PeriodId,
                revenueRequest.revenueEntity.SourceId,
                revenueRequest.revenueEntity.JournalId,
                revenueRequest.revenueEntity.FunctionId,
                revenueRequest.revenueEntity.DepartmentId,
                revenueRequest.revenueEntity.ActivityId,
                revenueRequest.revenueEntity.OrgId,
                revenueRequest.revenueEntity.ObjectId,
                revenueRequest.revenueEntity.Fund,
                revenueRequest.revenueEntity.ProjectId,
                revenueRequest.revenueEntity.Reference1,
                revenueRequest.revenueEntity.Reference2,
                revenueRequest.revenueEntity.Reference3,
                revenueRequest.revenueEntity.Reference4,
                revenueRequest.revenueEntity.NetAmount,
                revenueRequest.revenueEntity.Notes,
                revenueRequest.revenueEntity.ModifiedBy,
                revenueRequest.revenueEntity.DrawId,
                revenueRequest.revenueEntity.RevenueDate,
                revenueRequest.revenueEntity.StatusId,
                revenueRequest.revenueEntity.RevenueTypeId,
                revenueRequest.revenueEntity.FiscalYearId,
                revenueRequest.revenueEntity.AssignedTo,
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Revenue.USPSAVEREVENUE, SqlObject.Parameters);
                revenueResponse.Message = string.Empty;
                revenueResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                revenueResponse.ErrorMessage = ex.Message;
                revenueResponse.Exception = ex;
            }
            return revenueResponse;
        }

        public RevenueResponse GetRevenueExpense(RevenueRequest revenueRequest)
        {
            var expenseEntities = new List<ExpenseEntity>();
            SqlObject.Parameters = new object[] {
                revenueRequest.revenueEntity.RevenueId
            };
            var expenseDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPGETREVENUEEXPENSE, SqlObject.Parameters);
            foreach (DataRow revenueDataRow in expenseDataSet.Tables[0].Rows)
            {
                ExpenseEntity revenueEntity = new ExpenseEntity();
                try
                {
                    revenueEntity.ExpenseId = Convert.ToInt64(revenueDataRow["ExpenseId"].ToString());
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
                    revenueEntity.CFDA = revenueDataRow["CFDA"].ToString();
                    revenueEntity.AssignedToUser = revenueDataRow["AssignedToUser"].ToString();
                    revenueEntity.Notes = revenueDataRow["Notes"].ToString();
                    revenueEntity.Fund = revenueDataRow["Fund"].ToString();
                    revenueEntity.Reference1 = revenueDataRow["Reference1"].ToString();
                    revenueEntity.Reference2 = revenueDataRow["Reference2"].ToString();
                    revenueEntity.Reference3 = revenueDataRow["Reference3"].ToString();
                    revenueEntity.Reference4 = revenueDataRow["Reference4"].ToString();
                    revenueEntity.NetAmount = Convert.ToDecimal(revenueDataRow["NetAmount"].ToString());
                    revenueEntity.ExpenseNumber = revenueDataRow["ExpenseNumber"].ToString();
                    revenueEntity.StatusName = revenueDataRow["StatusName"].ToString();
                    revenueEntity.FiscalYear = revenueDataRow["FiscalYear"].ToString();
                    revenueEntity.ExpenseDate = Convert.ToDateTime(revenueDataRow["ExpenseDate"].ToString()).ToShortDateString();
                }
                catch (Exception exception)
                {
                    revenueEntity.ErrorMessage = exception.Message;
                    revenueEntity.Exception = exception;
                }
                finally
                {
                    expenseEntities.Add(revenueEntity);
                }
            }
            if (expenseEntities.Count >= 1 || expenseEntities.Count == 0)
            {
                revenueResponse.ErrorMessage = string.Empty;
                revenueResponse.Message = string.Empty;
            }
            revenueResponse.expenseEntities = expenseEntities;

            return revenueResponse;
        }

        public RevenueResponse GetRevenueDocuments(RevenueRequest revenueRequest)
        {
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.ErrorMessage = string.Empty;
            revenueResponse.ErrorMessage = string.Empty;
            revenueEntity.Message = string.Empty;
            revenueResponse.Message = string.Empty;
            revenueResponse.revenueEntity = GetRevenue(revenueRequest).revenueEntity;
            revenueResponse.expenseEntities = GetRevenueExpense(revenueRequest).expenseEntities;
            DocumentDAL documentDAL = new DocumentDAL();
            DocumentEntity documentEntity = new DocumentEntity();
            documentEntity.DocumentReferenceId = revenueRequest.revenueEntity.RevenueId;
            documentEntity.DocumentTypeId = 2;
            revenueResponse.documentEntities = documentDAL.GetDocuments(documentEntity);
            return revenueResponse;
        }

        public RevenueResponse GetRevenueInformation(RevenueRequest revenueRequest)
        {
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.ErrorMessage = string.Empty;
            revenueResponse.ErrorMessage = string.Empty;
            revenueEntity.Message = string.Empty;
            revenueResponse.Message = string.Empty;
            revenueResponse.revenueEntity = GetRevenue(revenueRequest).revenueEntity;
            revenueResponse.expenseEntities = GetRevenueExpense(revenueRequest).expenseEntities;
            revenueResponse.revenueTransactionEntities = GetRevenueTransactions(revenueRequest).revenueTransactionEntities;
            revenueResponse.transactionDetailEntities = GetTransactionByRevenueId(revenueRequest).transactionDetailEntities;
            return revenueResponse;
        }

        public RevenueResponse GetRevenueDocument(RevenueRequest revenueRequest)
        {
            revenueResponse.ErrorMessage = string.Empty;
            revenueResponse.Message = string.Empty;
            DocumentDAL documentDAL = new DocumentDAL();
            DocumentEntity documentEntity = new DocumentEntity();
            documentEntity.DocumentReferenceId = revenueRequest.documentEntity.DocumentReferenceId;
            documentEntity.DocumentId = revenueRequest.documentEntity.DocumentId;
            documentEntity.DocumentTypeId = 2;
            revenueResponse.documentEntity = documentDAL.GetDocument(documentEntity);
            CommonDAL commonDAL = new CommonDAL();
            revenueResponse.documentCategoryEntities = commonDAL.GetDocumentCategories();
            return revenueResponse;
        }

        public RevenueResponse SaveRevenueDocument(RevenueRequest revenueRequest)
        {
            revenueResponse.ErrorMessage = string.Empty;
            revenueResponse.Message = string.Empty;
            DocumentDAL documentDAL = new DocumentDAL();
            documentDAL.SaveDocument(revenueRequest.documentEntity);
            return revenueResponse;
        }

        public RevenueResponse LinkToDraw(RevenueRequest revenueRequest)
        {
            revenueResponse.revenueEntity = GetRevenueInfo(revenueRequest).revenueEntity;
            revenueResponse.drawEntities = GetLinkToDraw(revenueRequest).drawEntities;
            return revenueResponse;
        }

        public RevenueResponse SaveLinkToDraw(RevenueRequest revenueRequest)
        {
            try
            {
                SqlObject.Parameters = new object[] {
                    revenueRequest.revenueEntity.RevenueId,
                    revenueRequest.revenueEntity.DrawId,
                    revenueRequest.revenueEntity.NetAmount
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Revenue.USPSAVELINKTODRAW, SqlObject.Parameters);
                revenueResponse.Message = string.Empty;
                revenueResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                revenueResponse.ErrorMessage = ex.Message;
                revenueResponse.Exception = ex;
            }
            return revenueResponse;
        }

        public RevenueResponse DrawRevenue(RevenueRequest revenueRequest)
        {
            revenueResponse.drawRevenueEntities = LinkToDraw(revenueRequest).drawEntities;
            revenueResponse.drawEntities = DrawRevenues(revenueRequest).drawEntities;
            return revenueResponse;
        }

        private RevenueResponse DrawRevenues(RevenueRequest revenueRequest)
        {

            var drawEntities = new List<DrawEntity>();
            SqlObject.Parameters = new object[] {
                  revenueRequest.revenueEntity.RevenueId
            };
            var drawDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPGETREVENUEDRAWS, SqlObject.Parameters);
            foreach (DataRow drawDataRow in drawDataSet.Tables[0].Rows)
            {
                DrawEntity drawEntity = new DrawEntity();
                try
                {
                    drawEntity.DrawId = Convert.ToInt64(drawDataRow["DrawId"].ToString());
                    drawEntity.RevenueId = Convert.ToInt64(drawDataRow["RevenueId"].ToString());
                    drawEntity.DrawDownAmount = Convert.ToDecimal(drawDataRow["DarwDownAmount"].ToString());
                    drawEntity.RevenueAmount = Convert.ToDecimal(drawDataRow["RevenueAmount"].ToString());
                    drawEntity.DrawDownDate = Convert.ToDateTime(drawDataRow["DrawDownDate"].ToString()).ToShortDateString();
                    if (drawDataRow["DatePosted"].ToString() != "")
                        drawEntity.DatePosted = Convert.ToDateTime(drawDataRow["DatePosted"].ToString()).ToShortDateString();
                    drawEntity.BatchNumber = drawDataRow["BatchNumber"].ToString();
                    drawEntity.StatusName = drawDataRow["StatusName"].ToString();
                    drawEntity.FiscalYear = drawDataRow["FiscalYear"].ToString();
                    drawEntity.DrawNumber = drawDataRow["DrawNumber"].ToString();
                    drawEntity.CashReceipt = drawDataRow["CashReceipt"].ToString();
                    drawEntity.OrgName = drawDataRow["OrgName"].ToString();
                    drawEntity.ObjectName = drawDataRow["ObjectName"].ToString();
                    drawEntity.ProjectName = drawDataRow["ProjectName"].ToString();
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
            revenueResponse.drawEntities = drawEntities;
            return revenueResponse;
        }

        private RevenueResponse GetLinkToDraw(RevenueRequest revenueRequest)
        {
            var drawEntities = new List<DrawEntity>();
            SqlObject.Parameters = new object[] {
                revenueRequest.revenueEntity.RevenueId
            };
            var drawDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPGETLINKTODRAW, SqlObject.Parameters);
            foreach (DataRow drawDataRow in drawDataSet.Tables[0].Rows)
            {
                DrawEntity drawEntity = new DrawEntity();
                try
                {
                    drawEntity.DrawId = Convert.ToInt64(drawDataRow["DrawId"].ToString());
                    drawEntity.RevenueId = Convert.ToInt64(drawDataRow["RevenueId"].ToString());
                    drawEntity.DrawDownAmount = Convert.ToDecimal(drawDataRow["DarwDownAmount"].ToString());
                    drawEntity.RevenueAmount = Convert.ToDecimal(drawDataRow["RevenueAmount"].ToString());
                    drawEntity.DrawDownDate = Convert.ToDateTime(drawDataRow["DrawDownDate"].ToString()).ToShortDateString();
                    if (drawDataRow["DatePosted"].ToString() != "")
                        drawEntity.DatePosted = Convert.ToDateTime(drawDataRow["DatePosted"].ToString()).ToShortDateString();
                    drawEntity.BatchNumber = drawDataRow["BatchNumber"].ToString();
                    drawEntity.StatusName = drawDataRow["StatusName"].ToString();
                    drawEntity.FiscalYear = drawDataRow["FiscalYear"].ToString();
                    drawEntity.DrawNumber = drawDataRow["DrawNumber"].ToString();
                    drawEntity.CashReceipt = drawDataRow["CashReceipt"].ToString();
                    drawEntity.OrgName = drawDataRow["OrgName"].ToString();
                    drawEntity.ObjectName = drawDataRow["ObjectName"].ToString();
                    drawEntity.ProjectName = drawDataRow["ProjectName"].ToString();
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
            revenueResponse.drawEntities = drawEntities;
            return revenueResponse;
        }

        private RevenueResponse GetRevenueInfo(RevenueRequest revenueRequest)
        {
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.ErrorMessage = string.Empty;
            revenueResponse.ErrorMessage = string.Empty;
            revenueEntity.Message = string.Empty;
            revenueResponse.Message = string.Empty;
            if (revenueRequest.revenueEntity.RevenueId > 0)
            {
                SqlObject.Parameters = new object[] {
                    revenueRequest.revenueEntity.RevenueId
            };
                var revenueDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPGETREVENUE, SqlObject.Parameters);
                foreach (DataRow revenueDataRow in revenueDataSet.Tables[0].Rows)
                {
                    try
                    {
                        revenueEntity.RevenueId = Convert.ToInt64(revenueDataRow["RevenueId"].ToString());
                        revenueEntity.AssignedTo = Convert.ToInt64(revenueDataRow["AssignedTo"].ToString());
                        revenueEntity.StatusId = Convert.ToInt32(revenueDataRow["StatusId"].ToString());
                        revenueEntity.RevenueTypeId = Convert.ToInt32(revenueDataRow["RevenueTypeId"].ToString());
                        revenueEntity.JournalName = revenueDataRow["JournalName"].ToString();
                        revenueEntity.Journal = revenueDataRow["Journal"].ToString();
                        revenueEntity.PeriodName = revenueDataRow["PeriodName"].ToString();
                        revenueEntity.SourceName = revenueDataRow["SourceCode"].ToString();
                        revenueEntity.SourceDescription = revenueDataRow["SourceDescription"].ToString();
                        revenueEntity.FunctionName = revenueDataRow["FunctionCode"].ToString() + ' ' + revenueDataRow["FunctionDescription"].ToString();
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
                        revenueEntity.CFDA = revenueDataRow["CFDA"].ToString();
                        revenueEntity.DrawId = Convert.ToInt64(revenueDataRow["DrawId"].ToString());
                        revenueEntity.DrawName = revenueDataRow["DrawName"].ToString();
                        revenueEntity.Fund = revenueDataRow["Fund"].ToString();
                        revenueEntity.Reference1 = revenueDataRow["Reference1"].ToString();
                        revenueEntity.Reference2 = revenueDataRow["Reference2"].ToString();
                        revenueEntity.Reference3 = revenueDataRow["Reference3"].ToString();
                        revenueEntity.Reference4 = revenueDataRow["Reference4"].ToString();
                        revenueEntity.Notes = revenueDataRow["Notes"].ToString();
                        revenueEntity.NetAmount = Convert.ToDecimal(revenueDataRow["NetAmount"].ToString());
                        revenueEntity.RevenueNumber = revenueDataRow["RevenueNumber"].ToString();
                        revenueEntity.FiscalYear = revenueDataRow["FiscalYear"].ToString();
                        revenueEntity.DrawNumber = revenueDataRow["DrawNumber"].ToString();
                        revenueEntity.AssignedToUser = revenueDataRow["AssignedToUser"].ToString();
                        revenueEntity.StatusName = revenueDataRow["StatusName"].ToString();
                        revenueEntity.RevenueTypeName = revenueDataRow["RevenueTypeName"].ToString();
                        if (revenueDataRow["RevenueDate"].ToString() != String.Empty)
                            revenueEntity.RevenueDate = Convert.ToDateTime(revenueDataRow["RevenueDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception exception)
                    {
                        revenueEntity.ErrorMessage = exception.Message;
                        revenueResponse.ErrorMessage = exception.Message;
                        revenueEntity.Exception = exception;
                        revenueResponse.Exception = exception;
                    }
                    finally
                    {

                    }
                }
            }
            revenueResponse.revenueEntity = revenueEntity;
            return revenueResponse;
        }

        public RevenueResponse GetRevenueTransactions(RevenueRequest revenueRequest)
        {
            revenueResponse.ErrorMessage = string.Empty;
            revenueResponse.Message = string.Empty;
            List<RevenueTransactionEntity> revenueTransactionEntities = new List<RevenueTransactionEntity>();
            SqlObject.Parameters = new object[] {
                    revenueRequest.revenueEntity.RevenueId
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPGETREVENUETRANSACTIONS, SqlObject.Parameters);
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
                    revenueTransactionEntity.ActivityDescription = revenueDataRow["ActivityDescription"].ToString();
                    revenueTransactionEntity.ObjectDescription = revenueDataRow["ObjectDescription"].ToString();
                    revenueTransactionEntity.OrgName = revenueDataRow["OrgName"].ToString();
                    revenueTransactionEntity.ObjectName = revenueDataRow["ObjectName"].ToString();
                    revenueTransactionEntity.BatchNumber = revenueDataRow["BatchNumber"].ToString();
                    revenueTransactionEntity.ProjectName = revenueDataRow["ProjectName"].ToString();
                    revenueTransactionEntity.CompleteCount = Convert.ToInt32(revenueDataRow["CompleteCount"].ToString());
                    revenueTransactionEntity.ExpenseCount = Convert.ToInt32(revenueDataRow["ExpenseCount"].ToString());
                    revenueTransactionEntity.DocumentFile = revenueDataRow["DocumentFile"].ToString();
                    revenueTransactionEntity.DrawNumber = revenueDataRow["DrawNumber"].ToString();
                    revenueTransactionEntity.RelatedTrans = revenueDataRow["RelatedTrans"].ToString();
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
            if (revenueTransactionEntities.Count >= 1 || revenueTransactionEntities.Count == 0)
            {
                revenueResponse.ErrorMessage = string.Empty;
                revenueResponse.Message = string.Empty;
            }
            revenueResponse.revenueTransactionEntities = revenueTransactionEntities;
            return revenueResponse;
        }

        public RevenueResponse GetRevenueTransaction(RevenueRequest revenueRequest)
        {
            revenueResponse.ErrorMessage = string.Empty;
            revenueResponse.Message = string.Empty;
            RevenueEntity revenueEntity = new RevenueEntity();
            revenueEntity.RevenueId = revenueRequest.revenueTransactionEntity.RevenueId; 
            revenueRequest.revenueEntity = revenueEntity;
            revenueEntity = GetRevenue(revenueRequest).revenueEntity;
                
            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
            if (revenueRequest.revenueTransactionEntity.RevenueTransactionId > 0)
            {
                SqlObject.Parameters = new object[] {
                    revenueRequest.revenueTransactionEntity.RevenueTransactionId
                };
                var revenueDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPGETREVENUETRANSACTION, SqlObject.Parameters);
                foreach (DataRow revenueDataRow in revenueDataSet.Tables[0].Rows)
                {
                    try
                    {
                        revenueTransactionEntity.RevenueTransactionId = Convert.ToInt64(revenueDataRow["RevenueTransactionId"].ToString());
                        revenueTransactionEntity.RevenueId = Convert.ToInt64(revenueDataRow["RevenueId"].ToString());
                        revenueTransactionEntity.DrawId = Convert.ToInt64(revenueDataRow["DrawId"].ToString());
                        revenueTransactionEntity.RevenueTypeId = Convert.ToInt32(revenueDataRow["RevenueTypeId"].ToString());
                        revenueTransactionEntity.RevenueTypeName = revenueDataRow["RevenueTypeName"].ToString();
                        revenueTransactionEntity.DrawNumber = revenueDataRow["DrawNumber"].ToString();
                        revenueTransactionEntity.RevenueTranscationDescription = revenueDataRow["RevenueTranscationDescription"].ToString();
                        revenueTransactionEntity.RevenueTransactionAmount = Convert.ToDecimal(revenueDataRow["RevenueTransactionAmount"].ToString());
                        revenueTransactionEntity.RevenueTransactionNumber = revenueDataRow["RevenueTransactionNumber"].ToString();
                        revenueTransactionEntity.OrgName = revenueDataRow["OrgName"].ToString();
                        revenueTransactionEntity.ObjectName = revenueDataRow["ObjectName"].ToString();
                        revenueTransactionEntity.ProjectName = revenueDataRow["ProjectName"].ToString();
                        revenueTransactionEntity.RelatedTrans = revenueDataRow["RelatedTrans"].ToString();
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

                    }
                }
            }
            else
            {
                revenueTransactionEntity.OrgName = revenueEntity.OrgName;
                revenueTransactionEntity.ObjectName = revenueEntity.ObjectName;
                revenueTransactionEntity.ProjectName = revenueEntity.ProjectName;
                revenueTransactionEntity.RevenueId = revenueRequest.revenueTransactionEntity.RevenueId;
            }
            revenueEntity.RevenueId = revenueTransactionEntity.RevenueId;
            revenueRequest.revenueEntity = revenueEntity;
            revenueResponse.drawEntities = DrawRevenues(revenueRequest).drawEntities;
            revenueResponse.revenueTransactionEntity = revenueTransactionEntity;
            revenueResponse.revenueEntity = revenueEntity;
            CommonDAL commonDAL = new CommonDAL();
            revenueResponse.orgEntities = commonDAL.GetOrgs();
            revenueResponse.objectEntities = commonDAL.GetObjects();
            revenueResponse.projectEntities = commonDAL.GetProjects();

            RevenueTypeDAL revenueTypeDAL = new RevenueTypeDAL();
            revenueResponse.revenueTypeEntities = revenueTypeDAL.GetRevenueTypes().RevenueTypeEntities;

            return revenueResponse;
        }

        public RevenueResponse SaveRevenueTransaction(RevenueRequest revenueRequest)
        {
            try
            {
                if (revenueRequest.revenueTransactionEntity.RevenueTransactionId > 0)
                    revenueRequest.revenueTransactionEntity.SaveString = "U";
                else
                    revenueRequest.revenueTransactionEntity.SaveString = "I";

                SqlObject.Parameters = new object[] {
                revenueRequest.revenueTransactionEntity.SaveString,
                revenueRequest.revenueTransactionEntity.RevenueTransactionId,
                revenueRequest.revenueTransactionEntity.RevenueId,
                revenueRequest.revenueTransactionEntity.RevenueTransactionDate,
                revenueRequest.revenueTransactionEntity.RevenueTransactionAmount,
                revenueRequest.revenueTransactionEntity.RevenueTypeId,
                revenueRequest.revenueTransactionEntity.OrgName,
                revenueRequest.revenueTransactionEntity.ObjectName,
                revenueRequest.revenueTransactionEntity.RevenueTranscationDescription,
                revenueRequest.revenueTransactionEntity.ModifiedBy,
                revenueRequest.revenueTransactionEntity.ProjectName,
                revenueRequest.revenueTransactionEntity.DrawId,
                revenueRequest.revenueTransactionEntity.RelatedTrans
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Revenue.USPSAVEREVENEUTRANSACTION, SqlObject.Parameters);
                revenueResponse.Message = string.Empty;
                revenueResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                revenueResponse.ErrorMessage = ex.Message;
                revenueResponse.Exception = ex;
            }
            return revenueResponse;

        }
        
        public RevenueResponse GetRevenueTransactionDetails(RevenueRequest revenueRequest)
        {
            revenueResponse.ErrorMessage = string.Empty;
            revenueResponse.Message = string.Empty;
            List<RevenueTransactionEntity> revenueTransactionEntities = new List<RevenueTransactionEntity>();
            SqlObject.Parameters = new object[] {
                    revenueRequest.revenueTransactionEntity.ProjectName,
                    revenueRequest.revenueTransactionEntity.RevenueTransactionNumber,
                    revenueRequest.revenueTransactionEntity.RevenueTypeId
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPGETALLREVENUETRANSACTIONS, SqlObject.Parameters);
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
                    revenueTransactionEntity.RevenueNumber = revenueDataRow["RevenueNumber"].ToString();
                    revenueTransactionEntity.ActivityDescription = revenueDataRow["ActivityDescription"].ToString();
                    revenueTransactionEntity.ObjectDescription = revenueDataRow["ObjectDescription"].ToString();
                    revenueTransactionEntity.OrgName = revenueDataRow["OrgName"].ToString();
                    revenueTransactionEntity.ObjectName = revenueDataRow["ObjectName"].ToString();
                    revenueTransactionEntity.BatchNumber = revenueDataRow["BatchNumber"].ToString();
                    revenueTransactionEntity.ProjectName = revenueDataRow["ProjectName"].ToString();
                    revenueTransactionEntity.DrawNumber = revenueDataRow["DrawNumber"].ToString();
                    revenueTransactionEntity.RelatedTrans = revenueDataRow["RelatedTrans"].ToString();
                    revenueTransactionEntity.DocumentFile = revenueDataRow["DocumentFile"].ToString();
                    revenueTransactionEntity.ExpenseCount = Convert.ToInt32(revenueDataRow["ExpenseCount"].ToString());
                    revenueTransactionEntity.CompleteCount = Convert.ToInt32(revenueDataRow["CompleteCount"].ToString());
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
            if (revenueTransactionEntities.Count >= 1 || revenueTransactionEntities.Count == 0)
            {
                revenueResponse.ErrorMessage = string.Empty;
                revenueResponse.Message = string.Empty;
            }
            revenueResponse.revenueTransactionEntities = revenueTransactionEntities;

            CommonDAL commonDAL = new CommonDAL();
            revenueResponse.revenueTypeEntities = commonDAL.GetRevenueTypes();
            return revenueResponse;
        }

        public RevenueResponse DeleteRevenueTransaction(RevenueRequest revenueRequest)
        {
            revenueResponse.ErrorMessage = string.Empty;
            revenueResponse.Message = string.Empty;
            try
            {
                SqlObject.Parameters = new object[] {
                    revenueRequest.revenueTransactionEntity.RevenueTransactionId
                };
                var transactionDetailDataSet = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Revenue.USPDELETEREVENUETRANSACTION, SqlObject.Parameters);
            }
            catch (Exception ex)
            {
                revenueResponse.Exception = ex;
                revenueResponse.ErrorMessage = ex.Message;
            }
            return revenueResponse;
        }

        private RevenueResponse GetTransactionByRevenueId(RevenueRequest revenueRequest)
        {
            List<TransactionDetailEntity> transactionDetailEntities = new List<TransactionDetailEntity>();
            revenueResponse.ErrorMessage = string.Empty;
            revenueResponse.Message = string.Empty;
            if (revenueRequest.revenueEntity.RevenueId> 0)
            {
                SqlObject.Parameters = new object[] {
                    revenueRequest.revenueEntity.RevenueId
            };
                var drawDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPGETTRANSACTIONSBYREVENUEID, SqlObject.Parameters);
                foreach (DataRow expenseDataRow in drawDataSet.Tables[0].Rows)
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
                        transactionDetailEntity.OrgName = expenseDataRow["OrgName"].ToString();
                        transactionDetailEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                        transactionDetailEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                        transactionDetailEntity.ObjectDescription = expenseDataRow["ObjectDescription"].ToString();
                        transactionDetailEntity.ActivityDescription = expenseDataRow["ActivityDescription"].ToString();
                        transactionDetailEntity.DrawNumber = expenseDataRow["DrawNumber"].ToString();
                        transactionDetailEntity.VendorName = expenseDataRow["VendorName"].ToString();
                        transactionDetailEntity.StatusName = expenseDataRow["StatusName"].ToString();
                        transactionDetailEntity.CFDA = expenseDataRow["CFDA"].ToString();
                        transactionDetailEntity.RevenueTransactionAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                        transactionDetailEntity.DrawAmount = Convert.ToDecimal(expenseDataRow["DrawAmount"].ToString());
                        transactionDetailEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                        try
                        {
                            transactionDetailEntity.DrawDate = Convert.ToDateTime(expenseDataRow["DrawDate"].ToString()).ToShortDateString();
                        }
                        catch (Exception)
                        {
                            transactionDetailEntity.DrawDate = expenseDataRow["DrawDate"].ToString();
                        }
                        transactionDetailEntity.RevenueTransactionNumber = expenseDataRow["RevenueTransactionNumber"].ToString();
                        transactionDetailEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                        transactionDetailEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                        transactionDetailEntity.RevenueProjectName = expenseDataRow["RevenueProjectName"].ToString();
                        transactionDetailEntity.DocumentFile = expenseDataRow["DocumentFile"].ToString();
                        try
                        {
                            transactionDetailEntity.RevenueTransactionDate = Convert.ToDateTime(expenseDataRow["RevenueTransactionDate"].ToString()).ToShortDateString();
                        }
                        catch (Exception ex)
                        {
                            transactionDetailEntity.RevenueTransactionDate = expenseDataRow["RevenueTransactionDate"].ToString();
                        }
                        transactionDetailEntity.TransactionDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception exception)
                    {
                        revenueResponse.ErrorMessage = exception.Message;
                        revenueResponse.Exception = exception;
                        transactionDetailEntity.TransactionDate = expenseDataRow["TransactionDate"].ToString();
                    }
                    finally
                    {
                        transactionDetailEntities.Add(transactionDetailEntity);
                    }
                }
            }
            if(transactionDetailEntities.Count > 0)
            {
                revenueResponse.ErrorMessage = String.Empty;
                revenueResponse.Message = String.Empty;
            }
            revenueResponse.transactionDetailEntities = transactionDetailEntities;
            return revenueResponse;
        }

        public RevenueResponse GetManageReceivables(RevenueRequest revenueRequest)
        {
            revenueResponse.ErrorMessage = string.Empty;
            revenueResponse.Message = string.Empty;
            var transactionDetailEntities = new List<TransactionDetailEntity>();
            SqlObject.Parameters = new object[] {
                    revenueRequest.transactionDetailEntity.FiscalYearId,
                    revenueRequest.transactionDetailEntity.TransProject
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPGETALLMANAGERECEIVABLES, SqlObject.Parameters);
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
                    transactionDetailEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    transactionDetailEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    transactionDetailEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    transactionDetailEntity.VendorName = expenseDataRow["VendorName"].ToString();
                    transactionDetailEntity.StatusName = expenseDataRow["StatusName"].ToString();
                    transactionDetailEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    transactionDetailEntity.DrawAmount = Convert.ToDecimal(expenseDataRow["DrawAmount"].ToString());
                    transactionDetailEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    transactionDetailEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                    transactionDetailEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                    transactionDetailEntity.DocumentFile = expenseDataRow["DocumentFile"].ToString();
                    transactionDetailEntity.IsMissingExpense = Convert.ToBoolean(expenseDataRow["IsMissingExpense"].ToString());
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
                revenueResponse.ErrorMessage = string.Empty;
                revenueResponse.Message = string.Empty;
            }
            revenueResponse.transactionDetailEntities = transactionDetailEntities;
            CommonDAL commonDAL = new CommonDAL();
            revenueResponse.projectEntities = commonDAL.GetProjects();
            revenueResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            
            return revenueResponse;
        }

        public RevenueResponse GetTransactionDetail(RevenueRequest revenueRequest)
        {
            ExpenseEntity expenseEntity = new ExpenseEntity();
            expenseEntity.ErrorMessage = string.Empty;
            revenueResponse.ErrorMessage = string.Empty;
            expenseEntity.Message = string.Empty;
            revenueResponse.Message = string.Empty;
            TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
            if (revenueRequest.transactionDetailEntity.TransactionDetailId > 0)
            {
                SqlObject.Parameters = new object[] {
                    revenueRequest.transactionDetailEntity.TransactionDetailId
                };
                var expenseDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPGETDRAWTRANSACTIONDETAIL, SqlObject.Parameters);
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
                        transactionDetailEntity.VendorAdjustments = expenseDataRow["VendorAdjustments"].ToString();
                        transactionDetailEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                        transactionDetailEntity.DrawNumber = expenseDataRow["DrawNumber"].ToString();
                        transactionDetailEntity.VendorName = expenseDataRow["VendorName"].ToString();
                        transactionDetailEntity.StatusName = expenseDataRow["StatusName"].ToString();
                        transactionDetailEntity.DrawAmount = Convert.ToDecimal(expenseDataRow["DrawAmount"].ToString());
                        transactionDetailEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                        try
                        {
                            transactionDetailEntity.DrawDate = Convert.ToDateTime(expenseDataRow["DrawDate"].ToString()).ToShortDateString();
                        }
                        catch (Exception)
                        {
                            transactionDetailEntity.DrawDate = expenseDataRow["DrawDate"].ToString();
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

                    }
                }
            }
            
            //revenueResponse.expenseEntity = expenseEntity;
            revenueResponse.transactionDetailEntity = transactionDetailEntity;

            FGTCategoryEntity fGTCategoryEntity = new FGTCategoryEntity();
            FGTCategoryRequest fGTCategoryRequest = new FGTCategoryRequest();
            FGTCategoryDAL fGTCategoryDAL = new FGTCategoryDAL();
            FGTCategoryResponse fGTCategoryResponse;
            fGTCategoryEntity.FGTParentCategoryId = 0;
            fGTCategoryRequest.fGTCategoryEntity = fGTCategoryEntity;
            fGTCategoryResponse = fGTCategoryDAL.GetFGTCategoriesById(fGTCategoryRequest);
            revenueResponse.fgtCategoryEntities = fGTCategoryResponse.fGTCategoryEntities;

            fGTCategoryEntity.FGTParentCategoryId = transactionDetailEntity.FGTCategoryId1;
            fGTCategoryRequest.fGTCategoryEntity = fGTCategoryEntity;
            fGTCategoryResponse = fGTCategoryDAL.GetFGTCategoriesById(fGTCategoryRequest);
            revenueResponse.fgtCategoryEntities2 = fGTCategoryResponse.fGTCategoryEntities;

            revenueResponse.drawEntities = GetDrawsForReceivables();
            // revenueResponse.revenueTransactionEntities = GetRevenueTransactionByExpenseId(transactionDetailEntity.ExpenseId);
            //revenueResponse.transactionDetailEntities = GetTransactionDetails(transactionDetailEntity.ExpenseId);
            VendorDAL vendorDAL = new VendorDAL();
            VendorRequest vendorRequest = new VendorRequest();
            VendorEntity vendorEntity = new VendorEntity();
            vendorEntity.VendorName = string.Empty;
            vendorRequest.vendorEntity = vendorEntity;
            revenueResponse.vendorEntities = vendorDAL.GetVendors(vendorRequest).vendorEntities;

            CommonDAL commonDAL = new CommonDAL();
            revenueResponse.statusEntities = commonDAL.GetStatuses();

            return revenueResponse;
        }

        private List<DrawEntity> GetDrawsForReceivables()
        {
            List<DrawEntity> drawEntities = new List<DrawEntity>();
            SqlObject.Parameters = new object[] {
            };
            var drawDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPGETDRAWSFORRECEIVABLE, SqlObject.Parameters);
            foreach (DataRow drawDataRow in drawDataSet.Tables[0].Rows)
            {
                DrawEntity drawEntity = new DrawEntity();
                try
                {
                    drawEntity.DrawId = Convert.ToInt64(drawDataRow["DrawId"].ToString());
                    drawEntity.RevenueTransactionCount = Convert.ToInt32(drawDataRow["RevenueTransactionCount"].ToString());
                    drawEntity.RevenueCount = Convert.ToInt32(drawDataRow["RevenueCount"].ToString());
                    drawEntity.DrawDownAmount = Convert.ToDecimal(drawDataRow["DarwDownAmount"].ToString());
                    drawEntity.AllocatedAmount = Convert.ToDecimal(drawDataRow["AllocatedAmount"].ToString());
                    drawEntity.AdjustedAmount = Convert.ToDecimal(drawDataRow["AdjustedAmount"].ToString());
                    drawEntity.DrawDownDate = Convert.ToDateTime(drawDataRow["DrawDownDate"].ToString()).ToShortDateString();
                    if (drawDataRow["DatePosted"].ToString() != "")
                        drawEntity.DatePosted = Convert.ToDateTime(drawDataRow["DatePosted"].ToString()).ToShortDateString();
                    drawEntity.BatchNumber = drawDataRow["BatchNumber"].ToString();
                    drawEntity.StatusName = drawDataRow["StatusName"].ToString();
                    drawEntity.FiscalYear = drawDataRow["FiscalYear"].ToString();
                    drawEntity.DrawNumber = drawDataRow["DrawNumber"].ToString();
                    drawEntity.CashReceipt = drawDataRow["CashReceipt"].ToString();
                    drawEntity.OrgName = drawDataRow["OrgName"].ToString();
                    drawEntity.ObjectName = drawDataRow["ObjectName"].ToString();
                    drawEntity.ProjectName = drawDataRow["ProjectName"].ToString();
                    drawEntity.DrawDescription = drawDataRow["DrawDescription"].ToString();
                    drawEntity.AssignedToUser = drawDataRow["AssignedToUser"].ToString();

                    drawEntity.BatchNumber = drawEntity.BatchNumber + " - " + drawEntity.CashReceipt + " - " + drawEntity.DrawNumber + " - " + drawEntity.DrawDownAmount.ToString();
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
                revenueResponse.ErrorMessage = string.Empty;
                revenueResponse.Message = string.Empty;
            }
            return drawEntities;
        }

        public RevenueResponse SaveTransactionDetail(ExpenseRequest expenseRequest)
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
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Revenue.USPSAVETRANSACTIONDRAWRECEIVABLE, SqlObject.Parameters);
                revenueResponse.Message = string.Empty;
                revenueResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                revenueResponse.ErrorMessage = ex.Message;
                revenueResponse.Exception = ex;
            }
            return revenueResponse;
        }

        public RevenueResponse CheckRelatedTrans(RevenueRequest revenueRequest)
        {
            revenueResponse.ErrorMessage = String.Empty;
            revenueResponse.Message = String.Empty;
            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
            SqlObject.Parameters = new object[] {
                revenueRequest.revenueTransactionEntity.RevenueTransactionId,
                revenueRequest.revenueTransactionEntity.RelatedTrans
            };
            var expenseDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPCHECKRELATEDREVENUETRANSACTION, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in expenseDataSet.Tables[0].Rows)
            {
                try
                {
                    revenueTransactionEntity.RevenueTransactionNumber = expenseDataRow["RevenueTransactionNumber"].ToString();
                }
                catch (Exception exception)
                {
                    revenueResponse.ErrorMessage = exception.Message;
                    revenueResponse.Exception = exception;
                }
            }
            revenueResponse.revenueTransactionEntity = revenueTransactionEntity;
            return revenueResponse;
        }

        public RevenueResponse GetMissingRevenueTransactions(RevenueRequest revenueRequest)
        {
            revenueResponse.ErrorMessage = string.Empty;
            revenueResponse.Message = string.Empty;
            List<RevenueTransactionEntity> revenueTransactionEntities = new List<RevenueTransactionEntity>();
            SqlObject.Parameters = new object[] {
                    revenueRequest.revenueTransactionEntity.ProjectName,
                    revenueRequest.revenueTransactionEntity.RevenueTransactionNumber,
                    revenueRequest.revenueTransactionEntity.RevenueTypeId
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPGETMISSINGREVENUETRANSACTIONS, SqlObject.Parameters);
            foreach (DataRow revenueDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
                try
                {
                    revenueTransactionEntity.RevenueTransactionId = Convert.ToInt64(revenueDataRow["RevenueTransactionId"].ToString());
                    revenueTransactionEntity.RevenueTypeName = revenueDataRow["RevenueTypeName"].ToString();
                    revenueTransactionEntity.RevenueTranscationDescription = revenueDataRow["RevenueTranscationDescription"].ToString();
                    revenueTransactionEntity.RevenueTransactionAmount = Convert.ToDecimal(revenueDataRow["RevenueTransactionAmount"].ToString());
                    revenueTransactionEntity.FiscalYear = revenueDataRow["FiscalYear"].ToString();
                    revenueTransactionEntity.RevenueTransactionNumber = revenueDataRow["RevenueTransactionNumber"].ToString();
                    //revenueTransactionEntity.ActivityDescription = revenueDataRow["ActivityDescription"].ToString();
                    //revenueTransactionEntity.ObjectDescription = revenueDataRow["ObjectDescription"].ToString();
                    revenueTransactionEntity.OrgName = revenueDataRow["OrgName"].ToString();
                    revenueTransactionEntity.ObjectName = revenueDataRow["ObjectName"].ToString();
                    revenueTransactionEntity.BatchNumber = revenueDataRow["BatchNumber"].ToString();
                    revenueTransactionEntity.ProjectName = revenueDataRow["ProjectName"].ToString();
                    revenueTransactionEntity.CompleteCount = Convert.ToInt32(revenueDataRow["CompleteCount"].ToString());
                    revenueTransactionEntity.ExpenseCount = Convert.ToInt32(revenueDataRow["ExpenseCount"].ToString());
                    revenueTransactionEntity.DocumentFile = revenueDataRow["DocumentFile"].ToString();
                    revenueTransactionEntity.DrawNumber = revenueDataRow["DrawNumber"].ToString();
                    revenueTransactionEntity.RelatedTrans = revenueDataRow["RelatedTrans"].ToString();
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
            if (revenueTransactionEntities.Count >= 1 || revenueTransactionEntities.Count == 0)
            {
                revenueResponse.ErrorMessage = string.Empty;
                revenueResponse.Message = string.Empty;
            }
            revenueResponse.revenueTransactionEntities = revenueTransactionEntities;

            RevenueTypeDAL revenueTypeDAL = new RevenueTypeDAL();
            revenueResponse.revenueTypeEntities = revenueTypeDAL.GetRevenueTypes().RevenueTypeEntities;
            return revenueResponse;
        }

        public RevenueResponse GetMissingRevenueTransaction(RevenueRequest revenueRequest)
        {
            revenueResponse.ErrorMessage = string.Empty;
            revenueResponse.Message = string.Empty;
            RevenueTransactionEntity revenueTransactionEntity = new RevenueTransactionEntity();
            revenueTransactionEntity.FiscalYearId = 0; 
            if (revenueRequest.revenueTransactionEntity.RevenueTransactionId > 0)
            {
                SqlObject.Parameters = new object[] {
                    revenueRequest.revenueTransactionEntity.RevenueTransactionId
                };
                var revenueDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Revenue.USPGETMISSINGREVENUETRANSACTION, SqlObject.Parameters);
                foreach (DataRow revenueDataRow in revenueDataSet.Tables[0].Rows)
                {
                    try
                    {
                        revenueTransactionEntity.RevenueTransactionId = Convert.ToInt64(revenueDataRow["RevenueTransactionId"].ToString());
                        revenueTransactionEntity.DrawId = Convert.ToInt64(revenueDataRow["DrawId"].ToString());
                        revenueTransactionEntity.FiscalYearId = Convert.ToInt64(revenueDataRow["FiscalYearId"].ToString());
                        revenueTransactionEntity.RevenueTypeId = Convert.ToInt32(revenueDataRow["RevenueTypeId"].ToString());
                        revenueTransactionEntity.RevenueTypeName = revenueDataRow["RevenueTypeName"].ToString();
                        revenueTransactionEntity.FiscalYear = revenueDataRow["FiscalYear"].ToString();
                        revenueTransactionEntity.DrawNumber = revenueDataRow["DrawNumber"].ToString();
                        revenueTransactionEntity.RevenueTranscationDescription = revenueDataRow["RevenueTranscationDescription"].ToString();
                        revenueTransactionEntity.RevenueTransactionAmount = Convert.ToDecimal(revenueDataRow["RevenueTransactionAmount"].ToString());
                        revenueTransactionEntity.RevenueTransactionNumber = revenueDataRow["RevenueTransactionNumber"].ToString();
                        revenueTransactionEntity.OrgName = revenueDataRow["OrgName"].ToString();
                        revenueTransactionEntity.ObjectName = revenueDataRow["ObjectName"].ToString();
                        revenueTransactionEntity.ProjectName = revenueDataRow["ProjectName"].ToString();
                        revenueTransactionEntity.RelatedTrans = revenueDataRow["RelatedTrans"].ToString();
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

                    }
                }
            }
            
            revenueResponse.revenueTransactionEntity = revenueTransactionEntity;
            RevenueTypeDAL revenueTypeDAL = new RevenueTypeDAL();
            revenueResponse.revenueTypeEntities = revenueTypeDAL.GetRevenueTypes().RevenueTypeEntities;
            CommonDAL commonDAL = new CommonDAL();
            revenueResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            return revenueResponse;
        }

        public RevenueResponse SaveMissingRevenueTransaction(RevenueRequest revenueRequest)
        {
            try
            {
                if (revenueRequest.revenueTransactionEntity.RevenueTransactionId > 0)
                    revenueRequest.revenueTransactionEntity.SaveString = "U";
                else
                    revenueRequest.revenueTransactionEntity.SaveString = "I";

                SqlObject.Parameters = new object[] {
                revenueRequest.revenueTransactionEntity.SaveString,
                revenueRequest.revenueTransactionEntity.RevenueTransactionId,
                revenueRequest.revenueTransactionEntity.RevenueTransactionDate,
                revenueRequest.revenueTransactionEntity.RevenueTransactionAmount,
                revenueRequest.revenueTransactionEntity.RevenueTypeId,
                revenueRequest.revenueTransactionEntity.OrgName,
                revenueRequest.revenueTransactionEntity.ObjectName,
                revenueRequest.revenueTransactionEntity.RevenueTranscationDescription,
                revenueRequest.revenueTransactionEntity.ModifiedBy,
                revenueRequest.revenueTransactionEntity.ProjectName,
                revenueRequest.revenueTransactionEntity.DrawId,
                revenueRequest.revenueTransactionEntity.RelatedTrans,
                revenueRequest.revenueTransactionEntity.FiscalYearId,
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Revenue.USPSAVEMISSINGREVENEUTRANSACTION, SqlObject.Parameters);
                revenueResponse.Message = string.Empty;
                revenueResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                revenueResponse.ErrorMessage = ex.Message;
                revenueResponse.Exception = ex;
            }
            return revenueResponse;

        }
    }
}
