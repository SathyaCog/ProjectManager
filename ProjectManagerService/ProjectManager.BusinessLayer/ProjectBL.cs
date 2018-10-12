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
                .Join(_projectManager.Users, p => p.ProjectID, u => u.ProjectID, (p, u) =>
                   new
                   {
                       p.ProjectID,
                       p.Project,
                       p.StartDate,
                       p.EndDate,
                       p.Priority,
                       u.UserID,
                       ManagerName = u.FirstName + " " + u.LastName
                   })
               .Select(project => new CommonEntities.Projects()
               {
                   ProjectID = project.ProjectID,
                   Project = project.Project,
                   StartDate = project.StartDate,
                   EndDate = project.EndDate,
                   Priority = project.Priority,
                   ManagerID = project.UserID,
                   ManagerName = project.ManagerName
               }).ToList()
               .ForEach(y => projCollection.Add(y));

            return projCollection;
        }
        public void AddProject(CommonEntities.Projects project)
        {
            Projects proj = new Projects
            {
                Project = project.Project,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority
            };

            _projectManager.Projects.Add(proj);
            _projectManager.SaveChanges();
            var proId = proj.ProjectID;
            var ur = _projectManager.Users.Where(x => x.UserID == project.ManagerID).FirstOrDefault();
            if (ur != null)
            {
                ur.ProjectID = proId;
                _projectManager.SaveChanges();
            }
        }

        //public CommonEntities.Projects GetProjectById(int projId)
        //{

        //    CommonEntities.Projects project = null;
        //    project = _projectManager.Projects.Where(x => x.ProjectID == projId)
        //       .Join(_projectManager.Users, p => p.ManagerID, u => u.UserID, (p, u) =>
        //          new
        //          {
        //              p.ProjectID,
        //              p.Project,
        //              p.StartDate,
        //              p.EndDate,
        //              p.Priority,
        //              p.ManagerID,
        //              ManagerName = u.FirstName + " " + u.LastName
        //          })
        //      .Select(proj => new CommonEntities.Projects()
        //      {
        //          ProjectID = proj.ProjectID,
        //          Project = proj.Project,
        //          StartDate = proj.StartDate,
        //          EndDate = proj.EndDate,
        //          Priority = proj.Priority,
        //          ManagerID = proj.ManagerID,
        //          ManagerName = proj.ManagerName
        //      }).FirstOrDefault();

        //    return project;
        //}
        public void UpdateProject(CommonEntities.Projects project)
        {
            var proj = _projectManager.Projects.Where(x => x.ProjectID == project.ProjectID).FirstOrDefault();
            if (proj != null)
            {
                proj.ProjectID = project.ProjectID;
                proj.Project = project.Project;
                proj.StartDate = project.StartDate;
                proj.EndDate = project.EndDate;
                proj.Priority = project.Priority;
                _projectManager.SaveChanges();
            }
        }
    }
}
