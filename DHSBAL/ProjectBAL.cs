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

        public ProjectResponse GetProjects(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectDALRepository.GetProjects(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        public ProjectResponse GetProject(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectDALRepository.GetProject(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        public ProjectResponse SaveProject(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectDALRepository.SaveProject(projectRequest);
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

        public ProjectResponse GetProjectStatuses()
        {
            try
            {
                ProjectResponse = ProjectDALRepository.GetProjectStatuses();
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        public ProjectResponse GetProjectStatus(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectDALRepository.GetProjectStatus(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        public ProjectResponse SaveProjectStatus(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectDALRepository.SaveProjectStatus(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        public ProjectResponse CheckProjectStatus(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectDALRepository.CheckProjectStatus(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        public ProjectResponse GetProjectGroups()
        {
            try
            {
                ProjectResponse = ProjectDALRepository.GetProjectGroups();
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        public ProjectResponse GetProjectGroup(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectDALRepository.GetProjectGroup(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        public ProjectResponse SaveProjectGroup(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectDALRepository.SaveProjectGroup(projectRequest);
            }
            catch (Exception ex)
            {
                ProjectResponse.ErrorMessage = ex.Message;
                ProjectResponse.Exception = ex;
            }
            return ProjectResponse;
        }

        public ProjectResponse CheckProjectGroup(ProjectRequest projectRequest)
        {
            try
            {
                ProjectResponse = ProjectDALRepository.CheckProjectGroup(projectRequest);
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
