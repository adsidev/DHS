using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
{
    [RoutePrefix("ProjectStatus")]
    public class ProjectStatusController : ApiController
    {
        IProjectRepository ProjectRepository;
        ProjectResponse ProjectResponse;

        public ProjectStatusController()
        {
            ProjectRepository = new ProjectBAL();
            ProjectResponse = new ProjectResponse();
        }

        [HttpGet]
        [Route("GetProjectStatuses")]
        public ProjectResponse GetProjectStatuses()
        {
            try
            {
                ProjectResponse = ProjectRepository.GetProjectStatuses();
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }


        [HttpPost]
        [Route("GetProjectStatus")]
        public ProjectResponse GetProjectStatus(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectRepository.GetProjectStatus(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }


        [HttpPost]
        [Route("SaveProjectStatus")]
        public ProjectResponse SaveProjectStatus(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectRepository.SaveProjectStatus(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        [HttpPost]
        [Route("CheckProjectStatus")]
        public ProjectResponse CheckProjectStatus(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectRepository.CheckProjectStatus(projectRequest);
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
