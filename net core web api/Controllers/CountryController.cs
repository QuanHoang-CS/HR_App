using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        */

        // POST: Create new Country
        // POST: https://localhost:portnumber/api/country
        [HttpPost]
        public IActionResult CreateCountry([FromBody] AddCountryRequestDto newCountryDto)
        {
            var countryDomainModel = new Country
            {
                CountryId = newCountryDto.CountryId,
                CountryName = newCountryDto.CountryName,
                RegionId = newCountryDto.RegionId
            };

            _dbContext.Countries.Add(countryDomainModel);
            try
            {
                _dbContext.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                // rootException
                var rootException = ex.InnerException;
                while (rootException.InnerException != null)
                    rootException = rootException.InnerException;

                Console.WriteLine($"The actual error is: {rootException.Message}");
                //return BadRequest($"Update country with id {id} failed!");
                return BadRequest($"The actual error is: {rootException.Message}");
            }
            

            var countryDto = new CountryDto
            {
                CountryId = countryDomainModel.CountryId,
                CountryName = countryDomainModel.CountryName,
                RegionId = countryDomainModel.RegionId
            };

            return CreatedAtAction(nameof(GetById), new {id = countryDomainModel.CountryId}, countryDto);  //
        }

        [HttpPatch("id")]
        public IActionResult UpdateSearchById(string id, [FromBody] UpdateCountryRequestDto updateCountryDto)
        {
            var countryDomainModel = _dbContext.Countries.FirstOrDefault(x => x.CountryId == id);
            /*
            var newId = updateCountryDto.CountryId;

            // Check if cliet want to update id of a record.
            // In such case, check if the new id has length less than 3, and the new id is unique
            // Exit early if new id is not valid.
            // This part is not meaningful as I cannot change the primary ket id here
            if( !string.Equals(id, newId, StringComparison.OrdinalIgnoreCase) )
            {
                if(newId.Length > 2)            // Invalid length
                    return BadRequest("Invalid country id!!\n Country id must has less than 3 characters");

                var countryWithDupId = _dbContext.Countries.FirstOrDefault(x => x.CountryId == newId);

                if(countryWithDupId != null)    // New id not unique
                    return BadRequest("Invalid country id!!\n Country id must be unique");
                
            }*/

            // If we get to here, either client not want to update id, or new id is valid.
            countryDomainModel.RegionId = updateCountryDto.RegionId;
            countryDomainModel.CountryName = updateCountryDto.CountryName; 
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // rootException
                var rootException = ex.InnerException;
                while (rootException.InnerException != null)
                    rootException = rootException.InnerException;

                Console.WriteLine($"The actual error is: {rootException.Message}");
                //return BadRequest($"Update country with id {id} failed!");
                return BadRequest($"The actual error is: {rootException.Message}");
            }
            

            var countryDto = new CountryDto
            {
                CountryId = countryDomainModel.CountryId,
                CountryName = countryDomainModel.CountryName,
                RegionId = countryDomainModel.RegionId
            };
            return CreatedAtAction(nameof(GetById), new { id = countryDomainModel.CountryId }, countryDto);
        }
        /*
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
