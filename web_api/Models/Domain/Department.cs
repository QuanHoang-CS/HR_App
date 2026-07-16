using System.ComponentModel.DataAnnotations;

namespace MyApp.API.Models.Domain
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? LocationId { get; set; }
    }
}
