using System.Collections.ObjectModel;

namespace ProjectManager.InterfaceLayer
{
    public interface IUserBL
    {
        Collection<CommonEntities.Users> GetUsers();
        void AddUser(CommonEntities.Users user);
        CommonEntities.Users GetUserById(int userId);
        void UpdateUser(CommonEntities.Users user);
        void DeleteUser(CommonEntities.Users user);
    }
}
