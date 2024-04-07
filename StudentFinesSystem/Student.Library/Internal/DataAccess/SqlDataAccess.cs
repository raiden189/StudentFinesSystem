using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Student.Library.Internal.DataAccess
{
    internal class SqlDataAccess : ISqlDataAccess
    {
        private readonly string _connectionString;
        public SqlDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> LoadData<T, U>(string query, U parameters, string connectionName = "Default")
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                List<T> rows = con.Query<T>(query, parameters,
                    commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }

        public void SaveData<T>(string query, T parameters, string connectionName = "Default")
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
