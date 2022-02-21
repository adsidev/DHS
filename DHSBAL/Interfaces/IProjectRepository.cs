using DHSEntities;

namespace DHSBAL
{
    public interface IProjectRepository
    {
        ProjectResponse GetProjects();
        ProjectResponse GetProjectDetails(ProjectRequest projectRequest);
        ProjectResponse GetProjectResponse(ProjectRequest projectRequest);
    }
}
