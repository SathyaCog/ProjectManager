using Moq;
using ProjectManager.DataAccessLayer;
using ProjectManager.InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CommonEntities = ProjectManager.CommonEntities;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.BusinessLayer;
using System.Collections.ObjectModel;

namespace ProjectManagerService.Tests
{
    public class MockProjectData : IProjectBL
    {
        public Collection<CommonEntities.Projects> GetProjects()
        {
            Mock<ProjectManagerEntities> mockContext = MockDataSetList();
            var projectBL = new ProjectBL(mockContext.Object);
            Collection<CommonEntities.Projects> projects = projectBL.GetProjects();

            return projects;
        }

        public void AddProject(CommonEntities.Projects project)
        {
            Mock<ProjectManagerEntities> mockContext = MockDataSetList();
            var projectBL = new ProjectBL(mockContext.Object);
            projectBL.AddProject(project);
        }

        public void UpdateProject(CommonEntities.Projects project)
        {
            Mock<ProjectManagerEntities> mockContext = MockDataSetList();
            var projectBL = new ProjectBL(mockContext.Object);
            projectBL.UpdateProject(project);
        }

        public void SuspendProject(int projectID)
        {
            Mock<ProjectManagerEntities> mockContext = MockDataSetList();
            var projectBL = new ProjectBL(mockContext.Object);
            projectBL.SuspendProject(projectID);
        }

        private static Mock<ProjectManagerEntities> MockDataSetList()
        {
            var dataProjects = new List<Projects>()
            {
                new Projects
                {
                    ProjectID=1,
                    Project="Project 1",
                    StartDate=DateTime.Now.Date,
                    EndDate=DateTime.Now.Date.AddDays(1)
                },
                new Projects
                {
                    ProjectID=2,
                    Project="Project 2",
                    StartDate=DateTime.Now.Date,
                    EndDate=DateTime.Now.Date.AddDays(1)
                },
                new Projects
                {
                    ProjectID=3,
                    Project="Project 3",
                    StartDate=DateTime.Now.Date,
                    EndDate=DateTime.Now.Date.AddDays(1)
                }
        }.AsQueryable();

            var mocksetProjects = new Mock<DbSet<Projects>>();
            mocksetProjects.As<IQueryable<Projects>>().Setup(m => m.Provider).Returns(dataProjects.Provider);
            mocksetProjects.As<IQueryable<Projects>>().Setup(m => m.Expression).Returns(dataProjects.Expression);
            mocksetProjects.As<IQueryable<Projects>>().Setup(m => m.ElementType).Returns(dataProjects.ElementType);
            mocksetProjects.As<IQueryable<Projects>>().Setup(m => m.GetEnumerator()).Returns(dataProjects.GetEnumerator());

            var dataUsers = new List<Users>()
            {
                new Users
                {
                    UserID=1,
                    ProjectID=1,
                    FirstName="Sathya",
                    LastName="Natarajan",
                    EmployeeID="433461"
                },
                new Users
                {
                    UserID=2,
                    ProjectID=1,
                    FirstName="Murali",
                    LastName="S",
                    EmployeeID="433465"
                },
                new Users
                {
                    UserID=3,
                    ProjectID=2,
                    FirstName="Hari",
                    LastName="M",
                    EmployeeID="433469"
                }
        }.AsQueryable();

            var mocksetUsers = new Mock<DbSet<Users>>();
            mocksetUsers.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(dataUsers.Provider);
            mocksetUsers.As<IQueryable<Users>>().Setup(m => m.Expression).Returns(dataUsers.Expression);
            mocksetUsers.As<IQueryable<Users>>().Setup(m => m.ElementType).Returns(dataUsers.ElementType);
            mocksetUsers.As<IQueryable<Users>>().Setup(m => m.GetEnumerator()).Returns(dataUsers.GetEnumerator());

            var dataTasks = new List<Tasks>()
            {
                new Tasks
                {
                    TaskID=1,
                    Task="Task 1",
                    ProjectID=1,
                    Priority=10,
                    StartDate=DateTime.Now.Date,
                    EndDate=DateTime.Now.Date.AddDays(1)
                },
                new Tasks
                {
                    TaskID=2,
                    Task="Task 2",
                    ProjectID=1,
                    Priority=20,
                    StartDate=DateTime.Now.Date,
                    EndDate=DateTime.Now.Date.AddDays(1),
                    Status=true
                },
                new Tasks
                {
                   TaskID=3,
                    Task="Task 3",
                    ProjectID=2,
                    Priority=10,
                    StartDate=DateTime.Now.Date,
                    EndDate=DateTime.Now.Date.AddDays(1)
                },
                new Tasks
                {
                   TaskID=4,
                    Task="Task 4",
                    ProjectID=2,
                    Priority=20,
                    StartDate=DateTime.Now.Date,
                    EndDate=DateTime.Now.Date.AddDays(1),
                    Status=true
                }
        }.AsQueryable();

            var mocksetTasks = new Mock<DbSet<Tasks>>();
            mocksetUsers.As<IQueryable<Tasks>>().Setup(m => m.Provider).Returns(dataTasks.Provider);
            mocksetUsers.As<IQueryable<Tasks>>().Setup(m => m.Expression).Returns(dataTasks.Expression);
            mocksetUsers.As<IQueryable<Tasks>>().Setup(m => m.ElementType).Returns(dataTasks.ElementType);
            mocksetUsers.As<IQueryable<Tasks>>().Setup(m => m.GetEnumerator()).Returns(dataTasks.GetEnumerator());

            var mockContext = new Mock<ProjectManagerEntities>();
            mockContext.Setup(m => m.Projects).Returns(mocksetProjects.Object);
            mockContext.Setup(m => m.Users).Returns(mocksetUsers.Object);
            mockContext.Setup(m => m.Tasks).Returns(mocksetTasks.Object);

            return mockContext;
        }

    }
}
