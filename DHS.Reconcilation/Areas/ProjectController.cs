using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
{
    [RoutePrefix("Project")]
    public class ProjectController : ApiController
    {
        IProjectRepository ProjectRepository;
        ProjectResponse ProjectResponse;

        public ProjectController()
        {
            ProjectRepository = new ProjectBAL();
            ProjectResponse = new ProjectResponse();
        }

        [HttpPost]
        [Route("GetProjects")]
        public ProjectResponse GetProjects(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectRepository.GetProjects(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }


        [HttpPost]
        [Route("GetProject")]
        public ProjectResponse GetProject(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectRepository.GetProject(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }


        [HttpPost]
        [Route("SaveProject")]
        public ProjectResponse SaveProject(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectRepository.SaveProject(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        [HttpPost]
        [Route("GetProjectDetails")]
        public ProjectResponse GetProjectDetails(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectRepository.GetProjectDetails(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        [HttpPost]
        [Route("GetProjectResponse")]
        public ProjectResponse GetProjectResponse(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectRepository.GetProjectResponse(projectRequest);
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
