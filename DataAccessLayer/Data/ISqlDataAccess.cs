using System;

namespace DataAccessLayer.Data
{
    public interface ISqlDataAccess
    {
        //The GetData method is responsible for retrieving data from the database
        Task<IEnumerable<T>> GetData<T, P>(String spName, P parameters, string connectectionId = "DefaultConnection");

        //The SaveData method is responsible for saving data to the database
        Task SaveData<T>(string spName, T parameters, string connectectionId = "DefaultConnection");
      
    }
}
