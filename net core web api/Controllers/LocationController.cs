

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_core_web_api.Data;
using net_core_web_api.Data.Context;
using net_core_web_api.Models;

namespace net_core_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LocationController : ControllerBase
    {
        private HRDbContext _dbContext;

        public LocationController(HRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var locations = await _dbContext.Locations.ToListAsync();

            return Ok(locations);
        }
    }
}
