using ProjectManager.BusinessLayer;
using ProjectManager.InterfaceLayer;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Http;
using CommonEntities = ProjectManager.CommonEntities;
using ProjectMangerModel = ProjectManagerService.Models;

namespace ProjectManagerService.Controllers
{
    [RoutePrefix("api/Projects")]
    public class ProjectsController : ApiController
    {
        private readonly IProjectBL _projectBL = null;

        public ProjectsController()
        {
            _projectBL = new ProjectBL();
        }

        public ProjectsController(IProjectBL projectBL)
        {
            _projectBL = projectBL;
        }

        [HttpGet]
        [Route("GetProjects")]
        public IHttpActionResult GetProjects()
        {
            Collection<ProjectMangerModel.Projects> projects = new Collection<ProjectMangerModel.Projects>();

            var blProjects = _projectBL.GetProjects();
            blProjects.ToList()
                .ForEach(project => projects.Add(
                   new ProjectMangerModel.Projects
                   {
                       ProjectID = project.ProjectID,
                       Project = project.Project,
                       StartDate = project.StartDate,
                       EndDate = project.EndDate,
                       Priority = project.Priority
                   }));

            return Ok(projects);
        }

        [HttpPost]
        [Route("AddProject")]
        public IHttpActionResult AddProject([FromBody]ProjectMangerModel.Projects project)
        {
            try
            {
                CommonEntities.Projects proj = new CommonEntities.Projects
                {
                    ProjectID = project.ProjectID,
                    Project = project.Project,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Priority = project.Priority,
                    ManagerID = project.ManagerID
                };

                var result = _projectBL.AddProject(proj);
                if (result != -1)
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetProjectById/{projId}")]
        public IHttpActionResult GetProjectById(int projId)
        {
            ProjectMangerModel.Projects project = null;

            var blProject = _projectBL.GetProjectById(projId);
            if (blProject == null)
            {
                return NotFound();
            }
            project = new ProjectMangerModel.Projects
            {
                ProjectID = blProject.ProjectID,
                Project = blProject.Project,
                StartDate = blProject.StartDate,
                EndDate = blProject.EndDate,
                Priority = blProject.Priority,
                ManagerID = blProject.ManagerID,
                ManagerName = blProject.ManagerName
            };

            return Ok(project);
        }

        [HttpPost]
        [Route("UpdateProject")]
        public IHttpActionResult UpdateProject([FromBody]ProjectMangerModel.Projects project)
        {
            try
            {
                CommonEntities.Projects proj = new CommonEntities.Projects
                {
                    ProjectID = project.ProjectID,
                    Project = project.Project,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Priority = project.Priority
                };

                var result = _projectBL.UpdateProject(proj);
                if (result != -1)
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
