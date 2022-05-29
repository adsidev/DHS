
namespace DHSEntities
{
    public class ProjectStatusEntity :Audit
    {
        public int ProjectStatusId { get; set; }
        public string ProjectStatus { get; set;}
        public int ProjectStatusCount { get; set;}
    }
}
