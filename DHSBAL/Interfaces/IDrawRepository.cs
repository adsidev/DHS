using DHSEntities;

namespace DHSBAL
{
    public interface IDrawRepository
    {
        DrawResponse GetDraws(DrawRequest drawRequest);
        DrawResponse GetDraw(DrawRequest drawRequest);
        DrawResponse SaveDraw(DrawRequest drawRequest);
        DrawResponse GetDrawDocuments(DrawRequest drawRequest);
        DrawResponse GetDrawDocument(DrawRequest drawRequest);
        DrawResponse SaveDrawDocument(DrawRequest drawRequest);
        DrawResponse GetTransactionByDrawId(DrawRequest drawRequest);
        DrawResponse CheckDrawByBatchNumber(DrawRequest drawRequest);
    }
}
