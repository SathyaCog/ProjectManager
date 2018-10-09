using System.Collections.ObjectModel;

namespace ProjectManager.InterfaceLayer
{
    public interface IUserBL
    {
        Collection<CommonEntities.Users> GetUsers();
        int AddUser(CommonEntities.Users user);
        CommonEntities.Users GetUserById(int userId);
        int UpdateUser(CommonEntities.Users user);

    }
}
