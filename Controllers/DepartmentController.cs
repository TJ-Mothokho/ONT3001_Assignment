using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Repository;
using DataAccessLayer.Models.Domain;

namespace ONT3001_Assignment.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        // Constructor that takes in an instance of IDepartmentRepository
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        // Returns a view of all departments
        public async Task<IActionResult> Index()
        {
            return View(await _departmentRepository.GetAllAsync());
        }

        // Returns the create view
        public async Task<IActionResult> Create()
        {
            TempData["Message"] = null; //reset the message
            return View();
        }

        // POST: Department/Create
        //Creates a new department
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //The method returns a boolean that indicates whether the operation was successful(true) or not(false)
                    //then add an appropriate message to the TempData.
                    bool result = await _departmentRepository.AddAsync(department);
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
            return View(department);
        }


        // Returns the edit view
        public async Task<IActionResult> Edit(int id)
        {
            TempData["Message"] = null; //reset the message

            //Check if the id is not null and if the department exists in the database
            if (id != null)
            {
                var department = await _departmentRepository.GetByIdAsync(id);
                if (department is not null)
                {
                    return View(department);
                }
            }
            return View();
        }

        // POST: Department/Edit
        //Updates an existing department
        [HttpPost]
        public async Task<IActionResult> Edit(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //The method returns a boolean that indicates whether the operation was successful(true) or not(false)
                    //then add an appropriate message to the TempData.
                    bool result = await _departmentRepository.UpdateAsync(department);
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
                return View();
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Contact IT Department with the ERROR MESSAGE: \n{ex.Message}";
            }
            return View(department);
        }


        // Returns the delete view
        public async Task<IActionResult> Delete(int id)
        {
            TempData["Message"] = null; //reset the message

            if (id != null)
            {
                var department = await _departmentRepository.GetByIdAsync(id);
                if (department is not null)
                {
                    return View(department);
                }
            }
            return View();
        }

        // POST: Department/Delete
        //Deletes an existing department
        [HttpPost]
        public async Task<IActionResult> Delete(Department department)
        {
            try
            {
                //The method returns a boolean that indicates whether the operation was successful(true) or not(false)
                //then add an appropriate message to the TempData.
                bool result = await _departmentRepository.DeleteAsync(department.DepartmentID);
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
                TempData["Message"] = $"Contact IT Department with the ERROR MESSAGE: \n{ex.Message}";
            }
            return View(department);
        }
    }
}
