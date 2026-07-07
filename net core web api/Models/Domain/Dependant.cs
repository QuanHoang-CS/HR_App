namespace net_core_web_api.Models.Domain

{
    public class Dependant
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public int EmployeeId { get; set; }
    }
}
