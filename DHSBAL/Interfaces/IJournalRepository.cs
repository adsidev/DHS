using DHSEntities;

namespace DHSBAL
{
    public interface IJournalRepository
    {
        JournalResponse GetJournals(JournalRequest journalRequest);
        JournalResponse GetJournal(JournalRequest journalRequest);
        JournalResponse SaveJournal(JournalRequest journalRequest);
    }
}
