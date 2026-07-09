using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_core_web_api.Data.Context;
using net_core_web_api.Models.Domain;
using Microsoft.AspNetCore.Http;
using net_core_web_api.Models.DTO;

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

        // TODO: implement filtering in the future buddy ~.~
        [HttpGet("id")]
        public ActionResult GetDepartmentById(int id)
        {
            var department = _dbcontext.Departments.SingleOrDefault(x => x.DepartmentId == id);
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(AddDepartmentRequestDto newDepartment)
        {
            // Create a new DepartmentDomainModel and add it to the database
            // Remember to catch error
            // Create a DepartmentDto from the new Department and return it for result viewing
            var departmentDomainModel = new Department
            {
                DepartmentId = newDepartment.DepartmentId,
                DepartmentName = newDepartment.DepartmentName,
                LocationId = newDepartment.LocationId,

            };

            try
            {
                await _dbcontext.AddAsync(departmentDomainModel);
                _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Gotta read more about these exceptions
                // rootException
                var rootException = ex.InnerException;
                while (rootException.InnerException != null)
                    rootException = rootException.InnerException;

                Console.WriteLine($"The actual error is: {rootException.Message}");
                //return BadRequest($"Update country with id {id} failed!");
                return BadRequest($"The actual error is: {rootException.Message}");
            }

            // Create new Dto and pass back for view
            var departmentDto = new DepartmentDto
            {
                DepartmentId = departmentDomainModel.DepartmentId,
                DepartmentName = departmentDomainModel.DepartmentName,
                LocationId = departmentDomainModel.LocationId,
            };

            return CreatedAtAction(nameof(GetDepartmentById), new { id = departmentDomainModel.DepartmentId }, departmentDto);
        }
    }
}
