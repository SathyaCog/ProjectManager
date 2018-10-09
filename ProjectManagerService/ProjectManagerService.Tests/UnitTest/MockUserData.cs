using Moq;
using ProjectManager.BusinessLayer;
using ProjectManager.DataAccessLayer;
using ProjectManager.InterfaceLayer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using CommonEntities = ProjectManager.CommonEntities;

namespace ProjectManagerService.Tests
{
    public class MockUserData : IUserBL
    {
        public Collection<CommonEntities.Users> GetUsers()
        {
            Mock<ProjectManagerEntities> mockContext = MockDataSetList();
            var userBL = new UserBL(mockContext.Object);
            Collection<CommonEntities.Users> users = userBL.GetUsers();

            return users;
        }
        public int AddUser(CommonEntities.Users user)
        {
            Mock<ProjectManagerEntities> mockContext = MockDataSetList();
            var userBL = new UserBL(mockContext.Object);
            int result = userBL.AddUser(user);

            return result;
        }
        public CommonEntities.Users GetUserById(int userId)
        {
            Mock<ProjectManagerEntities> mockContext = MockDataSetList();
            var userBL = new UserBL(mockContext.Object);
            CommonEntities.Users user = userBL.GetUserById(userId);

            return user;
        }
        public int UpdateUser(CommonEntities.Users user)
        {
            Mock<ProjectManagerEntities> mockContext = MockDataSetList();
            var userBL = new UserBL(mockContext.Object);
            int result = userBL.UpdateUser(user);

            return result;
        }

        private static Mock<ProjectManagerEntities> MockDataSetList()
        {
            var data = new List<Users>()
            {
                new Users
                {
                    UserID=1,
                    FirstName="Sathya",
                    LastName="Natarajan",
                    EmployeeID="433461"
                },
                new Users
                {
                    UserID=2,
                    FirstName="Murali",
                    LastName="S",
                    EmployeeID="433465"
                },
                new Users
                {
                    UserID=3,
                    FirstName="Hari",
                    LastName="M",
                    EmployeeID="433469"
                }
        }.AsQueryable();

            var mockset = new Mock<DbSet<Users>>();
            mockset.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(data.Provider);
            mockset.As<IQueryable<Users>>().Setup(m => m.Expression).Returns(data.Expression);
            mockset.As<IQueryable<Users>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockset.As<IQueryable<Users>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ProjectManagerEntities>();
            mockContext.Setup(m => m.Users).Returns(mockset.Object);

            return mockContext;
        }
    }
}
