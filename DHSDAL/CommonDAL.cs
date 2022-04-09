using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class CommonDAL
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;

       public CommonDAL()
        {
            _connectionString = Constant.MRAConnectionString;
        }   
        
        public List<PeriodEntity> GetPeriods()
        {
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Common.USPGETPERIODS);
            List<PeriodEntity> periodEntities = new List<PeriodEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                PeriodEntity periodEntity = new PeriodEntity();
                try
                {
                    periodEntity.PeriodId = Convert.ToInt32(expenseDataRow["PeriodId"]);
                    periodEntity.PeriodName = expenseDataRow["Month"].ToString();
                    periodEntity.StartDate = Convert.ToDateTime(expenseDataRow["StartDate"].ToString());
                    periodEntity.EndDate = Convert.ToDateTime(expenseDataRow["EndDate"].ToString());
                }
                catch (Exception exception)
                {
                    periodEntity.ErrorMessage = exception.Message;
                    periodEntity.Exception = exception;
                }
                finally
                {
                    periodEntities.Add(periodEntity);
                }
            }
            return periodEntities;
        }

        public List<ActivityEntity> GetActivities()
        {
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Common.USPGETACTIVITIES);
            List<ActivityEntity> periodEntities = new List<ActivityEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                ActivityEntity periodEntity = new ActivityEntity();
                try
                {
                    periodEntity.ActivityId = Convert.ToInt32(expenseDataRow["ActivityId"]);
                    periodEntity.ActivityName = expenseDataRow["ActivityCode"].ToString();
                }
                catch (Exception exception)
                {
                    periodEntity.ErrorMessage = exception.Message;
                    periodEntity.Exception = exception;
                }
                finally
                {
                    periodEntities.Add(periodEntity);
                }
            }
            return periodEntities;
        }

        public List<SourceEntity> GetSources()
        {
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Common.USPGETSOURCES);
            List<SourceEntity> sourceEntities = new List<SourceEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                SourceEntity sourceEntity = new SourceEntity();
                try
                {
                    sourceEntity.SourceId = Convert.ToInt32(expenseDataRow["SourceId"]);
                    sourceEntity.SourceName = expenseDataRow["SourceCode"].ToString();
                }
                catch (Exception exception)
                {
                    sourceEntity.ErrorMessage = exception.Message;
                    sourceEntity.Exception = exception;
                }
                finally
                {
                    sourceEntities.Add(sourceEntity);
                }
            }
            return sourceEntities;
        }

        public List<FunctionEntity> GetFunctions()
        {
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Common.USPGETFUNCTIONS);
            List<FunctionEntity> sourceEntities = new List<FunctionEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                FunctionEntity functionEntity = new FunctionEntity();
                try
                {
                    functionEntity.FunctionId = Convert.ToInt32(expenseDataRow["FunctionId"]);
                    functionEntity.FunctionName = expenseDataRow["FunctionCode"].ToString();
                }
                catch (Exception exception)
                {
                    functionEntity.ErrorMessage = exception.Message;
                    functionEntity.Exception = exception;
                }
                finally
                {
                    sourceEntities.Add(functionEntity);
                }
            }
            return sourceEntities;
        }

        public List<DepartmentEntity> GetDepartments()
        {
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Common.USPGETDEPARTMENTS);
            List<DepartmentEntity> sourceEntities = new List<DepartmentEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                DepartmentEntity departmentEntity = new DepartmentEntity();
                try
                {
                    departmentEntity.DepartmentId = Convert.ToInt32(expenseDataRow["DepartmentId"]);
                    departmentEntity.DepartmentName = expenseDataRow["DepartmentCode"].ToString();
                }
                catch (Exception exception)
                {
                    departmentEntity.ErrorMessage = exception.Message;
                    departmentEntity.Exception = exception;
                }
                finally
                {
                    sourceEntities.Add(departmentEntity);
                }
            }
            return sourceEntities;
        }

        public List<OrgEntity> GetOrgs()
        {
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Common.USPGETORGS);
            List<OrgEntity> sourceEntities = new List<OrgEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                OrgEntity orgEntity = new OrgEntity();
                try
                {
                    orgEntity.OrgId = Convert.ToInt32(expenseDataRow["OrgId"]);
                    orgEntity.OrgName = expenseDataRow["OrgCode"].ToString();
                }
                catch (Exception exception)
                {
                    orgEntity.ErrorMessage = exception.Message;
                    orgEntity.Exception = exception;
                }
                finally
                {
                    sourceEntities.Add(orgEntity);
                }
            }
            return sourceEntities;
        }

        public List<ObjectEntity> GetObjects()
        {
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Common.USPGETOBJECTS);
            List<ObjectEntity> sourceEntities = new List<ObjectEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                ObjectEntity objectEntity = new ObjectEntity();
                try
                {
                    objectEntity.ObjectId = Convert.ToInt32(expenseDataRow["ObjectId"]);
                    objectEntity.ObjectName = expenseDataRow["ObjectCode"].ToString();
                }
                catch (Exception exception)
                {
                    objectEntity.ErrorMessage = exception.Message;
                    objectEntity.Exception = exception;
                }
                finally
                {
                    sourceEntities.Add(objectEntity);
                }
            }
            return sourceEntities;
        }

        public List<ProjectEntity> GetProjects()
        {
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Common.USPGETPROJECTS);
            List<ProjectEntity> sourceEntities = new List<ProjectEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                ProjectEntity projectEntity = new ProjectEntity();
                try
                {
                    projectEntity.ProjectId = Convert.ToInt32(expenseDataRow["ProjectId"]);
                    projectEntity.ProjectName = expenseDataRow["ProjectCode"].ToString() + " - " + expenseDataRow["ProjectDescription"].ToString();
                }
                catch (Exception exception)
                {
                    projectEntity.ErrorMessage = exception.Message;
                    projectEntity.Exception = exception;
                }
                finally
                {
                    sourceEntities.Add(projectEntity);
                }
            }
            return sourceEntities;
        }

        public List<StatusEntity> GetStatuses()
        {
            var statusDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Common.USPGETSTATUSES);
            List<StatusEntity> statusEntities = new List<StatusEntity>();
            foreach (DataRow statusDataRow in statusDataSet.Tables[0].Rows)
            {
                StatusEntity statusEntity = new StatusEntity();
                try
                {
                    statusEntity.StatusId = Convert.ToInt32(statusDataRow["StatusId"]);
                    statusEntity.StatusName = statusDataRow["StatusName"].ToString();
                }
                catch (Exception exception)
                {
                    statusEntity.ErrorMessage = exception.Message;
                    statusEntity.Exception = exception;
                }
                finally
                {
                    statusEntities.Add(statusEntity);
                }
            }
            return statusEntities;
        }

        public List<DocumentCategoryEntity> GetDocumentCategories()
        {
            var statusDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Common.USPGETDOCUMENTCATEGORIES);
            List<DocumentCategoryEntity> documentCategoryEntities = new List<DocumentCategoryEntity>();
            foreach (DataRow statusDataRow in statusDataSet.Tables[0].Rows)
            {
                DocumentCategoryEntity documentCategoryEntity = new DocumentCategoryEntity();
                try
                {
                    documentCategoryEntity.DocumentCategoryId = Convert.ToInt32(statusDataRow["DocumentCategoryId"]);
                    documentCategoryEntity.DocumentCategoryName = statusDataRow["DocumentCategory"].ToString();
                }
                catch (Exception exception)
                {
                    documentCategoryEntity.ErrorMessage = exception.Message;
                    documentCategoryEntity.Exception = exception;
                }
                finally
                {
                    documentCategoryEntities.Add(documentCategoryEntity);
                }
            }
            return documentCategoryEntities;
        }

        public List<RevenueTypeEntity> GetRevenueTypes()
        {
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Common.USPGETREVENUETYPES);
            List<RevenueTypeEntity> revenueTypeEntities = new List<RevenueTypeEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                RevenueTypeEntity revenueTypeEntity = new RevenueTypeEntity();
                try
                {
                    revenueTypeEntity.RevenueTypeId = Convert.ToInt32(expenseDataRow["RevenueTypeId"]);
                    revenueTypeEntity.RevenueTypeName = expenseDataRow["RevenueTypeName"].ToString();
                }
                catch (Exception exception)
                {
                    revenueTypeEntity.ErrorMessage = exception.Message;
                    revenueTypeEntity.Exception = exception;
                }
                finally
                {
                    revenueTypeEntities.Add(revenueTypeEntity);
                }
            }
            return revenueTypeEntities;
        }

        public List<FiscalYearEntity> GetFiscalYears()
        {
            var periodDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Common.USPGETFISCALYEARS);
            List<FiscalYearEntity> fiscalYearEntities = new List<FiscalYearEntity>();
            foreach (DataRow expenseDataRow in periodDataSet.Tables[0].Rows)
            {
                FiscalYearEntity fiscalYearEntity = new FiscalYearEntity();
                try
                {
                    fiscalYearEntity.FiscalYearId = Convert.ToInt32(expenseDataRow["FiscalYearId"]);
                    fiscalYearEntity.FiscalYear = expenseDataRow["FiscalYear"].ToString();
                }
                catch (Exception exception)
                {
                    fiscalYearEntity.ErrorMessage = exception.Message;
                    fiscalYearEntity.Exception = exception;
                }
                finally
                {
                    fiscalYearEntities.Add(fiscalYearEntity);
                }
            }
            return fiscalYearEntities;
        }

    }
}
