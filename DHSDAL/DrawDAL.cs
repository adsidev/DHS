using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class DrawDAL : IDrawDALRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Draw Response <see cref="DrawResponse"/>
        /// </summary>
        DrawResponse drawResponse;

        public DrawDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            drawResponse = new DrawResponse();
        }

        public DrawResponse GetDraws(DrawRequest drawRequest)
        {
            var drawEntities = new List<DrawEntity>();
            SqlObject.Parameters = new object[] {
                drawRequest.drawEntity.StatusId,
                drawRequest.drawEntity.AssignedTo,
                drawRequest.drawEntity.FiscalYearId,
                drawRequest.drawEntity.ProjectName,
                drawRequest.drawEntity.BatchNumber
            };
            var drawDataSet= SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Draw.USPGETDRAWS, SqlObject.Parameters);
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
                    drawEntity.DrawDownDate = Convert.ToDateTime(drawDataRow["DrawDownDate"].ToString()).ToShortDateString();
                    if(drawDataRow["DatePosted"].ToString()!="")
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
            if (drawEntities.Count >= 1 || drawEntities.Count==0)
            {
                drawResponse.ErrorMessage = string.Empty;
                drawResponse.Message = string.Empty;
            }
            drawResponse.drawEntities = drawEntities;
            CommonDAL commonDAL = new CommonDAL();
            drawResponse.statusEntities = commonDAL.GetStatuses();
            drawResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            UserDAL userDAL = new UserDAL();
            drawResponse.userEntities = userDAL.GetAssignedTo().userEntities;
            return drawResponse;
        }

        public DrawResponse GetDraw(DrawRequest drawRequest)
        {

            DrawEntity drawEntity = new DrawEntity();
            drawEntity.ErrorMessage = string.Empty;
            drawResponse.ErrorMessage = string.Empty;
            drawEntity.Message = string.Empty;
            drawResponse.Message = string.Empty;
            if (drawRequest.drawEntity.DrawId > 0)
            {
                SqlObject.Parameters = new object[] {
                    drawRequest.drawEntity.DrawId
            };
                var drawDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Draw.USPGETDRAW, SqlObject.Parameters);
                foreach (DataRow drawDataRow in drawDataSet.Tables[0].Rows)
                {
                    try
                    {
                        drawEntity.DrawId = Convert.ToInt64(drawDataRow["DrawId"].ToString());
                        drawEntity.AssignedTo = Convert.ToInt64(drawDataRow["AssignedTo"].ToString());
                        drawEntity.StatusId = Convert.ToInt32(drawDataRow["StatusId"].ToString());
                        drawEntity.FiscalYearId = Convert.ToInt32(drawDataRow["FiscalYearId"].ToString());
                        drawEntity.DrawDownDate = Convert.ToDateTime(drawDataRow["DrawDownDate"].ToString()).ToShortDateString();
                        if (drawDataRow["DatePosted"].ToString() != "")
                            drawEntity.DatePosted = Convert.ToDateTime(drawDataRow["DatePosted"].ToString()).ToShortDateString();
                        drawEntity.DrawDownAmount = Convert.ToDecimal(drawDataRow["DarwDownAmount"].ToString());
                        drawEntity.BatchNumber = drawDataRow["BatchNumber"].ToString();
                        drawEntity.DrawNumber = drawDataRow["DrawNumber"].ToString();
                        drawEntity.StatusName = drawDataRow["StatusName"].ToString();
                        drawEntity.FiscalYear = drawDataRow["FiscalYear"].ToString();
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
                        drawResponse.ErrorMessage = exception.Message;
                        drawEntity.Exception = exception;
                        drawResponse.Exception = exception;
                    }
                    finally
                    {

                    }
                }
            }

            drawResponse.drawEntity = drawEntity;
            CommonDAL commonDAL = new CommonDAL();
            drawResponse.statusEntities = commonDAL.GetStatuses();
            drawResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            drawResponse.orgEntities = commonDAL.GetOrgs();
            drawResponse.objectEntities = commonDAL.GetObjects();
            drawResponse.projectEntities = commonDAL.GetProjects();

            UserDAL userDAL = new UserDAL();
            drawResponse.userEntities = userDAL.GetAssignedTo().userEntities;
            return drawResponse;
        }

        public DrawResponse SaveDraw(DrawRequest drawRequest)
        {
            try
            {
                if (drawRequest.drawEntity.DrawId > 0)
                    drawRequest.drawEntity.SaveString = "U";
                else
                    drawRequest.drawEntity.SaveString = "I";

                SqlObject.Parameters = new object[] {
                drawRequest.drawEntity.SaveString,
                drawRequest.drawEntity.DrawId,
                drawRequest.drawEntity.DrawDownAmount,
                drawRequest.drawEntity.DrawDownDate,
                drawRequest.drawEntity.DatePosted,
                drawRequest.drawEntity.BatchNumber,
                drawRequest.drawEntity.CashReceipt,
                drawRequest.drawEntity.DrawDescription,
                drawRequest.drawEntity.ModifiedBy,
                drawRequest.drawEntity.StatusId,
                drawRequest.drawEntity.FiscalYearId,
                drawRequest.drawEntity.AssignedTo,
                drawRequest.drawEntity.OrgName,
                drawRequest.drawEntity.ObjectName,
                drawRequest.drawEntity.ProjectName,
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Draw.USPSAVEDRAW, SqlObject.Parameters);
                drawResponse.Message = string.Empty;
                drawResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        public DrawResponse GetDrawDocuments(DrawRequest drawRequest)
        {
            DrawEntity drawEntity = new DrawEntity();
            drawEntity.ErrorMessage = string.Empty;
            drawResponse.ErrorMessage = string.Empty;
            drawEntity.Message = string.Empty;
            drawResponse.Message = string.Empty;
            drawResponse.drawEntity = GetDraw(drawRequest).drawEntity;
            DocumentDAL documentDAL = new DocumentDAL();
            DocumentEntity documentEntity = new DocumentEntity();
            documentEntity.DocumentReferenceId = drawRequest.drawEntity.DrawId;
            documentEntity.DocumentTypeId = 3;
            drawResponse.documentEntities = documentDAL.GetDocuments(documentEntity);
            return drawResponse;
        }

        public DrawResponse GetDrawDocument(DrawRequest drawRequest)
        {
            drawResponse.ErrorMessage = string.Empty;
            drawResponse.Message = string.Empty;
            DocumentDAL documentDAL = new DocumentDAL();
            DocumentEntity documentEntity = new DocumentEntity();
            documentEntity.DocumentReferenceId = drawRequest.documentEntity.DocumentReferenceId;
            documentEntity.DocumentId = drawRequest.documentEntity.DocumentId;
            documentEntity.DocumentTypeId = 3;
            drawResponse.documentEntity = documentDAL.GetDocument(documentEntity);
            CommonDAL commonDAL = new CommonDAL();
            drawResponse.documentCategoryEntities = commonDAL.GetDocumentCategories();
            return drawResponse;
        }

        public DrawResponse SaveDrawDocument(DrawRequest drawRequest)
        {
            drawResponse.ErrorMessage = string.Empty;
            drawResponse.Message = string.Empty;
            DocumentDAL documentDAL = new DocumentDAL();
            documentDAL.SaveDocument(drawRequest.documentEntity);
            return drawResponse;
        }

        public DrawResponse GetTransactionByDrawId(DrawRequest drawRequest)
        {
            List<TransactionDetailEntity> transactionDetailEntities = new List<TransactionDetailEntity>();
            drawResponse.ErrorMessage = string.Empty;
            drawResponse.Message = string.Empty;
            drawResponse.drawEntity = GetDraw(drawRequest).drawEntity;
            if (drawRequest.drawEntity.DrawId > 0)
            {
                SqlObject.Parameters = new object[] {
                    drawRequest.drawEntity.DrawId
            };
                var drawDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Draw.USPGETTRANSACTIONBYDRAWID, SqlObject.Parameters);
                foreach (DataRow drawDataRow in drawDataSet.Tables[0].Rows)
                {
                    TransactionDetailEntity transactionDetailEntity = new TransactionDetailEntity();
                    try
                    {
                        transactionDetailEntity.TransactionAmount = Convert.ToDecimal(drawDataRow["TransactionAmount"].ToString());
                        transactionDetailEntity.FGTCategoryName2 = drawDataRow["FGTCategoryName2"].ToString();
                        transactionDetailEntity.FGTCategoryName1 = drawDataRow["FGTCategoryName1"].ToString();
                        transactionDetailEntity.TransactionDescription = drawDataRow["TransactionDescription"].ToString();
                        transactionDetailEntity.TransactionDate = Convert.ToDateTime(drawDataRow["TransactionDate"].ToString()).ToShortDateString();
                        transactionDetailEntity.VendorAdjustments = drawDataRow["VendorAdjustments"].ToString();
                        transactionDetailEntity.TransactionNumber = drawDataRow["TransactionNumber"].ToString();
                        transactionDetailEntity.OrgName = drawDataRow["OrgName"].ToString();
                        transactionDetailEntity.ObjectName = drawDataRow["ObjectName"].ToString();
                        transactionDetailEntity.ProjectName = drawDataRow["ProjectName"].ToString();
                        transactionDetailEntity.CFDA = drawDataRow["CFDA"].ToString();
                    }
                    catch (Exception exception)
                    {
                        drawResponse.ErrorMessage = exception.Message;
                        drawResponse.Exception = exception;
                    }
                    finally
                    {
                        transactionDetailEntities.Add(transactionDetailEntity);
                    }
                }
            }
            drawResponse.transactionDetailEntities = transactionDetailEntities;
            drawResponse.revenueEntities = GetRevenuesByDrawId(drawRequest).revenueEntities;
            drawResponse.expenseEntities = GetExpensesByDrawId(drawRequest).expenseEntities;
            return drawResponse;
        }

        private DrawResponse GetRevenuesByDrawId(DrawRequest drawRequest)
        {
            var revenueEntities = new List<RevenueEntity>();
            SqlObject.Parameters = new object[] {
                drawRequest.drawEntity.DrawId
            };
            var revenueDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Draw.USPGETREVENUESBYDRAWID, SqlObject.Parameters);
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
            if (revenueEntities.Count >= 1 || revenueEntities.Count == 0)
            {
                drawResponse.ErrorMessage = string.Empty;
                drawResponse.Message = string.Empty;
            }
            drawResponse.revenueEntities = revenueEntities;
            return drawResponse;
        }

        private DrawResponse GetExpensesByDrawId(DrawRequest drawRequest)
        {
            var expenseEntities = new List<ExpenseEntity>();
            SqlObject.Parameters = new object[] {
                drawRequest.drawEntity.DrawId
            };
            var expenseDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Draw.USPGETEXPENSESBYDRAWID, SqlObject.Parameters);
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
            if (expenseEntities.Count >= 1 || expenseEntities.Count == 0)
            {
                drawResponse.ErrorMessage = string.Empty;
                drawResponse.Message = string.Empty;
            }
            drawResponse.expenseEntities = expenseEntities;
            return drawResponse;
        }

        public DrawResponse CheckDrawByBatchNumber(DrawRequest drawRequest)
        {
            bool IsExists = false;
            SqlObject.Parameters = new object[] {
                    drawRequest.drawEntity.BatchNumber,
                    drawRequest.drawEntity.DrawId
            };
            var drawDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Draw.USPCHECKDRAWBYBATCHNUMBER, SqlObject.Parameters);
            foreach (DataRow drawDataRow in drawDataSet.Tables[0].Rows)
            {
                if(Convert.ToInt32(drawDataRow["BatchNumberCount"].ToString())>=1)
                    IsExists = true;
            }
            drawResponse.IsExists = IsExists;
            return drawResponse;
        }

        public DrawResponse DeleteDraw(DrawRequest drawRequest)
        {
            try
            {
                SqlObject.Parameters = new object[] {
                drawRequest.drawEntity.DrawId
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Draw.USPDELETEDRAW, SqlObject.Parameters);
                drawResponse.Message = string.Empty;
                drawResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

    }
}
