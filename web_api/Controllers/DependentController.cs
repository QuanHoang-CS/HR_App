using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.API.Data.Context;
using MyApp.API.Models.Domain;
using MyApp.API.Models.DTO;

namespace MyApp.API.Controllers
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


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddDependentRequestDto dependent)
        {
            var newDependent = new Dependent
            {
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                Relationship = dependent.Relationship,
                EmployeeId = dependent.EmployeeId,
            };

            try
            {
                await _dbContext.Dependents.AddAsync(newDependent);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var rootEx = ex.InnerException;
                while (rootEx != null && rootEx.InnerException != null)
                    rootEx = rootEx.InnerException;

                return BadRequest($"Root error is: {rootEx.Message}");
            }

            var dependentDto = new DependentDto
            {
                Id = newDependent.Id,
                FirstName = newDependent.FirstName,
                LastName = newDependent.LastName,
                Relationship = newDependent.Relationship,
                EmployeeId = newDependent.EmployeeId,
            };

            return CreatedAtAction(nameof(GetByDependentId), new {id = newDependent.Id}, dependentDto);
        }


        [HttpDelete("dependentId")]
        public async Task<IActionResult> deleteByDependentId([FromQuery] int id)
        {
            var deletedDependent = await _dbContext.Dependents.FirstOrDefaultAsync(d => d.Id == id);

            if(deletedDependent == null)
            {
                return NotFound($"No dependent with id: \'{id}\' found");
            }

            try
            {
                _dbContext.Dependents.Remove(deletedDependent);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                var rootEx = ex.InnerException;
                if(rootEx != null &&  rootEx.InnerException != null)
                    rootEx = rootEx.InnerException;

                return BadRequest($"The root error is: {rootEx.Message}");
            }

            var deletedDependentDto = new DependentDto
            {
                Id = deletedDependent.Id,
                FirstName = deletedDependent.FirstName,
                LastName = deletedDependent.LastName,
                Relationship = deletedDependent.Relationship,
                EmployeeId = deletedDependent.EmployeeId,
            };

            return Ok(deletedDependentDto);
        }


        [HttpDelete("employeeId")]
        public async Task<IActionResult> deleteByEmployeeId([FromQuery] int id)
        {
            var deletedDependent = await _dbContext.Dependents.FirstOrDefaultAsync(d => d.Id == id);

            if(deletedDependent == null)
            {
                return NotFound($"No dependet with employee id: \'{id}\' found");
            }

            try
            {
                _dbContext.Dependents.Remove(deletedDependent);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                var rootEx = ex.InnerException;
                while(rootEx != null && rootEx.InnerException != null)
                    rootEx = rootEx.InnerException;

                return BadRequest($"Root error is: {rootEx.Message}");
            }

            var deletedDependentDto = new DependentDto
            {
                Id = deletedDependent.Id,
                FirstName = deletedDependent.FirstName,
                LastName = deletedDependent.LastName,
                Relationship = deletedDependent.Relationship,
                EmployeeId = deletedDependent.EmployeeId,
            };

            return Ok(deletedDependentDto);
        }


    }
}
