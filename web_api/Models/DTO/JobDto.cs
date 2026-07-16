namespace MyApp.API.Models.DTO
{
    public class JobDto
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
    }
}
