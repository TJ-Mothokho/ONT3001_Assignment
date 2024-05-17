using DataAccessLayer.Data;
using DataAccessLayer.Models.Domain;

namespace DataAccessLayer.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {

        //The class DepartmentRepository implements the IDepartmentRepository interface and it has a constructor that takes in an ISqlDataAccess object.
        private readonly ISqlDataAccess _db;
        public DepartmentRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        //The AddAsync method takes an Department object as a parameter and inserts the data into the database using the spInsertDepartment stored procedure.
        //The method returns a boolean that indicates whether the operation was successful (true) or not (false).
        public async Task<bool> AddAsync(Department department)
        {
            try
            {
                await _db.SaveData("spInsertDepartment", new { department.Name });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //The GetAllAsync method retrieves all the departments from the database using the spGetAllDepartments stored procedure.
        //The method returns a list of departments (as an IEnumerable).
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _db.GetData<Department, dynamic>("spGetAllDepartments", new { });
        }

        //The GetByIdAsync method retrieves an department with a specific ID from the database using the spGetDepartmentById stored procedure.
        //The method returns an Department object that contains the department details with the specified ID.
        public async Task<Department> GetByIdAsync(int departmentID)
        {
            var department = await _db.GetData<Department, dynamic>("spGetDepartmentById", new { departmentID });
            return department.FirstOrDefault();
        }

        //The UpdateAsync method updates an department's information in the database using the spUpdateDepartment stored procedure.
        //The method returns a boolean that indicates whether the operation was successful (true) or not (false).
        public async Task<bool> UpdateAsync(Department department)
        {
            try
            {
                await _db.SaveData("spUpdateDepartment", new { department.DepartmentID, department.Name });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //The DeleteAsync method deletes an Department from the database using the spDeleteDepartment stored procedure.
        //The method returns a boolean that indicates whether the operation was successful (true) or not (false).
        public async Task<bool> DeleteAsync(int departmentID)
        {
            try
            {
                await _db.SaveData("spDeleteDepartment", new { departmentID });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
