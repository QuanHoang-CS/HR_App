namespace MyApp.API.Models.DTO
{
    public class AddCountryRequestDto
    {
        public string CountryId {  get; set; }
        public string CountryName { get; set; }
        public int RegionId { get; set; }
    }
}
