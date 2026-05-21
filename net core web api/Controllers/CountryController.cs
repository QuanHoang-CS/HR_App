using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using net_core_web_api.Data.Context;
using net_core_web_api.Models.Domain;
using net_core_web_api.Models.DTO;
using System.Diagnostics.Metrics;

namespace net_core_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly HRDbContext _dbContext;

        public CountryController(HRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // get data from Domain Models
            // map Domain Models to DTO
            // Reruen DTO back to the client
            var countries = _dbContext.Countries.ToList();
            var countryDto = new List<CountryDto>();
            foreach (var country in countries) 
            {
                countryDto.Add
                    (new CountryDto()
                        {
                            CountryId = country.CountryId,
                            CountryName = country.CountryName,
                            RegionId = country.RegionId,
                        }
                    );
             }

            return Ok(countryDto);
        }

        // GET: https://localhost:portnumber/api/Country/{id}
        // When we pass an id "{id}" after the urll: https://localhost:portnumber/api/Country/:, the inputed id will be mapped
        // to the input parameter of GetById()
        // Without [Route...] attribute, it will leads to error 500 since we have 2 [HttpGet] elements with the same route.
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            var country = _dbContext.Countries.FirstOrDefault(x => x.CountryId == id);
            CountryDto countryDto;
            if(country == null)
            {
                return NotFound($"Country with id \"{id}\" not found!");
            }
            else
            {
                countryDto = new CountryDto()
                {
                    CountryId = country.CountryId,
                    CountryName = country.CountryName,
                    RegionId = country.RegionId,
                };
            }
            
            return Ok(countryDto);
        }
        /*
        [HttpGet]
        public IActionResult GetByName(string countryname)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult GetByCode(string counttryCode)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult CreateCountry(string countryName, string countryCode, int countryId)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public IActionResult UpdateByName(string countryName, Countries newCountry)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public IActionResult UpdateById(int countryId, Countries newCountry)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public IActionResult UpdateByCode(string countryCodem, Countries newCountry)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public IActionResult DeleteCountryByName(string countryName)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public IActionResult DeleteCountryById(int countryId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public IActionResult DeleteCountryByCode(string countryCode)
        {
            throw new NotImplementedException();
        }
        */

    }
}
