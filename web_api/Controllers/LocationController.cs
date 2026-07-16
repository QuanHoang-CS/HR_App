

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.API.Data;
using MyApp.API.Data.Context;
using MyApp.API.Models;

namespace MyApp.API.Controllers
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
