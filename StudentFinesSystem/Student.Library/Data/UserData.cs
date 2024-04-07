using Student.Library.Data.Interface;
using Student.Library.Internal.DataAccess;
using Student.Library.Models;

namespace Student.Library.Data
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _dataAccess;
        public UserData(string connectionString)
        {
            _dataAccess = new SqlDataAccess(connectionString);
        }

        public User GetUserById(string Id)
        {
            var result = _dataAccess.LoadData<User, dynamic>(
                "dbo.spUser_Get",
                new { Id = Id });
            return result.FirstOrDefault();
        }

        public List<User> GetAllUser()
        {
            var result = _dataAccess.LoadData<User, dynamic>(
                "dbo.spUser_GetAll", null);
            return result;
        }

        public void SaveUser(DBUser userModel) =>
            _dataAccess.SaveData("dbo.spUser_Insert", userModel);

        public void UpdateUser(User userModel) =>
            _dataAccess.SaveData("dbo.spUser_Update", userModel);
    }
}
