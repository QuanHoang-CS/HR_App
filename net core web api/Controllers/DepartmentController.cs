using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_core_web_api.Data.Context;
using net_core_web_api.Models.Domain;
using Microsoft.AspNetCore.Http;

namespace net_core_web_api.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class DepartmentController : ControllerBase
    {
        private HRDbContext _dbcontext;

        public DepartmentController(HRDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] string? filterOn, int? filterQuery)
        {
            // Without AsQueryable(), the call to _dbcotext.Departments will return a variable of type DbSet<Department>
            // DbSet<T> implement the IEnumerable, if we use LINQ on deparments, it will return a IQueryable object, and require us
            // to cast back to the DbSet<Department> type
            var departments = _dbcontext.Departments.AsQueryable();

            if (!string.IsNullOrEmpty(filterOn) && !(filterQuery is null) && filterQuery.HasValue)
            {
                if (string.Equals(filterOn, "LocationId", StringComparison.OrdinalIgnoreCase))
                    departments = departments.Where(x => x.LocationId == filterQuery);
                else
                    return NotFound();
            }

            return Ok(departments);
        }

        [HttpGet("id")]
        public ActionResult GetDepartmentById(int id)
        {
            var department = _dbcontext.Departments.SingleOrDefault(x => x.DepartmentId == id);
            return Ok(department);
        }
    }
}
