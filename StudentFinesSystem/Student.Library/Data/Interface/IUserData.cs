using Student.Library.Models;

namespace Student.Library.Data.Interface
{
    public interface IUserData
    {
        User GetUserById(string Id);
        List<User> GetAllUser();
        void SaveUser(DBUser userModel);
        void UpdateUser(User userModel);
    }
}