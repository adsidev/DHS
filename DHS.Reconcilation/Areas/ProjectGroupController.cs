using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
{
    [RoutePrefix("ProjectGroup")]
    public class ProjectGroupController : ApiController
    {
        IProjectRepository ProjectRepository;
        ProjectResponse ProjectResponse;

        public ProjectGroupController()
        {
            ProjectRepository = new ProjectBAL();
            ProjectResponse = new ProjectResponse();
        }

        [HttpGet]
        [Route("GetProjectGroups")]
        public ProjectResponse GetProjectGroups()
        {
            try
            {
                ProjectResponse = ProjectRepository.GetProjectGroups();
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }


        [HttpPost]
        [Route("GetProjectGroup")]
        public ProjectResponse GetProjectGroup(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectRepository.GetProjectGroup(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }


        [HttpPost]
        [Route("SaveProjectGroup")]
        public ProjectResponse SaveProjectGroup(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectRepository.SaveProjectGroup(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        [HttpPost]
        [Route("CheckProjectGroup")]
        public ProjectResponse CheckProjectGroup(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectRepository.CheckProjectGroup(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

    }
}
