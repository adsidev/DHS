using DHSEntities;

namespace DHSDAL
{
    public interface IJournalDALRepository
    {
        JournalResponse GetJournals(JournalRequest journalRequest);
        JournalResponse GetJournal(JournalRequest journalRequest);
        JournalResponse SaveJournal(JournalRequest journalRequest);
    }
}
