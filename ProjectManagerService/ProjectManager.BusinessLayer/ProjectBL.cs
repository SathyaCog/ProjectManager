using ProjectManager.DataAccessLayer;
using ProjectManager.InterfaceLayer;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectManager.BusinessLayer
{
    public class ProjectBL : IProjectBL
    {
        private readonly ProjectManagerEntities _projectManager;

        public ProjectBL()
        {
            _projectManager = new ProjectManagerEntities();
        }
        public ProjectBL(ProjectManagerEntities projectManager)
        {
            _projectManager = projectManager;
        }
        public Collection<CommonEntities.Projects> GetProjects()
        {

            Collection<CommonEntities.Projects> projCollection = new Collection<CommonEntities.Projects>();
            _projectManager.Projects
                .Join(_projectManager.Users, p => p.ManagerID, u => u.UserID, (p, u) =>
                   new
                   {
                       p.ProjectID,
                       p.Project,
                       p.StartDate,
                       p.EndDate,
                       p.Priority,
                       p.ManagerID,
                       ManagerName = u.FirstName + " " + u.LastName
                   })
               .Select(project => new CommonEntities.Projects()
               {
                   ProjectID = project.ProjectID,
                   Project = project.Project,
                   StartDate = project.StartDate,
                   EndDate = project.EndDate,
                   Priority = project.Priority,
                   ManagerID = project.ManagerID,
                   ManagerName = project.ManagerName
               }).ToList()
               .ForEach(y => projCollection.Add(y));

            return projCollection;
        }
        public int AddProject(CommonEntities.Projects project)
        {
            int result = -1;
            Projects proj = new Projects
            {
                ProjectID = project.ProjectID,
                Project = project.Project,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
                ManagerID = project.ManagerID
            };

            _projectManager.Projects.Add(proj);
            result = _projectManager.SaveChanges();

            return result;
        }
        public CommonEntities.Projects GetProjectById(int projId)
        {

            CommonEntities.Projects project = null;
            project = _projectManager.Projects.Where(x => x.ProjectID == projId)
               .Join(_projectManager.Users, p => p.ManagerID, u => u.UserID, (p, u) =>
                  new
                  {
                      p.ProjectID,
                      p.Project,
                      p.StartDate,
                      p.EndDate,
                      p.Priority,
                      p.ManagerID,
                      ManagerName = u.FirstName + " " + u.LastName
                  })
              .Select(proj => new CommonEntities.Projects()
              {
                  ProjectID = proj.ProjectID,
                  Project = proj.Project,
                  StartDate = proj.StartDate,
                  EndDate = proj.EndDate,
                  Priority = proj.Priority,
                  ManagerID = proj.ManagerID,
                  ManagerName = proj.ManagerName
              }).FirstOrDefault();

            return project;
        }
        public int UpdateProject(CommonEntities.Projects project)
        {
            int result = -1;
            var proj = _projectManager.Projects.Where(x => x.ProjectID == project.ProjectID).FirstOrDefault();
            if (proj != null)
            {
                proj.ProjectID = project.ProjectID;
                proj.Project = project.Project;
                proj.StartDate = project.StartDate;
                proj.EndDate = project.EndDate;
                proj.Priority = project.Priority;
                result = _projectManager.SaveChanges();
            }

            return result;
        }
    }
}
