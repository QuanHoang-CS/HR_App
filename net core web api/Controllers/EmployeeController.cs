using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using net_core_web_api.Data.Context;
using net_core_web_api.Models.DTO;

namespace net_core_web_api.Controllers
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


    }
}
