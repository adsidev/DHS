using DHSDAL;
using DHSEntities;
using System;

namespace DHSBAL
{
    public class ProjectBAL : IProjectRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IProjectDALRepository ProjectDALRepository;

        ProjectResponse ProjectResponse;
        public ProjectBAL()
        {
            ProjectDALRepository = new ProjectDAL();
            ProjectResponse = new ProjectResponse();
        }

        public ProjectResponse GetProjects()
        {
            try
            {
                ProjectResponse = ProjectDALRepository.GetProjects();
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        public ProjectResponse GetProjectDetails(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectDALRepository.GetProjectDetails(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        public ProjectResponse GetProjectResponse(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectDALRepository.GetProjectResponse(projectRequest);
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
