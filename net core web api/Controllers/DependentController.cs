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

    }
}
