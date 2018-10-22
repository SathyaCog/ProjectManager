using NUnit.Framework;
using ProjectMangerModel = ProjectManagerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.InterfaceLayer;
using ProjectManagerService.Controllers;
using System.Web.Http.Results;
using System.Collections.ObjectModel;

namespace ProjectManagerService.Tests
{
    [TestFixture]
    public class ProjectControllersTest
    {
        [Test]
        public void GetProjectsTest()
        {
            IProjectBL projectBL = new MockProjectData();

            var projectsController = new ProjectsController(projectBL);
            var response = projectsController.GetProjects();
            var responseResult = response as OkNegotiatedContentResult<Collection<ProjectMangerModel.Projects>>;
            Assert.IsNotNull(responseResult);
            Assert.IsNotNull(responseResult.Content);
            foreach (var project in responseResult.Content)
            {
                Assert.IsNotNull(project.ProjectID);
                Assert.IsNotNull(project.Project);
                Assert.IsNotNull(project.Priority);
                Assert.IsNotNull(project.ManagerID);
            }
        }
    }
}
