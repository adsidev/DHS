using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class ReportDAL : IReportDALRepository
    { /// <summary>
      /// Connection string
      /// </summary>
        private readonly string _connectionString;

        ReportResponse reportResponse;

        public ReportDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            reportResponse = new ReportResponse();
            reportResponse.Message = String.Empty;
            reportResponse.ErrorMessage = String.Empty;
        }

        public ReportResponse GetGrantProjectReport()
        {
            reportResponse.ErrorMessage = string.Empty;
            reportResponse.Message = string.Empty;
            try
            {
               CommonDAL commonDAL = new CommonDAL();
                reportResponse.projectEntities = commonDAL.GetProjects();
                reportResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
                reportResponse.statusEntities = commonDAL.GetStatuses();
                ProjectDAL projectDAL = new ProjectDAL();
                reportResponse.projectStatusEntities = projectDAL.GetProjectStatus();
            }
            catch (Exception exception)
            {
                reportResponse.ErrorMessage = exception.Message;
                reportResponse.Exception = exception;
            }
            finally
            {
                
            }
            return reportResponse;
        }

        public ReportResponse GetGrantReport(ReportRequest reportRequest)
        {

            reportResponse.reportEntities = GetExpense(reportRequest);
            reportResponse.expenseAdjustments = GetExpenseAdjustments(reportRequest);
            reportResponse.projectReceivables = GetProjectReceivables(reportRequest);
            reportResponse.priorYearExpenseTransactions = GetPriorYearExpenseTransaction(reportRequest);
            reportResponse.projectPayables = GetProjectPayables(reportRequest);
            reportResponse.projectDueFrom = GetProjectDueFrom(reportRequest);
            reportResponse.revenueDeposits = GetRevenueDeposits(reportRequest);
            reportResponse.revenueAdjustments = GetRevenueAdjustments(reportRequest);
            reportResponse.projectDrawNotFound = GetProjectDrawNotFound(reportRequest);
            reportResponse.drawEntities = GetDraws(reportRequest);
            CommonDAL commonDAL = new CommonDAL();
            reportResponse.projectEntities = commonDAL.GetProjects();
            reportResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            return reportResponse;
        }

        private List<ReportEntity> GetExpense(ReportRequest reportRequest)
        {
            List<ReportEntity> reportEntities = new List<ReportEntity>();
            SqlObject.Parameters = new object[] {
                    reportRequest.FiscalYearId,
                    reportRequest.ProjectId,
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Report.USPGETFGTREPORT, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                ReportEntity reportEntity = new ReportEntity();
                try
                {
                    try
                    {
                        reportEntity.ExpenseDepositDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.ExpenseDepositDate = expenseDataRow["TransactionDate"].ToString();
                    }
                    reportEntity.Category = expenseDataRow["FGTCategoryName"].ToString();
                    reportEntity.StatusId = Convert.ToInt32(expenseDataRow["StatusId"].ToString());
                    reportEntity.SubCategory = expenseDataRow["FGTCategoryName2"].ToString();
                    reportEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                    reportEntity.CorrectAmount = Convert.ToDecimal(expenseDataRow["CorrectAmount"].ToString());
                    reportEntity.Fund = expenseDataRow["Fund"].ToString();
                    reportEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                    reportEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                    reportEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                    reportEntity.VendorName = expenseDataRow["VendorName"].ToString();
                    reportEntity.DepartmentName = expenseDataRow["DepartmentName"].ToString();
                    reportEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    reportEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    reportEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    reportEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    reportEntity.DrawDownAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                    try
                    {
                        reportEntity.DrawDownDate = Convert.ToDateTime(expenseDataRow["DrawDownDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DrawDownDate = expenseDataRow["DrawDownDate"].ToString();
                    }
                    reportEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    reportEntity.CashReceipt = expenseDataRow["CashReceipt"].ToString();
                    try
                    {
                        reportEntity.DatePosted = Convert.ToDateTime(expenseDataRow["DatePosted"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DatePosted = expenseDataRow["DatePosted"].ToString();
                    }
                    reportEntity.Remarks = expenseDataRow["DrawDescription"].ToString().Replace("Complete - ","");
                    reportEntity.RevenueTransactionId = Convert.ToInt64(expenseDataRow["RevenueTransactionId"].ToString());
                }
                catch (Exception exception)
                {
                    reportResponse.Message = exception.Message;
                    reportResponse.ErrorMessage = exception.Message;
                }
                finally
                {
                    reportEntities.Add(reportEntity);
                }
            }
            return reportEntities;
        }

        private List<ExpenseAdjustments> GetExpenseAdjustments(ReportRequest reportRequest)
        {
            List<ExpenseAdjustments> reportEntities = new List<ExpenseAdjustments>();
            SqlObject.Parameters = new object[] {
                    reportRequest.ProjectId,
                    reportRequest.FiscalYearId,
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Report.USPFGTREPORT, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                ExpenseAdjustments reportEntity = new ExpenseAdjustments();
                try
                {
                    try
                    {
                        reportEntity.ExpenseAdjustmentDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.ExpenseAdjustmentDate = expenseDataRow["TransactionDate"].ToString();
                    }

                    reportEntity.Category = expenseDataRow["FGTCategoryName"].ToString();
                    reportEntity.SubCategory = expenseDataRow["FGTCategoryName2"].ToString();
                    reportEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                    reportEntity.CorrectAmount = Convert.ToDecimal(expenseDataRow["CorrectAmount"].ToString());
                    reportEntity.Fund = expenseDataRow["Fund"].ToString();
                    reportEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                    reportEntity.VendorName = expenseDataRow["VendorName"].ToString();
                    reportEntity.DepartmentName = expenseDataRow["DepartmentName"].ToString();
                    reportEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    reportEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    reportEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    reportEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    reportEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                    reportEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                    reportEntity.StatusName = expenseDataRow["StatusName"].ToString();
                    reportEntity.Remarks = expenseDataRow["TransactionDescription"].ToString().Replace("Complete - ", "");
                }
                catch (Exception exception)
                {
                    reportResponse.Message = exception.Message;
                    reportResponse.ErrorMessage = exception.Message;
                }
                finally
                {
                    reportEntities.Add(reportEntity);
                }
            }
            return reportEntities;
        }

        private List<ProjectReceivables> GetProjectReceivables(ReportRequest reportRequest)
        {
            List<ProjectReceivables> reportEntities = new List<ProjectReceivables>();
            SqlObject.Parameters = new object[] {
                    reportRequest.FiscalYearId,
                    reportRequest.ProjectId,
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Report.USPGETFGTPROJECTRECEIBABLES, SqlObject.Parameters);
            transactionDetailDataSet.Tables[0].DefaultView.Sort = "StatusName";
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].DefaultView.ToTable().Rows)
            {
                ProjectReceivables reportEntity = new ProjectReceivables();
                try
                {
                    try
                    {
                        reportEntity.ExpenseDepositDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.ExpenseDepositDate = expenseDataRow["TransactionDate"].ToString();
                    }

                    reportEntity.Category = expenseDataRow["FGTCategoryName"].ToString();
                    reportEntity.SubCategory = expenseDataRow["FGTCategoryName2"].ToString();
                    reportEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                    reportEntity.ReceivablesDrawNextYear = Convert.ToDecimal(expenseDataRow["ReceivablesDrawNextYear"].ToString());
                    reportEntity.ReceivablesDrawPending = Convert.ToDecimal(expenseDataRow["ReceivablesDrawPending"].ToString());
                    reportEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                    reportEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                    reportEntity.CorrectAmount = Convert.ToDecimal(expenseDataRow["CorrectAmount"].ToString());
                    reportEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                    reportEntity.Fund = expenseDataRow["Fund"].ToString();
                    reportEntity.VendorName = expenseDataRow["VendorName"].ToString();
                    reportEntity.StatusName = expenseDataRow["StatusName"].ToString();
                    reportEntity.DepartmentName = expenseDataRow["DepartmentName"].ToString();
                    reportEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    reportEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    reportEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    reportEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    reportEntity.DrawDownAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                    try
                    {
                        reportEntity.DrawDownDate = Convert.ToDateTime(expenseDataRow["DrawDownDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DrawDownDate = expenseDataRow["DrawDownDate"].ToString();
                    }
                    reportEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    reportEntity.CashReceipt = expenseDataRow["CashReceipt"].ToString();
                    try
                    {
                        reportEntity.DatePosted = Convert.ToDateTime(expenseDataRow["DatePosted"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DatePosted = expenseDataRow["DatePosted"].ToString();
                    }
                    reportEntity.Remarks = expenseDataRow["DrawDescription"].ToString().Replace("Complete - ", "");
                    reportEntity.RevenueTransactionId = Convert.ToInt64(expenseDataRow["RevenueTransactionId"].ToString());
                }
                catch (Exception exception)
                {
                    reportResponse.Message = exception.Message;
                    reportResponse.ErrorMessage = exception.Message;
                }
                finally
                {
                    reportEntities.Add(reportEntity);
                }
            }
            return reportEntities;
        }

        private List<RevenueDeposits> GetRevenueDeposits(ReportRequest reportRequest)
        {
            List<RevenueDeposits> reportEntities = new List<RevenueDeposits>();
            SqlObject.Parameters = new object[] {
                    reportRequest.ProjectId,
                    reportRequest.FiscalYearId,
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Report.USPGETREVENUETRANSACTIONFORREPORT, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                RevenueDeposits reportEntity = new RevenueDeposits();
                try
                {
                    try
                    {
                        reportEntity.RevenueTransactionDate = Convert.ToDateTime(expenseDataRow["RevenueTransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.RevenueTransactionDate = expenseDataRow["RevenueTransactionDate"].ToString();
                    }

                    reportEntity.RevenueTransactionAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                    reportEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    reportEntity.RevenueTransactionNumber = expenseDataRow["RevenueTransactionNumber"].ToString();
                    reportEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    reportEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    reportEntity.RevenueTypeName = expenseDataRow["RevenueTypeName"].ToString();
                    reportEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    reportEntity.Remarks = expenseDataRow["RevenueTranscationDescription"].ToString();
                }
                catch (Exception exception)
                {
                    reportResponse.Message = exception.Message;
                    reportResponse.ErrorMessage = exception.Message;
                }
                finally
                {
                    reportEntities.Add(reportEntity);
                }
            }
            return reportEntities;
        }

        private List<RevenueAdjustments> GetRevenueAdjustments(ReportRequest reportRequest)
        {
            List<RevenueAdjustments> reportEntities = new List<RevenueAdjustments>();
            SqlObject.Parameters = new object[] {
                    reportRequest.ProjectId,
                    reportRequest.FiscalYearId,
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Report.USPGETREVENUETRANSACTIONBYPROJECTIDFORREPORT, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                RevenueAdjustments reportEntity = new RevenueAdjustments();
                try
                {
                    try
                    {
                        reportEntity.RevenueTransactionDate = Convert.ToDateTime(expenseDataRow["RevenueTransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.RevenueTransactionDate = expenseDataRow["RevenueTransactionDate"].ToString();
                    }

                    reportEntity.RevenueTransactionAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                    reportEntity.RevenueTransactionNumber = expenseDataRow["RevenueTransactionNumber"].ToString();
                    reportEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    reportEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    reportEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    reportEntity.RevenueTypeName = expenseDataRow["RevenueTypeName"].ToString();
                    reportEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                    reportEntity.StatusName = expenseDataRow["StatusName"].ToString();
                    reportEntity.Remarks = expenseDataRow["RevenueTranscationDescription"].ToString();
                }
                catch (Exception exception)
                {
                    reportResponse.Message = exception.Message;
                    reportResponse.ErrorMessage = exception.Message;
                }
                finally
                {
                    reportEntities.Add(reportEntity);
                }
            }
            return reportEntities;
        }

        private List<DrawEntity> GetDraws(ReportRequest reportRequest)
        {
            List<DrawEntity> reportEntities = new List<DrawEntity>();
            SqlObject.Parameters = new object[] {
                    reportRequest.ProjectId,
                    reportRequest.FiscalYearId,
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Report.USPGETDRAWSBYPROJECTID, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                DrawEntity reportEntity = new DrawEntity();
                try
                {
                    try
                    {
                        reportEntity.DrawDownDate = Convert.ToDateTime(expenseDataRow["DrawDownDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DrawDownDate = expenseDataRow["DrawDownDate"].ToString();
                    }

                    try
                    {
                        reportEntity.DatePosted = Convert.ToDateTime(expenseDataRow["DatePosted"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DatePosted = expenseDataRow["DatePosted"].ToString();
                    }

                    reportEntity.DrawDownAmount = Convert.ToDecimal(expenseDataRow["DarwDownAmount"].ToString());
                    reportEntity.AdjustedAmount = Convert.ToDecimal(expenseDataRow["AdjustedAmount"].ToString());
                    reportEntity.DrawDescription = expenseDataRow["DrawDescription"].ToString();
                    reportEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    reportEntity.CashReceipt = expenseDataRow["CashReceipt"].ToString();
                }
                catch (Exception exception)
                {
                    reportResponse.Message = exception.Message;
                    reportResponse.ErrorMessage = exception.Message;
                }
                finally
                {
                    reportEntities.Add(reportEntity);
                }
            }
            return reportEntities;
        }

        private List<ProjectPayables> GetProjectPayables(ReportRequest reportRequest)
        {
            List<ProjectPayables> reportEntities = new List<ProjectPayables>();
            SqlObject.Parameters = new object[] {
                    reportRequest.FiscalYearId,
                    reportRequest.ProjectId,
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Report.USPGETFGTPROJECTPAYABLES, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                ProjectPayables reportEntity = new ProjectPayables();
                try
                {
                    try
                    {
                        reportEntity.ExpenseDepositDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.ExpenseDepositDate = expenseDataRow["TransactionDate"].ToString();
                    }

                    reportEntity.Category = expenseDataRow["FGTCategoryName"].ToString();
                    reportEntity.SubCategory = expenseDataRow["FGTCategoryName2"].ToString();
                    reportEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                    reportEntity.CorrectAmount = Convert.ToDecimal(expenseDataRow["CorrectAmount"].ToString());
                    reportEntity.Fund = expenseDataRow["Fund"].ToString();
                    reportEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                    reportEntity.VendorName = expenseDataRow["VendorName"].ToString();
                    reportEntity.DepartmentName = expenseDataRow["DepartmentName"].ToString();
                    reportEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    reportEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    reportEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    reportEntity.DrawProjectName = expenseDataRow["DrawProjectName"].ToString();
                    reportEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    reportEntity.DrawDownAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                    try
                    {
                        reportEntity.DrawDownDate = Convert.ToDateTime(expenseDataRow["DrawDownDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DrawDownDate = expenseDataRow["DrawDownDate"].ToString();
                    }
                    reportEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    reportEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                    reportEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                    reportEntity.CashReceipt = expenseDataRow["CashReceipt"].ToString();
                    try
                    {
                        reportEntity.DatePosted = Convert.ToDateTime(expenseDataRow["DatePosted"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DatePosted = expenseDataRow["DatePosted"].ToString();
                    }
                    reportEntity.Remarks = expenseDataRow["DrawDescription"].ToString().Replace("Complete - ", "");
                    reportEntity.RevenueTransactionId = Convert.ToInt64(expenseDataRow["RevenueTransactionId"].ToString());
                }
                catch (Exception exception)
                {
                    reportResponse.Message = exception.Message;
                    reportResponse.ErrorMessage = exception.Message;
                }
                finally
                {
                    reportEntities.Add(reportEntity);
                }
            }
            return reportEntities;
        }

        private List<ProjectPayables> GetProjectDueFrom(ReportRequest reportRequest)
        {
            List<ProjectPayables> reportEntities = new List<ProjectPayables>();
            SqlObject.Parameters = new object[] {
                    reportRequest.FiscalYearId,
                    reportRequest.ProjectId,
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Report.USPGETFGTPROJECTDUEFROM, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                ProjectPayables reportEntity = new ProjectPayables();
                try
                {
                    try
                    {
                        reportEntity.ExpenseDepositDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.ExpenseDepositDate = expenseDataRow["TransactionDate"].ToString();
                    }

                    reportEntity.Category = expenseDataRow["FGTCategoryName"].ToString();
                    reportEntity.SubCategory = expenseDataRow["FGTCategoryName2"].ToString();
                    reportEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                    reportEntity.CorrectAmount = Convert.ToDecimal(expenseDataRow["CorrectAmount"].ToString());
                    reportEntity.Fund = expenseDataRow["Fund"].ToString();
                    reportEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                    reportEntity.VendorName = expenseDataRow["VendorName"].ToString();
                    reportEntity.DepartmentName = expenseDataRow["DepartmentName"].ToString();
                    reportEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    reportEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    reportEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    reportEntity.DrawProjectName = expenseDataRow["DrawProjectName"].ToString();
                    reportEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    reportEntity.DrawDownAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                    try
                    {
                        reportEntity.DrawDownDate = Convert.ToDateTime(expenseDataRow["DrawDownDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DrawDownDate = expenseDataRow["DrawDownDate"].ToString();
                    }
                    reportEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    reportEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                    reportEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                    reportEntity.CashReceipt = expenseDataRow["CashReceipt"].ToString();
                    try
                    {
                        reportEntity.DatePosted = Convert.ToDateTime(expenseDataRow["DatePosted"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DatePosted = expenseDataRow["DatePosted"].ToString();
                    }
                    reportEntity.Remarks = expenseDataRow["DrawDescription"].ToString().Replace("Complete - ", "");
                    reportEntity.RevenueTransactionId = Convert.ToInt64(expenseDataRow["RevenueTransactionId"].ToString());
                }
                catch (Exception exception)
                {
                    reportResponse.Message = exception.Message;
                    reportResponse.ErrorMessage = exception.Message;
                }
                finally
                {
                    reportEntities.Add(reportEntity);
                }
            }
            return reportEntities;
        }

        private List<ProjectPayables> GetProjectDrawNotFound(ReportRequest reportRequest)
        {
            List<ProjectPayables> reportEntities = new List<ProjectPayables>();
            SqlObject.Parameters = new object[] {
                    reportRequest.FiscalYearId,
                    reportRequest.ProjectId,
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Report.USPGETFGTPROJECTDRAWSNOTFOUND, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].Rows)
            {
                ProjectPayables reportEntity = new ProjectPayables();
                try
                {
                    try
                    {
                        reportEntity.ExpenseDepositDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.ExpenseDepositDate = expenseDataRow["TransactionDate"].ToString();
                    }

                    reportEntity.Category = expenseDataRow["FGTCategoryName"].ToString();
                    reportEntity.SubCategory = expenseDataRow["FGTCategoryName2"].ToString();
                    reportEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                    reportEntity.CorrectAmount = Convert.ToDecimal(expenseDataRow["CorrectAmount"].ToString());
                    reportEntity.Fund = expenseDataRow["Fund"].ToString();
                    reportEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                    reportEntity.VendorName = expenseDataRow["VendorName"].ToString();
                    reportEntity.DepartmentName = expenseDataRow["DepartmentName"].ToString();
                    reportEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    reportEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    reportEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    reportEntity.DrawProjectName = expenseDataRow["DrawProjectName"].ToString();
                    reportEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    reportEntity.DrawDownAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                    try
                    {
                        reportEntity.DrawDownDate = Convert.ToDateTime(expenseDataRow["DrawDownDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DrawDownDate = expenseDataRow["DrawDownDate"].ToString();
                    }
                    reportEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    reportEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                    reportEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                    reportEntity.CashReceipt = expenseDataRow["CashReceipt"].ToString();
                    try
                    {
                        reportEntity.DatePosted = Convert.ToDateTime(expenseDataRow["DatePosted"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DatePosted = expenseDataRow["DatePosted"].ToString();
                    }
                    reportEntity.Remarks = expenseDataRow["DrawDescription"].ToString().Replace("Complete - ", "");
                    reportEntity.RevenueTransactionId = Convert.ToInt64(expenseDataRow["RevenueTransactionId"].ToString());
                }
                catch (Exception exception)
                {
                    reportResponse.Message = exception.Message;
                    reportResponse.ErrorMessage = exception.Message;
                }
                finally
                {
                    reportEntities.Add(reportEntity);
                }
            }
            return reportEntities;
        }

        public ReportResponse GetProjectReceivablesReport(ReportRequest reportRequest)
        {
            List<ProjectReceivables> reportEntities = new List<ProjectReceivables>();
            SqlObject.Parameters = new object[] {
                    reportRequest.FiscalYearId,
                    reportRequest.ProjectStatusId,
                    reportRequest.ProjectId
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Report.USPREPORTPROJECTRECEIVABLES, SqlObject.Parameters);
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].DefaultView.ToTable().Rows)
            {
                ProjectReceivables reportEntity = new ProjectReceivables();
                try
                {
                    try
                    {
                        reportEntity.ExpenseDepositDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.ExpenseDepositDate = expenseDataRow["TransactionDate"].ToString();
                    }
                    reportEntity.Category = expenseDataRow["FGTCategoryName"].ToString();
                    reportEntity.SubCategory = expenseDataRow["FGTCategoryName2"].ToString();
                    reportEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                    reportEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                    reportEntity.StatusName = expenseDataRow["StatusName"].ToString();
                    reportEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                    reportEntity.CorrectAmount = Convert.ToDecimal(expenseDataRow["CorrectAmount"].ToString());
                    reportEntity.ReceivablesDrawNextYear = Convert.ToDecimal(expenseDataRow["ReceivablesDrawNextYear"].ToString());
                    reportEntity.ReceivablesDrawPending = Convert.ToDecimal(expenseDataRow["ReceivablesDrawPending"].ToString());
                    reportEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                    reportEntity.Fund = expenseDataRow["Fund"].ToString();
                    reportEntity.VendorName = expenseDataRow["VendorName"].ToString();
                    reportEntity.DepartmentName = expenseDataRow["DepartmentName"].ToString();
                    reportEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    reportEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    reportEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    reportEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    reportEntity.DrawDownAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                    try
                    {
                        reportEntity.DrawDownDate = Convert.ToDateTime(expenseDataRow["DrawDownDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DrawDownDate = expenseDataRow["DrawDownDate"].ToString();
                    }
                    reportEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    reportEntity.CashReceipt = expenseDataRow["CashReceipt"].ToString();
                    try
                    {
                        reportEntity.DatePosted = Convert.ToDateTime(expenseDataRow["DatePosted"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DatePosted = expenseDataRow["DatePosted"].ToString();
                    }
                    reportEntity.Remarks = expenseDataRow["DrawDescription"].ToString().Replace("Complete - ", "");
                    reportEntity.RevenueTransactionId = Convert.ToInt64(expenseDataRow["RevenueTransactionId"].ToString());
                }
                catch (Exception exception)
                {
                    reportResponse.Message = exception.Message;
                    reportResponse.ErrorMessage = exception.Message;
                }
                finally
                {
                    reportEntities.Add(reportEntity);
                }
            }
            reportResponse.projectReceivables = reportEntities;

            CommonDAL commonDAL = new CommonDAL();
            reportResponse.fiscalYearEntities = commonDAL.GetFiscalYears();
            reportResponse.statusEntities = commonDAL.GetStatuses();
            ProjectDAL projectDAL = new ProjectDAL();
            reportResponse.projectStatusEntities = projectDAL.GetProjectStatus();
            return reportResponse;
        }

        private List<ProjectReceivables> GetPriorYearExpenseTransaction(ReportRequest reportRequest)
        {
            List<ProjectReceivables> reportEntities = new List<ProjectReceivables>();
            SqlObject.Parameters = new object[] {
                    reportRequest.FiscalYearId,
                    reportRequest.ProjectId,
            };
            var transactionDetailDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Report.USPGETFGTPRIORYEAREXPENSETRANSACTIONS, SqlObject.Parameters);
            transactionDetailDataSet.Tables[0].DefaultView.Sort = "StatusName";
            foreach (DataRow expenseDataRow in transactionDetailDataSet.Tables[0].DefaultView.ToTable().Rows)
            {
                ProjectReceivables reportEntity = new ProjectReceivables();
                try
                {
                    try
                    {
                        reportEntity.ExpenseDepositDate = Convert.ToDateTime(expenseDataRow["TransactionDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.ExpenseDepositDate = expenseDataRow["TransactionDate"].ToString();
                    }

                    reportEntity.Category = expenseDataRow["FGTCategoryName"].ToString();
                    reportEntity.SubCategory = expenseDataRow["FGTCategoryName2"].ToString();
                    reportEntity.TransactionAmount = Convert.ToDecimal(expenseDataRow["TransactionAmount"].ToString());
                    reportEntity.ReceivablesDrawNextYear = Convert.ToDecimal(expenseDataRow["ReceivablesDrawNextYear"].ToString());
                    reportEntity.ReceivablesDrawPending = Convert.ToDecimal(expenseDataRow["ReceivablesDrawPending"].ToString());
                    reportEntity.RelatedTrans = expenseDataRow["RelatedTrans"].ToString();
                    reportEntity.OtherBatchNumber = expenseDataRow["OtherBatchNumber"].ToString();
                    reportEntity.CorrectAmount = Convert.ToDecimal(expenseDataRow["CorrectAmount"].ToString());
                    reportEntity.TransactionNumber = expenseDataRow["TransactionNumber"].ToString();
                    reportEntity.Fund = expenseDataRow["Fund"].ToString();
                    reportEntity.VendorName = expenseDataRow["VendorName"].ToString();
                    reportEntity.StatusName = expenseDataRow["StatusName"].ToString();
                    reportEntity.OrgName = expenseDataRow["OrgName"].ToString();
                    reportEntity.ObjectName = expenseDataRow["ObjectName"].ToString();
                    reportEntity.ProjectName = expenseDataRow["ProjectName"].ToString();
                    reportEntity.CFDA = expenseDataRow["CFDA"].ToString();
                    reportEntity.DrawDownAmount = Convert.ToDecimal(expenseDataRow["RevenueTransactionAmount"].ToString());
                    try
                    {
                        reportEntity.DrawDownDate = Convert.ToDateTime(expenseDataRow["DrawDownDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DrawDownDate = expenseDataRow["DrawDownDate"].ToString();
                    }
                    reportEntity.BatchNumber = expenseDataRow["BatchNumber"].ToString();
                    reportEntity.CashReceipt = expenseDataRow["CashReceipt"].ToString();
                    try
                    {
                        reportEntity.DatePosted = Convert.ToDateTime(expenseDataRow["DatePosted"].ToString()).ToShortDateString();
                    }
                    catch (Exception)
                    {
                        reportEntity.DatePosted = expenseDataRow["DatePosted"].ToString();
                    }
                    reportEntity.Remarks = expenseDataRow["DrawDescription"].ToString().Replace("Complete - ", "");
                    reportEntity.RevenueTransactionId = Convert.ToInt64(expenseDataRow["RevenueTransactionId"].ToString());
                }
                catch (Exception exception)
                {
                    reportResponse.Message = exception.Message;
                    reportResponse.ErrorMessage = exception.Message;
                }
                finally
                {
                    reportEntities.Add(reportEntity);
                }
            }
            return reportEntities;
        }
    }
}
