using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using net_core_web_api.Data.Context;
using net_core_web_api.Models.Domain;
using net_core_web_api.Models.DTO;

namespace net_core_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DependentController : ControllerBase
    {
        private HRDbContext _dbContext;

        public DependentController(HRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dependents = await _dbContext.Dependents.ToListAsync();
            var dependentDtoList = new List<DependentDto>();

            if(dependents == null)
            {
                return Ok("No dependents added");
            }
            foreach (var dependent in dependents)
            {
                dependentDtoList.Add(new DependentDto()
                {
                    Id = dependent.Id,
                    FirstName = dependent.FirstName,
                    LastName = dependent.LastName,
                    Relationship = dependent.Relationship,
                    EmployeeId = dependent.EmployeeId,
                });
            }

            return Ok(dependentDtoList);
        }

        [HttpGet("dependentId")]
        public async Task<IActionResult> GetByDependentId([FromQuery] int id)
        {
            var dependent = await _dbContext.Dependents.FirstOrDefaultAsync(d => d.Id == id);

            if(dependent == null)
            {
                return NotFound($"No dependent with id: \'{id}\'");
            }

            var dependentDto = new DependentDto
            {
                Id = dependent.Id,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                Relationship = dependent.Relationship,
                EmployeeId = dependent.EmployeeId,
            };

            return Ok(dependentDto);
        }

        [HttpGet("employeeId")]
        public async Task<IActionResult> GetByEmployeeId(int id)
        {
            // Should I use async filter or synchronous filter here?
            // Af I keep implementing, I gotta check if dependents yield any result, so I have to execute it with Any() or
            // ToListAsync() anyway. So let's just do it now below
            // var dependents = _dbContext.Dependents.Where(d => d.EmployeeId == id);
            var dependents = await _dbContext.Dependents.Where(d => d.EmployeeId == id).ToListAsync();
            var dependentDtoList = new List<DependentDto>();

            if(dependents == null)
            {
                return NotFound($"Employee with id: \'{id}\' is either not found, or have no dependents");
            }

            foreach(var dependent in dependents)
            {
                dependentDtoList.Add(new DependentDto
                {
                    Id = dependent.Id,
                    FirstName = dependent.FirstName,
                    LastName = dependent.LastName,
                    Relationship = dependent.Relationship,
                    EmployeeId = dependent.EmployeeId,
                });
            }

            return Ok(dependentDtoList);
        }

        [HttpDelete("dependentId")]
        public async Task<IActionResult> deleteByDependentId([FromQuery] int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("EmployeeId")]
        public async Task<IActionResult> deleteByEmployeeId([FromQuery] int id)
        {
            throw new NotImplementedException();
        }


    }
}
