using Microsoft.AspNetCore.Mvc;
using net_core_web_api.Data.Context;

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
        public IActionResult GetAll()
        {
            var jobs = _dbContext.Jobs.ToList();
            return Ok(jobs);
        }

        
        [HttpGet("id")]
        public IActionResult GetJobById(int id)
        {
            var job = _dbContext.Jobs.FirstOrDefault(x => x.JobId == id);
            return Ok(job);
        }
    }
}