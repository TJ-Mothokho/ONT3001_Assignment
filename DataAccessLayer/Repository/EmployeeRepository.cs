using DataAccessLayer.Models.Domain;
using DataAccessLayer.Data;


namespace DataAccessLayer.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        //The class EmployeeRepository implements the IEmployeeRepository interface and it has a constructor that takes in an ISqlDataAccess object.
        private readonly ISqlDataAccess _db;

        public EmployeeRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        //The AddAsync method takes an Employee object as a parameter and inserts the data into the database using the spInsertEmployee stored procedure.
        //The method returns a boolean that indicates whether the operation was successful (true) or not (false).
        public async Task<bool> AddAsync(Employee employee)
        {
            try
            {
                await _db.SaveData("spInsertEmployee", new { employee.FirstName, employee.LastName, employee.DepartmentID });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //The GetAllAsync method retrieves all the employees from the database using the spGetAllEmployees stored procedure.
        //The method returns a list of employees (as an IEnumerable).
        public async Task<IEnumerable<dynamic>> GetAllAsync()
        {
            return await _db.GetData<dynamic, dynamic>("spGetAllEmployees", new { });
        }

        //The GetByIdAsync method retrieves an employee with a specific ID from the database using the spGetEmployeeById stored procedure.
        //The method returns an Employee object that contains the employee details with the specified ID.
        public async Task<Employee> GetByIdAsync(int EmployeeID)
        {
            var employee = await _db.GetData<Employee, dynamic>("spGetEmployeeById", new { EmployeeID });
            return employee.FirstOrDefault();
        }
        //The UpdateAsync method updates an employee's information in the database using the spUpdateEmployee stored procedure.
        //The method returns a boolean that indicates whether the operation was successful (true) or not (false).
        public async Task<bool> UpdateAsync(Employee employee)
        {
            try
            {
                await _db.SaveData("spUpdateEmployee", new { employee.EmployeeID, employee.FirstName, employee.LastName, employee.DepartmentID });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //The DeleteAsync method deletes an employee from the database using the spDeleteEmployee stored procedure.
        //The method returns a boolean that indicates whether the operation was successful (true) or not (false).
        public async Task<bool> DeleteAsync(int employeeID)
        {
            try
            {
                await _db.SaveData("spDeleteEmployee", new { employeeID });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
