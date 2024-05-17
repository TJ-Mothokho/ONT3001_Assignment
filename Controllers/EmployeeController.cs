using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Repository;
using DataAccessLayer.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ONT3001_Assignment.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        // Constructor that takes in an instance of IEmployeeRepository and IDepartmentRepository
        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            //We are going to use the departmentRepository to get the list of departments for the dropdown.
            _departmentRepository = departmentRepository;
        }

        //get the list of employees
        public async Task<IActionResult> Index()
        {
            return View(await _employeeRepository.GetAllAsync());
        }

        // Returns the create view
        public async Task<IActionResult> Create()
        {
            //The GetDepaermentDropDownList method is called to get the list of departments for the dropdown.
            ViewBag.DepartmentList = await GetDepartmentDropDownList();
            return View();
        }

        // POST: Employee/Create
        //Creates a new employee
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //The method returns a boolean that indicates whether the operation was successful(true) or not(false)
                    //then add an appropriate message to the TempData.
                    bool result = await _employeeRepository.AddAsync(employee);
                    if (result)
                    {
                        TempData["Message"] = "Success";
                    }
                    else
                    {
                        TempData["Message"] = "Failed";
                        //If the operation was not successful, the user is redirected to the create view
                        return RedirectToAction("Index");
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Contact IT Department with the ERROR MESSAGE: \n{ex.Message}";
            }
            return View(employee);
        }

        // Returns the edit view
        public async Task<IActionResult> Edit(int id)
        {
            //The GetDepaermentDropDownList method is called to get the list of departments for the dropdown.
            ViewBag.DepartmentList = await GetDepartmentDropDownList();

            TempData["Message"] = null; //reset the message

            //Check if the id is not null and if the employee exists in the database
            if (id != null)
            {
                //Check if the employee exists in the database
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee is not null)
                {
                    return View(employee);
                }
            }
            return View();
        }

        // POST: Employee/Edit
        //Updates an existing employee
        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //The method returns a boolean that indicates whether the operation was successful(true) or not(false)
                    //then add an appropriate message to the TempData.
                    bool result = await _employeeRepository.UpdateAsync(employee);
                    if (result)
                    {
                        TempData["Message"] = "Success";
                    }
                    else
                    {
                        TempData["Message"] = "Failed";
                    }
                    return View();
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Contact IT Employee with the ERROR MESSAGE: \n{ex.Message}";
            }
            return View(employee);
        }

        // Returns the delete view
        public async Task<IActionResult> Delete(int id)
        {
            if (id != null)
            {
                //Checks if the id is not null and if the employee exists in the database
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee is not null)
                {
                    return View(employee);
                }
            }
            return View();
        }

        // POST: Employee/Delete
        //Deletes an existing employee
        [HttpPost]
        public async Task<IActionResult> Delete(Employee employee)
        {
            try
            {
                //The method returns a boolean that indicates whether the operation was successful(true) or not(false)
                //then add an appropriate message to the TempData.
                bool result = await _employeeRepository.DeleteAsync(employee.EmployeeID);
                if (result)
                {
                    TempData["Message"] = "Success";
                }
                else
                {
                    TempData["Message"] = "Failed";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Contact IT Employee with the ERROR MESSAGE: \n{ex.Message}";
            }
            return View(employee);
        }

        //The GetDepartmentDropDownList method is a private method that returns a list of SelectListItems.
        //The SelectListItems are used to populate the dropdown list. The method retrieves the departments from the database
        //and creates a SelectListItem for each department.
        private async Task<IEnumerable<SelectListItem>> GetDepartmentDropDownList()
        {
            var departments = await _departmentRepository.GetAllAsync();

            //The SelectListItem contains the DepartmentID as the value and the department name as the text.
            //The method returns the list of SelectListItem.
            return departments.Select(department => new SelectListItem { Value = department.DepartmentID.ToString(), Text = department.Name });
        }
    }
}
