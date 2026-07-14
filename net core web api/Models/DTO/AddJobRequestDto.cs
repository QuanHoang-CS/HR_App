namespace net_core_web_api.Models.DTO
{
    public class AddJobRequestDto
    {
        public string JobTitle { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; } 
    }
}
