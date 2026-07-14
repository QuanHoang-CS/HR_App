using Microsoft.AspNetCore.Mvc;
using net_core_web_api.Data.Context;
using net_core_web_api.Models.DTO;
using net_core_web_api.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddJobRequestDto job)
        {
            var newJob = new Job
            {
                JobTitle = job.JobTitle,
                MinSalary = job.MinSalary,
                MaxSalary = job.MaxSalary,
            };

            try
            {
                await _dbContext.Jobs.AddAsync(newJob);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var rootEx = ex.InnerException;
                while(rootEx != null) 
                    rootEx = rootEx.InnerException;

                return BadRequest($"Root error is: {rootEx.Message}");
            }

            var jobDto = new JobDto
            {
                JobId = newJob.JobId,
                JobTitle = newJob.JobTitle,
                MinSalary = newJob.MinSalary,
                MaxSalary = newJob.MaxSalary,
            };
            return CreatedAtAction(nameof(GetById), new { id = newJob.JobId }, jobDto);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteById([FromQuery] int id)
        {
            var deletedJob = await _dbContext.Jobs.FirstOrDefaultAsync(j => j.JobId == id);

            if(deletedJob == null)
            {
                return NotFound($"No job with id: \'{id}\' found.");
            }

            try
            {
                _dbContext.Jobs.Remove(deletedJob);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                var rootEx = ex.InnerException;

                while(rootEx != null && rootEx.InnerException != null)
                    rootEx = rootEx.InnerException;

                return BadRequest($"The root exception is: {rootEx.Message}");
            }

            JobDto jobDto = new JobDto
            {
                JobId = deletedJob.JobId,
                JobTitle = deletedJob.JobTitle,
                MinSalary = deletedJob.MinSalary,
                MaxSalary = deletedJob.MaxSalary,
            };

            return Ok(jobDto);
        }
    }
}