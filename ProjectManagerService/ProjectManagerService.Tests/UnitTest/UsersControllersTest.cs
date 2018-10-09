using System.Web.Http.Results;
using NUnit.Framework;
using ProjectManager.InterfaceLayer;
using ProjectManagerService.Controllers;
using System.Collections.ObjectModel;
using ProjectMangerModel = ProjectManagerService.Models;

namespace ProjectManagerService.Tests
{
    [TestFixture]
    public class UsersControllersTest
    {
        [Test]
        public void GetUsersTest()
        {
            IUserBL taskBL = new MockUserData();

            var userController = new UsersController(taskBL);
            var response = userController.GetUsers();
            var responseResult = response as OkNegotiatedContentResult<Collection<ProjectMangerModel.Users>>;
            Assert.IsNotNull(responseResult);
            Assert.IsNotNull(responseResult.Content);
            foreach (var task in responseResult.Content)
            {
                Assert.IsNotNull(task.UserID);
                Assert.IsNotNull(task.FirstName);
                Assert.IsNotNull(task.LastName);
                Assert.IsNotNull(task.EmployeeID);
            }
        }

        [Test]
        public void AddUserTest_Success()
        {
            IUserBL userBL = new MockUserData();

            // Arrange
            var userController = new UsersController(userBL);

            ProjectMangerModel.Users model = new ProjectMangerModel.Users
            {
                UserID = 4,
                FirstName = "Meena",
                LastName = "M",
                EmployeeID = "433466"
            };

            // Act
            var response = userController.AddUser(model);

            // Assert
            Assert.IsTrue(response is OkResult);
        }


    }
}
