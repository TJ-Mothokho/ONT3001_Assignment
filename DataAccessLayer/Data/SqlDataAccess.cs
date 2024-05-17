using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;


namespace DataAccessLayer.Data
{
    public class SqlDataAccess:ISqlDataAccess
    { 
        //The class SqlDataAccess implements the ISqlDataAccess interface and it has a constructor that takes in an IConfiguration object.
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        //Both methods create a new connection to the database.
        //The connection is disposed of after the method is executed.
        //The methods are defined as async so that they can be awaited when called.

        //The GetData method uses the QueryAsync method from the Dapper library to execute the stored procedure and return the result.
        public async Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connectionID = "DefaultConnection")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionID));

            //The parameters is the object that will be passed to the stored procedure.
            //The method returns a Task<IEnumerable<T>> object where T is the type of the object that will be returned.
            return await connection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
        }

        //The SaveData method uses the ExecuteAsync method from the Dapper library to execute the stored procedure and save the data.
        public async Task SaveData<T>(string spName, T parameters, string connectionID = "DefaultConnection")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionID));

            //The parameters is the object that will be passed to the stored procedure.
            //The method does not return anything.
            await connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
        }   

        




    }
}
