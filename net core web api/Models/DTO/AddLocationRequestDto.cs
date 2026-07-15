namespace net_core_web_api.Models.DTO
{
    public class AddLocationRequestDto
    {
        public string? StreetAddress { get; set; }
        public string? PostalCode { get; set; }
        public string City { get; set; }
        public string? StateProvince { get; set; }
        public string CountryId { get; set; }
    }
}
