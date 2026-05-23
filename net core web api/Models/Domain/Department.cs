using System.ComponentModel.DataAnnotations;

namespace net_core_web_api.Models.Domain
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int LocationId { get; set; }
    }
}
