using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
{
    [RoutePrefix("Journal")]
    public class JournalController : ApiController
    {
        IJournalRepository journalRepository;
        JournalResponse journalResponse;

        public JournalController()
        {
            journalRepository = new JournalBAL();
            journalResponse = new JournalResponse();
        }

        [HttpPost]
        [Route("GetJournals")]
        public JournalResponse GetJournals(JournalRequest journalRequest)
        {
            try
            {
                journalResponse = journalRepository.GetJournals(journalRequest);
            }
            catch (Exception ex)
            {
                journalResponse.ErrorMessage = ex.Message;
                journalResponse.Exception = ex;
            }
            return journalResponse;
        }

        [HttpPost]
        [Route("GetJournal")]
        public JournalResponse GetJournal(JournalRequest journalRequest)
        {
            try
            {
                journalResponse = journalRepository.GetJournal(journalRequest);
            }
            catch (Exception ex)
            {
                journalResponse.ErrorMessage = ex.Message;
                journalResponse.Exception = ex;
            }
            return journalResponse;
        }

        [HttpPost]
        [Route("SaveJournal")]
        public JournalResponse SaveJournal(JournalRequest journalRequest)
        {
            try
            {
                journalResponse = journalRepository.SaveJournal(journalRequest);
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
