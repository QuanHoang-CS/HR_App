namespace net_core_web_api.Models.DTO
{
    public class JobDto
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
    }
}
