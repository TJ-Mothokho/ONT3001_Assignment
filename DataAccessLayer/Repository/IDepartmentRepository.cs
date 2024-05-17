using DataAccessLayer.Models.Domain;

namespace DataAccessLayer.Repository
{
    public interface IDepartmentRepository
    {
        // AddAsync method is used to add a new department to the database
        Task<bool> AddAsync(Department department);

        // UpdateAsync method is used to update the department details in the database
        Task<bool> UpdateAsync(Department department);

        // GetByIdAsync method is used to get the department details by DepartmentID
        Task<Department> GetByIdAsync(int DepartmentID);

        // GetAllAsync method is used to get all the departments
        Task<IEnumerable<Department>> GetAllAsync();

        // DeleteAsync method is used to delete the department by DepartmentID
        Task<bool> DeleteAsync(int DepartmentID);
    }
}
