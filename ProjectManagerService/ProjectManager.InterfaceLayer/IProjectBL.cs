using System.Collections.ObjectModel;

namespace ProjectManager.InterfaceLayer
{
    public interface IProjectBL
    {
        Collection<CommonEntities.Projects> GetProjects();
        int AddProject(CommonEntities.Projects project);
        CommonEntities.Projects GetProjectById(int projId);
        int UpdateProject(CommonEntities.Projects project);
    }
}
