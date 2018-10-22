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
            IUserBL userBL = new MockUserData();

            var userController = new UsersController(userBL);
            var response = userController.GetUsers();
            var responseResult = response as OkNegotiatedContentResult<Collection<ProjectMangerModel.Users>>;
            Assert.IsNotNull(responseResult);
            Assert.IsNotNull(responseResult.Content);
            foreach (var user in responseResult.Content)
            {
                Assert.IsNotNull(user.UserID);
                Assert.IsNotNull(user.FirstName);
                Assert.IsNotNull(user.LastName);
                Assert.IsNotNull(user.EmployeeID);
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

        [Test]
        public void AddUserTest_Error()
        {
            IUserBL userBL = new MockUserData();

            // Arrange
            var userController = new UsersController(null);

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
            Assert.IsTrue(response is InternalServerErrorResult);
        }

        [Test]
        public void UpdateUserTest_Success()
        {
            IUserBL userBL = new MockUserData();

            // Arrange
            var userController = new UsersController(userBL);

            ProjectMangerModel.Users model = new ProjectMangerModel.Users
            {
                UserID = 1,
                FirstName = "Sathya",
                LastName = "Natarajan",
                EmployeeID = "433465"
            };

            // Act
            var response = userController.UpdateUser(model);

            // Assert
            Assert.IsTrue(response is OkResult);
        }

        [Test]
        public void UpdateUserTest_Error()
        {
            IUserBL userBL = new MockUserData();

            // Arrange
            var userController = new UsersController(null);

            ProjectMangerModel.Users model = new ProjectMangerModel.Users
            {
                UserID = 1,
                FirstName = "Sathya",
                LastName = "Natarajan",
                EmployeeID = "433465"
            };

            // Act
            var response = userController.UpdateUser(model);

            // Assert
            Assert.IsTrue(response is InternalServerErrorResult);
        }

        [Test]
        public void DeleteUserTest_Success()
        {
            IUserBL userBL = new MockUserData();

            // Arrange
            var userController = new UsersController(userBL);

            ProjectMangerModel.Users model = new ProjectMangerModel.Users
            {
                UserID = 1,
                FirstName = "Sathya",
                LastName = "Natarajan",
                EmployeeID = "433461"
            };

            // Act
            var response = userController.UpdateUser(model);

            // Assert
            Assert.IsTrue(response is OkResult);
        }

        [Test]
        public void DeleteUserTest_Error()
        {
            IUserBL userBL = new MockUserData();

            // Arrange
            var userController = new UsersController(null);

            ProjectMangerModel.Users model = new ProjectMangerModel.Users
            {
                UserID = 1,
                FirstName = "Sathya",
                LastName = "Natarajan",
                EmployeeID = "433461"
            };

            // Act
            var response = userController.UpdateUser(model);

            // Assert
            Assert.IsTrue(response is InternalServerErrorResult);
        }
    }
}
