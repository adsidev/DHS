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
        ProjectResponse GetProjectStatuses();
        ProjectResponse GetProjectStatus(ProjectRequest projectRequest);
        ProjectResponse SaveProjectStatus(ProjectRequest projectRequest);
        ProjectResponse CheckProjectStatus(ProjectRequest projectRequest);
        ProjectResponse GetProjectGroups();
        ProjectResponse GetProjectGroup(ProjectRequest projectRequest);
        ProjectResponse SaveProjectGroup(ProjectRequest projectRequest);
        ProjectResponse CheckProjectGroup(ProjectRequest projectRequest);
    }
}
