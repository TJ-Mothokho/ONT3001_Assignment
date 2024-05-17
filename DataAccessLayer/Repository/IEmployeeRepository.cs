using DataAccessLayer.Models.Domain;

namespace DataAccessLayer.Repository
{
    public interface IEmployeeRepository
    {
        //AddAsync method is used to add a new employee to the database
        Task<bool> AddAsync(Employee employee);

        //UpdateAsync method is used to update an existing employee in the database
        Task<bool> UpdateAsync(Employee employee);

        //GetByIdAsync method is used to get an employee by its EmployeeID
        Task<Employee> GetByIdAsync(int EmployeeID);

        //GetAllAsync method is used to get all employees from the database
        Task<IEnumerable<dynamic>> GetAllAsync();

        //DeleteAsync method is used to delete an employee by its EmployeeID
        Task<bool> DeleteAsync(int EmployeeID);
    }
}
