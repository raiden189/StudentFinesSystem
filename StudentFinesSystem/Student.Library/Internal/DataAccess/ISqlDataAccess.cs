
namespace Student.Library.Internal.DataAccess
{
    public interface ISqlDataAccess
    {
        List<T> LoadData<T, U>(string query, U parameters, string connectionName = "Default");
        void SaveData<T>(string query, T parameters, string connectionName = "Default");
    }
}