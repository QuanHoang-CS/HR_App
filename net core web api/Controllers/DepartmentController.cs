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

        // The [FromBody] attribute can be ommitted in this case, since the class DepartmentController has the [ApiController] attribute
        // which makes all complex type parameter (such as AddDepartmentRequestDto in this case) be taken from request's body
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] AddDepartmentRequestDto newDepartment)
        {
            // Create a new DepartmentDomainModel and add it to the database
            // Remember to catch error
            // Create a DepartmentDto from the new Department and return it for result viewing
            var departmentDomainModel = new Department
            {
               // DepartmentId = newDepartment.DepartmentId,
                DepartmentName = newDepartment.DepartmentName,
                LocationId = newDepartment.LocationId,

            };

            try
            {
                await _dbcontext.AddAsync(departmentDomainModel);
                await _dbcontext.SaveChangesAsync();     // Missing await here lead to CS4014 error. The write to the db doesn't happen at all
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
            // Note that departmentDomainModel doesn't receive an id from the requester,
            // but after a new entry is created in the db, the db generate the id for the new department (since department key is auto incremented)
            // and EF core write this back to our departmentDomainModel (which is of type Department)
            var departmentDto = new DepartmentDto
            {
                DepartmentId = departmentDomainModel.DepartmentId,
                DepartmentName = departmentDomainModel.DepartmentName,
                LocationId = departmentDomainModel.LocationId,
            };

            return CreatedAtAction(nameof(GetDepartmentById), new { id = departmentDto.DepartmentId }, departmentDto);
        }
    }
}
