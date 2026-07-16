using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace MyApp.API.Models.Domain
{
    public class Location
    {
        public int LocationId { get; set; }
        public string? StreetAddress { get; set; }
        public string? PostalCode { get; set; }
        public string City { get; set; }
        public string? StateProvince { get; set; }
        public string CountryId { get; set; }
    }
}
