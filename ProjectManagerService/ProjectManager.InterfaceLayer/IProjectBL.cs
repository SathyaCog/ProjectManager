using System.Collections.ObjectModel;

namespace ProjectManager.InterfaceLayer
{
    public interface IProjectBL
    {
        Collection<CommonEntities.Projects> GetProjects();
        void AddProject(CommonEntities.Projects project);
        //CommonEntities.Projects GetProjectById(int projId);
        void UpdateProject(CommonEntities.Projects project);
    }
}
