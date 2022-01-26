using System.Data;

namespace DHSEntities
{
    public class ImportRequest
    {
        public DataSet dataset { get; set; }
        public string FiscalYear { get; set; }
    }
}
