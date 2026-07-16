using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using MyApp.API.Data.Context;
using MyApp.API.Models.Domain;
using MyApp.API.Models.DTO;

namespace MyApp.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private HRDbContext _dbContext;

        public EmployeeController(HRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _dbContext.Employees.ToListAsync();
            var employeesDto = new List<EmployeeDto>();
            
            foreach (var employee in employees)
            {
                employeesDto.Add(new EmployeeDto
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HireDate = employee.HireDate,
                    JobId = employee.JobId,
                    Salary = employee.Salary,
                    ManagerId = employee.ManagerId,
                    DepartmentId = employee.DepartmentId,
                });
            }
            return Ok(employeesDto);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmployeeId == id);
            
            if(employee == null)
            {
                return NotFound($"No employee with given id: \'{id}\'");
            }

            var EmployeeDto = new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HireDate = employee.HireDate,
                JobId = employee.JobId,
                Salary = employee.Salary,
                ManagerId = employee.ManagerId,
                DepartmentId = employee.DepartmentId,
            };

            return Ok(EmployeeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddEmployeeRequestDto employee)
        {
            Employee newEmployee = new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HireDate = employee.HireDate,
                JobId = employee.JobId,
                Salary = employee.Salary,
                ManagerId = employee.ManagerId,
                DepartmentId = employee.DepartmentId,
            };

            try
            {
                await _dbContext.Employees.AddAsync(newEmployee);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                var rootEx = ex.InnerException;
                while(rootEx.InnerException != null)
                    rootEx = rootEx.InnerException;
                
                //return BadRequest(ex);
                return BadRequest($"The root error is: {rootEx.Message}");
            }

            EmployeeDto employeeDto = new EmployeeDto
            {
                EmployeeId = newEmployee.EmployeeId,
                FirstName = newEmployee.FirstName,
                LastName = newEmployee.LastName,
                Email = newEmployee.Email,
                PhoneNumber = newEmployee.PhoneNumber,
                HireDate = newEmployee.HireDate,
                JobId = newEmployee.JobId,
                Salary = newEmployee.Salary,
                ManagerId = newEmployee.ManagerId,
                DepartmentId = newEmployee.DepartmentId,
            };
            return CreatedAtAction(nameof(GetById), new { newEmployee.EmployeeId }, employeeDto);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch(DbUpdateException ex)
                {
                    var rootEx = ex.InnerException;
                    while (rootEx.InnerException != null)
                        rootEx = rootEx.InnerException;

                    Console.WriteLine($"The actual error is: {rootEx.Message}");
                    return BadRequest($"The actual error is: {rootEx.Message}");
                }
            }
            else
            {
                return NotFound($"No employee with given id: \'{id}\'");
            }

            return Ok(employee);
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteByFilters([FromBody] string? startDate, string? endDate, string? phoneNumber, string? email, string? hireDate, string? manager, string? department)
        {
            throw new NotImplementedException();
        }
    }
}
