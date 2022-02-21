using DHSEntities;

namespace DHSDAL
{
    public interface IProjectDALRepository
    {
        ProjectResponse GetProjects();
        ProjectResponse GetProjectDetails(ProjectRequest projectRequest);
        ProjectResponse GetProjectResponse(ProjectRequest projectRequest);
    }
}
