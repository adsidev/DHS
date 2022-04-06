using DHSEntities;

namespace DHSBAL
{
    public interface IProjectRepository
    {
        ProjectResponse GetProjects(ProjectRequest projectRequest);
        ProjectResponse GetProjectDetails(ProjectRequest projectRequest);
        ProjectResponse GetProjectResponse(ProjectRequest projectRequest);
        ProjectResponse GetProject(ProjectRequest projectRequest);
        ProjectResponse SaveProject(ProjectRequest projectRequest);
    }
}
