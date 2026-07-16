namespace MyApp.API.Models.DTO
{
    public class AddDepartmentRequestDto
    {
        // DepartmentId is self-incremented and cannot/shouldn't be manually entered
        // So there's no point having it here
        public string DepartmentName { get; set; }
        public int LocationId { get; set; }
    }
}
