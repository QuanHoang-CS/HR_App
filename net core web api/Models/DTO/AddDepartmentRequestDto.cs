namespace net_core_web_api.Models.DTO
{
    public class AddDepartmentRequestDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int LocationId { get; set; }
    }
}
