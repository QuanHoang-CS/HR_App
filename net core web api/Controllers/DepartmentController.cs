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
        public ActionResult GetAll()
        {
            var departments = _dbcontext.Departments.ToList();

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
