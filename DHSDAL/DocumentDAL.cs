using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;


namespace DHSDAL
{
    public class DocumentDAL
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// FiscalPeriod Response <see cref="CostReportInvoiceResponse"/>
        /// </summary>

        public DocumentDAL()
        {
            _connectionString = Constant.MRAConnectionString;
        }

        public List<DocumentEntity> GetDocuments(DocumentEntity entity)
        {
            SqlObject.Parameters = new object[] {
                entity.DocumentReferenceId,
                entity.DocumentTypeId
            };
            var documentDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Document.USPGETDOCUMENTS, SqlObject.Parameters);
            List<DocumentEntity> documentEntities = new List<DocumentEntity>();
            foreach (DataRow documentDataRow in documentDataSet.Tables[0].Rows)
            {
                DocumentEntity documentEntity = new DocumentEntity();
                try
                {
                    documentEntity.DocumentId = Convert.ToInt64(documentDataRow["DocumentId"]);
                    documentEntity.DocumentReferenceId = Convert.ToInt64(documentDataRow["DocumentReferenceId"]);
                    documentEntity.DocumentTitle = documentDataRow["DocumentTitle"].ToString();
                    documentEntity.DocumentDescription = documentDataRow["DocumentDescription"].ToString();
                    documentEntity.DocumentFile = documentDataRow["DocumentFile"].ToString();
                    documentEntity.DocumentCategory = documentDataRow["DocumentCategory"].ToString();
                    documentEntity.DocumentCategoryId = Convert.ToInt32(documentDataRow["DocumentCategoryId"].ToString());
                    documentEntity.DisplayOrder = Convert.ToInt32(documentDataRow["DisplayOrder"].ToString());
                }
                catch (Exception exception)
                {
                    documentEntity.ErrorMessage = exception.Message;
                    documentEntity.Exception = exception;
                }
                finally
                {
                    documentEntities.Add(documentEntity);
                }
            }
            return documentEntities;
        }

        public DocumentEntity GetDocument(DocumentEntity entity)
        {
            DocumentEntity documentEntity = new DocumentEntity();
            if (entity.DocumentId==0)
            {
                documentEntity.DocumentReferenceId = entity.DocumentReferenceId;
                return documentEntity;
            }
            SqlObject.Parameters = new object[] {
                entity.DocumentId
            };
            var documentDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Document.USPGETDOCUMENT, SqlObject.Parameters);
            
            foreach (DataRow documentDataRow in documentDataSet.Tables[0].Rows)
            {                
                try
                {
                    documentEntity.DocumentId = Convert.ToInt64(documentDataRow["DocumentId"]);
                    documentEntity.DocumentReferenceId = Convert.ToInt64(documentDataRow["DocumentReferenceId"]);
                    documentEntity.DocumentTitle = documentDataRow["DocumentTitle"].ToString();
                    documentEntity.DocumentDescription = documentDataRow["DocumentDescription"].ToString();
                    documentEntity.DocumentFile = documentDataRow["DocumentFile"].ToString();
                    documentEntity.DocumentCategory = documentDataRow["DocumentCategory"].ToString();
                    documentEntity.DocumentCategoryId = Convert.ToInt32(documentDataRow["DocumentCategoryId"].ToString());
                    documentEntity.DisplayOrder = Convert.ToInt32(documentDataRow["DisplayOrder"].ToString());
                }
                catch (Exception exception)
                {
                    documentEntity.ErrorMessage = exception.Message;
                    documentEntity.Exception = exception;
                }
                finally
                {
                    
                }
            }
            return documentEntity;
        }

        public void SaveDocument(DocumentEntity entity)
        {
            if (entity.DocumentId > 0)
                entity.SaveString = "U";
            else
                entity.SaveString = "I";
            SqlObject.Parameters = new object[] {
                entity.SaveString,
                entity.DocumentId,
                entity.DocumentTypeId,
                entity.DocumentReferenceId,
                entity.DocumentTitle,
                entity.DocumentDescription,
                entity.DocumentCategoryId,
                entity.DocumentFile,
                entity.ModifiedBy,
                entity.DisplayOrder
            };
            var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Document.USPSAVEDOCUMENT, SqlObject.Parameters);
            
            }


    }
        
    }
