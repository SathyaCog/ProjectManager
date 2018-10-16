using ProjectManager.DataAccessLayer;
using ProjectManager.InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BusinessLayer
{
    public class TaskBL : ITaskBL
    {
        private readonly ProjectManagerEntities _projectManager;

        public TaskBL()
        {
            _projectManager = new ProjectManagerEntities();
        }

        public TaskBL(ProjectManagerEntities projectManager)
        {
            _projectManager = projectManager;
        }

        public Collection<CommonEntities.ParentTasks> GetParentTasks()
        {

            Collection<CommonEntities.ParentTasks> taskCollection = new Collection<CommonEntities.ParentTasks>();
            _projectManager.ParentTasks
                .Select(u => new CommonEntities.ParentTasks()
                {
                    ParentTaskID = u.ParentTaskID,
                    ParentTask = u.ParentTask
                }).ToList()
               .ForEach(y => taskCollection.Add(y));

            return taskCollection;
        }

        public void AddTask(CommonEntities.Tasks task)
        {
            Tasks tk = new Tasks
            {
                Task = task.Task,
                ProjectID = task.ProjectID,
                Priority = task.Priority,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                Status = false
            };
            if (task.ParentTaskID == 0)
            {
                tk.ParentTaskID = null;
            }
            else
            {
                tk.ParentTaskID = task.ParentTaskID;
            }


            _projectManager.Tasks.Add(tk);
            _projectManager.SaveChanges();
            var taskId = tk.TaskID;
            var ur = _projectManager.Users.Where(x => x.UserID == task.UserID).FirstOrDefault();
            if (ur != null)
            {
                ur.TaskID = taskId;
                _projectManager.SaveChanges();
            }
        }

        public void AddParentTask(CommonEntities.ParentTasks pTask)
        {
            ParentTasks tk = new ParentTasks
            {
                ParentTask = pTask.ParentTask
            };

            _projectManager.ParentTasks.Add(tk);
            _projectManager.SaveChanges();
        }
    }
}
