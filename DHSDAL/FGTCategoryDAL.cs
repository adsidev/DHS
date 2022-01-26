using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class FGTCategoryDAL : IFGTCategoryDALRepository
    { /// <summary>
      /// Connection string
      /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// FiscalPeriod Response <see cref="CostReportInvoiceResponse"/>
        /// </summary>
        FGTCategoryResponse fGTCategoryResponse;

        public FGTCategoryDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            fGTCategoryResponse = new FGTCategoryResponse();
        }

        public FGTCategoryResponse GetFGTCategoriesById(FGTCategoryRequest fGTCategoryRequest)
        {
            SqlObject.Parameters = new object[] {
                    fGTCategoryRequest.fGTCategoryEntity.FGTParentCategoryId
            };
            var fGTCategoryDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.FGTCategory.USPGETFGTCATEGORIESBYTYPEID, SqlObject.Parameters);
            List<FGTCategoryEntity> fGTCategoryEntities = new List<FGTCategoryEntity>();
            foreach (DataRow fGTCategoryDataRow in fGTCategoryDataSet.Tables[0].Rows)
            {
                FGTCategoryEntity fGTCategoryEntity = new FGTCategoryEntity();
                try
                {
                    fGTCategoryEntity.FGTCategoryId = Convert.ToInt64(fGTCategoryDataRow["FGTCategoryId"]);
                    fGTCategoryEntity.FGTParentCategoryId = Convert.ToInt32(fGTCategoryDataRow["FGTParentCategoryId"]);
                    fGTCategoryEntity.FGTCategoryName = fGTCategoryDataRow["FGTCategoryName"].ToString();
                    fGTCategoryEntity.FGTCategoryDescription = fGTCategoryDataRow["FGTCategoryDescription"].ToString();
                }
                catch (Exception exception)
                {
                    fGTCategoryEntity.ErrorMessage = exception.Message;
                    fGTCategoryEntity.Exception = exception;
                }
                finally
                {
                    fGTCategoryEntities.Add(fGTCategoryEntity);
                }
            }
            fGTCategoryResponse.fGTCategoryEntities = fGTCategoryEntities;
            return fGTCategoryResponse;
        }

        public FGTCategoryResponse GetFGTCategories()
        {
            List<FGTCategoryEntity> fGTCategoryEntities = new List<FGTCategoryEntity>();
            SqlObject.Parameters = new object[] {
            };
            var fGTCategoryDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.FGTCategory.USPGETFGTCATEGORIES, SqlObject.Parameters);
            foreach (DataRow fGTCategoryDataRow in fGTCategoryDataSet.Tables[0].Rows)
            {
                FGTCategoryEntity fGTCategoryEntity = new FGTCategoryEntity();
                try
                {
                    fGTCategoryEntity.FGTCategoryId = Convert.ToInt64(fGTCategoryDataRow["FGTCategoryId"].ToString());
                    fGTCategoryEntity.FGTParentCategoryId = Convert.ToInt64(fGTCategoryDataRow["FGTParentCategoryId"].ToString());
                    fGTCategoryEntity.FGTCategoryName = fGTCategoryDataRow["FGTCategoryName"].ToString();
                    fGTCategoryEntity.FGTCategoryDescription = fGTCategoryDataRow["FGTCategoryDescription"].ToString();
                    fGTCategoryEntity.FGTPatentCategoryName = fGTCategoryDataRow["FGTPatentCategoryName"].ToString();
                }
                catch (Exception exception)
                {
                    fGTCategoryEntity.ErrorMessage = exception.Message;
                    fGTCategoryEntity.Exception = exception;
                }
                finally
                {
                    fGTCategoryEntities.Add(fGTCategoryEntity);
                }
            }
            if (fGTCategoryEntities.Count >= 1 || fGTCategoryEntities.Count == 0)
            {
                fGTCategoryResponse.ErrorMessage = string.Empty;
                fGTCategoryResponse.Message = string.Empty;
            }
            fGTCategoryResponse.fGTCategoryEntities = fGTCategoryEntities;
            return fGTCategoryResponse;
        }

        public FGTCategoryResponse GetFGTCategory(FGTCategoryRequest fGTCategoryRequest)
        {
            FGTCategoryEntity fGTCategoryEntity = new FGTCategoryEntity();
            fGTCategoryEntity.ErrorMessage = string.Empty;
            fGTCategoryResponse.ErrorMessage = string.Empty;
            fGTCategoryEntity.Message = string.Empty;
            fGTCategoryResponse.Message = string.Empty;
            if (fGTCategoryRequest.fGTCategoryEntity.FGTCategoryId > 0)
            {
                SqlObject.Parameters = new object[] {
                    fGTCategoryRequest.fGTCategoryEntity.FGTCategoryId
                };
                var fGTCategoryDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.FGTCategory.USPGETFGTCATEGORY, SqlObject.Parameters);
                foreach (DataRow fGTCategoryDataRow in fGTCategoryDataSet.Tables[0].Rows)
                {
                    try
                    {
                        fGTCategoryEntity.FGTCategoryId = Convert.ToInt64(fGTCategoryDataRow["FGTCategoryId"].ToString());
                        fGTCategoryEntity.FGTParentCategoryId = Convert.ToInt64(fGTCategoryDataRow["FGTParentCategoryId"].ToString());
                        fGTCategoryEntity.FGTCategoryName = fGTCategoryDataRow["FGTCategoryName"].ToString();
                        fGTCategoryEntity.FGTCategoryDescription = fGTCategoryDataRow["FGTCategoryDescription"].ToString();
                        fGTCategoryEntity.FGTPatentCategoryName = fGTCategoryDataRow["FGTPatentCategoryName"].ToString();
                    }
                    catch (Exception exception)
                    {
                        fGTCategoryEntity.ErrorMessage = exception.Message;
                        fGTCategoryResponse.ErrorMessage = exception.Message;
                        fGTCategoryEntity.Exception = exception;
                        fGTCategoryResponse.Exception = exception;
                    }
                    finally
                    {

                    }
                }
            }

            fGTCategoryResponse.fGTCategoryEntity = fGTCategoryEntity;
            fGTCategoryResponse.fGTCategoryEntities = GetFGTCategoriesByParentId().fGTCategoryEntities;
            return fGTCategoryResponse;
        }

        public FGTCategoryResponse SaveFGTCategory(FGTCategoryRequest fGTCategoryRequest)
        {
            try
            {
                if (fGTCategoryRequest.fGTCategoryEntity.FGTCategoryId > 0)
                    fGTCategoryRequest.fGTCategoryEntity.SaveString = "U";
                else
                    fGTCategoryRequest.fGTCategoryEntity.SaveString = "I";

                SqlObject.Parameters = new object[] {
                fGTCategoryRequest.fGTCategoryEntity.SaveString,
                fGTCategoryRequest.fGTCategoryEntity.FGTCategoryId,
                fGTCategoryRequest.fGTCategoryEntity.FGTParentCategoryId,
                fGTCategoryRequest.fGTCategoryEntity.FGTCategoryName,
                fGTCategoryRequest.fGTCategoryEntity.FGTCategoryDescription,
                fGTCategoryRequest.fGTCategoryEntity.ModifiedBy
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.FGTCategory.USPSAVEFGTCATEGORY, SqlObject.Parameters);
                fGTCategoryResponse.Message = string.Empty;
                fGTCategoryResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                fGTCategoryResponse.ErrorMessage = ex.Message;
                fGTCategoryResponse.Exception = ex;
            }
            return fGTCategoryResponse;
        }

        private FGTCategoryResponse GetFGTCategoriesByParentId()
        {
            List<FGTCategoryEntity> fGTCategoryEntities = new List<FGTCategoryEntity>();
            SqlObject.Parameters = new object[] {
            };
            var fGTCategoryDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.FGTCategory.USPGETFGTCATEGORIESBYPARENTID, SqlObject.Parameters);
            foreach (DataRow fGTCategoryDataRow in fGTCategoryDataSet.Tables[0].Rows)
            {
                FGTCategoryEntity fGTCategoryEntity = new FGTCategoryEntity();
                try
                {
                    fGTCategoryEntity.FGTCategoryId = Convert.ToInt64(fGTCategoryDataRow["FGTCategoryId"].ToString());
                    fGTCategoryEntity.FGTParentCategoryId = Convert.ToInt64(fGTCategoryDataRow["FGTParentCategoryId"].ToString());
                    fGTCategoryEntity.FGTCategoryName = fGTCategoryDataRow["FGTCategoryName"].ToString();
                    fGTCategoryEntity.FGTCategoryDescription = fGTCategoryDataRow["FGTCategoryDescription"].ToString();
                    fGTCategoryEntity.FGTPatentCategoryName = fGTCategoryDataRow["FGTPatentCategoryName"].ToString();
                }
                catch (Exception exception)
                {
                    fGTCategoryEntity.ErrorMessage = exception.Message;
                    fGTCategoryEntity.Exception = exception;
                }
                finally
                {
                    fGTCategoryEntities.Add(fGTCategoryEntity);
                }
            }
            if (fGTCategoryEntities.Count >= 1 || fGTCategoryEntities.Count == 0)
            {
                fGTCategoryResponse.ErrorMessage = string.Empty;
                fGTCategoryResponse.Message = string.Empty;
            }
            fGTCategoryResponse.fGTCategoryEntities = fGTCategoryEntities;
            return fGTCategoryResponse;
        }
    }
}
