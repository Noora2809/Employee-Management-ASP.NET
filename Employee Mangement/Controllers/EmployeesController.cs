using ASP.NET_CRUD_Operation_Project.Data;
using ASP.NET_CRUD_Operation_Project.Models;
using ASP.NET_CRUD_Operation_Project.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ASP.NET_CRUD_Operation_Project.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeManagementDbContext employeeDbcontext;
        public EmployeesController(EmployeeManagementDbContext EmployeeDbcontext)
        {
            this.employeeDbcontext = EmployeeDbcontext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await employeeDbcontext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department,
                Salary = addEmployeeRequest.Salary,
                DateOfJoining = addEmployeeRequest.DateOfJoining
            };
            await employeeDbcontext.Employees.AddAsync(employee);
            await employeeDbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await employeeDbcontext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = employee.Name,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department,
                    Salary = employee.Salary,
                    DateOfJoining = employee.DateOfJoining
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return Redirect("Index");
            
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await employeeDbcontext.Employees.FindAsync(model.Id);
            if (employee != null) 
            {
                employee.Name = model.Name;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;
                employee.Salary = model.Salary;
                employee.DateOfJoining = model.DateOfJoining;

                await employeeDbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await employeeDbcontext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employeeDbcontext.Employees.Remove(employee);
                await employeeDbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}