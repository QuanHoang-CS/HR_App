namespace net_core_web_api.Models.DTO
{
    public class AddDependentRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public int EmployeeId { get; set; }
    }
}
