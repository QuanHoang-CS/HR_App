using Microsoft.AspNetCore.Mvc;
using net_core_web_api.Data.Context;
using net_core_web_api.Models.DTO;
using net_core_web_api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace net_core_web_api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private HRDbContext _dbContext;

        public JobController(HRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobs = _dbContext.Jobs.ToList();

            var jobDto = new List<JobDto>();

            foreach (var job in jobs) 
            {
                jobDto.Add
                (
                    new JobDto()
                    {
                        JobId = job.JobId,
                        JobTitle = job.JobTitle,
                        MaxSalary = job.MaxSalary,
                        MinSalary = job.MinSalary,
                    }
                );
            }
            return Ok(jobDto);
        }

        
        [HttpGet("id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var job = await _dbContext.Jobs.FirstOrDefaultAsync(x => x.JobId == id);
           
            if(job == null)
            {
                return NotFound($"Job with Id \"{id}\" not found !");
            }

            var jobDto = new JobDto()
            {
                JobId = job.JobId,
                JobTitle = job.JobTitle,
                MaxSalary = job.MaxSalary,
                MinSalary = job.MinSalary,
            };
                     
            return Ok(jobDto);
        }

    }
}