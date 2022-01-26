using DHSDAL;
using DHSEntities;
using System;

namespace DHSBAL
{
    public class JournalBAL : IJournalRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IJournalDALRepository journalDALRepository;

        JournalResponse journalResponse;
        public JournalBAL()
        {
            journalDALRepository = new JournalDAL();
            journalResponse = new JournalResponse();
        }

        public JournalResponse GetJournals(JournalRequest journalRequest)
        {
            try
            {
                journalResponse = journalDALRepository.GetJournals(journalRequest);
            }
            catch (Exception ex)
            {
                journalResponse.ErrorMessage = ex.Message;
                journalResponse.Exception = ex;
            }
            return journalResponse;
        }

        public JournalResponse GetJournal(JournalRequest journalRequest)
        {
            try
            {
                journalResponse = journalDALRepository.GetJournal(journalRequest);
            }
            catch (Exception ex)
            {
                journalResponse.ErrorMessage = ex.Message;
                journalResponse.Exception = ex;
            }
            return journalResponse;
        }

        public JournalResponse SaveJournal(JournalRequest journalRequest)
        {
            try
            {
                journalResponse = journalDALRepository.SaveJournal(journalRequest);
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
