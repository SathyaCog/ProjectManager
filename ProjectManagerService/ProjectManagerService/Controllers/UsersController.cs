using ProjectManager.BusinessLayer;
using ProjectManager.InterfaceLayer;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Http;
using CommonEntities = ProjectManager.CommonEntities;
using ProjectMangerModel = ProjectManagerService.Models;

namespace ProjectManagerService.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private readonly IUserBL _userBL = null;

        public UsersController()
        {
            _userBL = new UserBL();
        }

        public UsersController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        [HttpGet]
        [Route("GetUsers")]
        public IHttpActionResult GetUsers()
        {
            Collection<ProjectMangerModel.Users> users = new Collection<ProjectMangerModel.Users>();

            var blProjects = _userBL.GetUsers();
            blProjects.ToList()
                .ForEach(ur => users.Add(
                   new ProjectMangerModel.Users
                   {
                       UserID = ur.UserID,
                       FirstName = ur.FirstName,
                       LastName = ur.LastName,
                       EmployeeID = ur.EmployeeID
                   }));

            return Ok(users);
        }

        [HttpPost]
        [Route("AddUser")]
        public IHttpActionResult AddUser([FromBody]ProjectMangerModel.Users user)
        {
            try
            {
                CommonEntities.Users usr = new CommonEntities.Users
                {
                    UserID = user.UserID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmployeeID = user.EmployeeID
                };

                var result = _userBL.AddUser(usr);
                if (result != -1)
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetUserById/{userId}")]
        public IHttpActionResult GetUserById(int userId)
        {
            ProjectMangerModel.Users user = null;

            var blUser = _userBL.GetUserById(userId);
            if (blUser == null)
            {
                return NotFound();
            }
            user = new ProjectMangerModel.Users
            {
                UserID = blUser.UserID,
                FirstName = blUser.FirstName,
                LastName = blUser.LastName,
                EmployeeID = blUser.EmployeeID
            };

            return Ok(user);
        }

        [HttpPost]
        [Route("UpdateUser")]
        public IHttpActionResult UpdateUser([FromBody]ProjectMangerModel.Users user)
        {
            try
            {
                CommonEntities.Users usr = new CommonEntities.Users
                {
                    UserID = user.UserID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmployeeID = user.EmployeeID
                };

                var result = _userBL.UpdateUser(usr);
                if (result != -1)
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
