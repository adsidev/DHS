using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class JournalDAL : IJournalDALRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Journal Response <see cref="JournalResponse"/>
        /// </summary>
        JournalResponse journalResponse;

        public JournalDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            journalResponse = new JournalResponse();
        }

        public JournalResponse GetJournals(JournalRequest journalRequest)
        {
            var journalEntities = new List<JournalEntity>();
            SqlObject.Parameters = new object[] {
            };
            var journalDataSet= SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Journal.USPGETJOURNALS, SqlObject.Parameters);
            foreach (DataRow journalDataRow in journalDataSet.Tables[0].Rows)
            {
                JournalEntity journalEntity = new JournalEntity();
                try
                {
                    journalEntity.JournalId = Convert.ToInt64(journalDataRow["JournalId"].ToString());
                    journalEntity.JournalName = journalDataRow["JournalName"].ToString(); 
                    journalEntity.JournalCode = journalDataRow["JournalCode"].ToString();
                    journalEntity.JournalDescription = journalDataRow["JournalDescription"].ToString();
                }
                catch (Exception exception)
                {
                    journalEntity.ErrorMessage = exception.Message;
                    journalEntity.Exception = exception;
                }
                finally
                {
                    journalEntities.Add(journalEntity);
                }
            }
            if (journalEntities.Count >= 1 || journalEntities.Count==0)
            {
                journalResponse.ErrorMessage = string.Empty;
                journalResponse.Message = string.Empty;
            }
            journalResponse.journalEntities = journalEntities;
            return journalResponse;
        }

        public JournalResponse GetJournal(JournalRequest journalRequest)
        {

            JournalEntity journalEntity = new JournalEntity();
            journalEntity.ErrorMessage = string.Empty;
            journalResponse.ErrorMessage = string.Empty;
            journalEntity.Message = string.Empty;
            journalResponse.Message = string.Empty;
            if (journalRequest.journalEntity.JournalId > 0)
            {
                SqlObject.Parameters = new object[] {
                    journalRequest.journalEntity.JournalId
            };
                var journalDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Journal.USPGETJOURNAL, SqlObject.Parameters);
                foreach (DataRow journalDataRow in journalDataSet.Tables[0].Rows)
                {
                    try
                    {
                        journalEntity.JournalId = Convert.ToInt64(journalDataRow["JournalId"].ToString());
                        journalEntity.JournalName = journalDataRow["JournalName"].ToString();
                        journalEntity.JournalCode = journalDataRow["JournalCode"].ToString();
                        journalEntity.JournalDescription = journalDataRow["JournalDescription"].ToString();
                    }
                    catch (Exception exception)
                    {
                        journalEntity.ErrorMessage = exception.Message;
                        journalResponse.ErrorMessage = exception.Message;
                        journalEntity.Exception = exception;
                        journalResponse.Exception = exception;
                    }
                    finally
                    {

                    }
                }
            }

            journalResponse.journalEntity = journalEntity;
            return journalResponse;
        }

        public JournalResponse SaveJournal(JournalRequest journalRequest)
        {
            try
            {
                if (journalRequest.journalEntity.JournalId > 0)
                    journalRequest.journalEntity.SaveString = "U";
                else
                    journalRequest.journalEntity.SaveString = "I";

                SqlObject.Parameters = new object[] {
                journalRequest.journalEntity.SaveString,
                journalRequest.journalEntity.JournalId,
                journalRequest.journalEntity.JournalCode,
                journalRequest.journalEntity.JournalName,
                journalRequest.journalEntity.JournalDescription,
                journalRequest.journalEntity.ModifiedBy,

                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Journal.USPSAVEJOURNAL, SqlObject.Parameters);
                journalResponse.Message = string.Empty;
                journalResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                journalResponse.ErrorMessage = ex.Message;
                journalResponse.Exception = ex;
            }
            return journalResponse;
        }
    }
}
