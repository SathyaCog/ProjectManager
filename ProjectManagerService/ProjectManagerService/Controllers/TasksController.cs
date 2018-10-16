using ProjectManager.BusinessLayer;
using ProjectManager.InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonEntities = ProjectManager.CommonEntities;
using ProjectMangerModel = ProjectManagerService.Models;

namespace ProjectManagerService.Controllers
{
    [RoutePrefix("api/Tasks")]
    public class TasksController : ApiController
    {
        private readonly ITaskBL _taskBL = null;

        public TasksController()
        {
            _taskBL = new TaskBL();
        }

        public TasksController(ITaskBL taskBL)
        {
            _taskBL = taskBL;
        }

        [HttpPost]
        [Route("AddTask")]
        public IHttpActionResult AddTask([FromBody]ProjectMangerModel.Tasks task)
        {
            try
            {
                CommonEntities.Tasks tk = new CommonEntities.Tasks
                {
                    Task = task.Task,
                    ProjectID = task.ProjectID,
                    Priority = task.Priority,
                    ParentTaskID = task.ParentTaskID,
                    StartDate = task.StartDate,
                    EndDate = task.EndDate,
                    UserID = task.UserID
                };

                _taskBL.AddTask(tk);
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("AddParentTask")]
        public IHttpActionResult AddParentTask([FromBody]ProjectMangerModel.ParentTasks task)
        {
            try
            {
                CommonEntities.ParentTasks tk = new CommonEntities.ParentTasks
                {
                    ParentTask = task.ParentTask
                };

                _taskBL.AddParentTask(tk);
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetParentTasks")]
        public IHttpActionResult GetParentTasks()
        {
            Collection<ProjectMangerModel.ParentTasks> parentTasks = new Collection<ProjectMangerModel.ParentTasks>();

            var blTasks = _taskBL.GetParentTasks();
            blTasks.ToList()
                .ForEach(t => parentTasks.Add(
                   new ProjectMangerModel.ParentTasks
                   {
                       ParentTaskID = t.ParentTaskID,
                       ParentTask = t.ParentTask
                   }));
            return Ok(parentTasks);
        }

    }
}
